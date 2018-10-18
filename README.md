# ZoneGen:: VDC Secure Network Modeling 
Zonegen is a CLI tool for creating, managing, and visualising network security zone models for virtual data centres.

[![Build Status](https://dev.azure.com/jordownado/zonegen/_apis/build/status/zonegen-CI)](https://dev.azure.com/jordownado/zonegen/_build/latest?definitionId=2)

## Background

[Virtual Data Centres](https://docs.microsoft.com/en-us/azure/architecture/vdc/networking-virtual-datacenter) (VDC) is a concept that covers the extension of enterprise networks to the cloud. It covers networking, security, application services (like DNS, AD etc.) plus more. 

Networks are created in cloud environments. These networks may have a multitude of environments, each with a multitude of zones, each with a multitude of security restrictions. 

E.g. An application may have many environments like dev, pre-pro and prod. Each of these environments may have a range of sub-zones like front-end, back-end and management. Each environment may have another set of rules such as "can route back to on-premises". Perhaps production can route, but dev cannot. 

Initially these systems may be small and simple to manage, but over time it will gain in size and complexity and become increasingly hard to manage. 

Usually (in Azure) these environments and zones will manifest as Virtual Networks with Subnets for the Zones. The permissions will manifest as routes and Network Security Groups. The Virtual Networks will normally connect back to a "hub" network (with the environment networks representing the "spokes").

## Abstraction

Often these systems manifest as an array of complex configuration files and scripts. The files will be taken and used to build out templates (say ARM) that are applied to the network. 

Without abstraction, these scripts and files often violate single responsibility rules (config is not infrastructure as code etc.). 

The way we think about it is - unless you can write a script that creates a visual representation of your configuration (without having to know about how it's applied to a cloud) then it's not abstracted enough. 

That's what this project does. 

## Simple Config, Single Responsibility

This project takes a very simple configuration layout and turns it in to a file that can then be used by "something else" to apply the configurations to a cloud. It's now Azure or AWS specific. The "something else" could be Anisble or Terraform or ARM. It doesn't matter. The responsibility of the output of this projec tis to be simple and elegant and readable. It's so simple, that we can graph the outputs with GraphViz with just a few lines of [code](https://github.com/jakkaj/zonegen/blob/master/src/NetworkZoneModel/ZoneModel.Services/Utils/GraphWriter.cs)!


## Getting started

The idea is that you take a set of environments and zones and turn them in to a config file. You can use the docker instructions below if you do not want to have to set up a .NET Core environment on your machine. 

### Inititalise the sample configs

Initialise a Zone Model project with directory structure and config files.

```
 dotnet run --project src\NetworkZoneModel\NetworkZoneModelCli\NetworkZoneModelCli.csproj -- init -g testgroup -r australiaeast -e dev
 ```

This will create a neat little template structure under the templates folder. 

In here you will find folders - they form part of the convention. The structure is: zonegroup/region/environment/. 

In this structure you will find Environments, Zones and rules. 

### Environments

Under the environment folder (which is "dev" in this example) there is `config.yaml` which contains the all the environments for the region as well as their IP address CIDR ranges. This is the only place where IP addresses will be seen in this layout. 

```yaml
regions:
- id: australiaeast
  environments:
  - id: dev
    cidr: 10.2.0.0/16
    zoneSubnetMaskSize: 24
    zones: []
    rules: []
  - id: hub
    ignore: true
    cidr: 10.1.0.0/16
    zoneSubnetMaskSize: 24
    zones: []
    rules: []

```

### IP address management

In complex VDC systems, IP address management is a problem. Networks and zones need their addresses stored somewhere for later reference to ensure everything says sane and routable. In this system we just store the IP address once and use convention to calculated the Zone subnet ranges. 

In the sample above we have two ranges - the "dev" environment and a reference to the hub we will be using. The CIDR range for the environemnt is /16. The subnet CIDR mask is /24. This is used in conjunection with the `zone.yaml` file to extract subnet ranges using the zone index offset. 

### Zones

`zones.yaml` contain a simple listing of zone names and indexes. 

```yaml
- id: frontend
  index: 1
- id: backend
  index: 2
```

The zone names make sense, you can reference them later from rules to allow transit and link them up. 

The index is used to calculate the subnet offset for this zone from the parent environment CIDR range. 

For example, the parent of "dev" is `10.2.0.0/16`. Index 2 in this range with a mask of `24` would be `10.2.2.0/24`. All that is needed is the index of 2 and the rest can be calculated. 

No messy IP addresses everywhere! Reference another zone using a fully qualified name later and it will automatically calculate the IP addresses during the config file build. 

### Rules

By default the Zones should be locked down - they cannot communicate with each other!

In this config, that is the case - if you graph the output wihout rules you will see no links are created between zones. 

```yaml
id: access-backend
from: frontend
to: backend
description: Allow frontend to access backend
isBidirectional: true
ports:
- 80
- 443

```

In this sample we allow the frontend zone to access the backend zone over port 80 and 443. It is also allowed to access the other way as it's set to `isBidirectional`. That's it. Just add the rules and they will manifest in the output config file as a rotatable rule that can be used to build out what ever routing template as required in Anisble or Terraform etc. 

## Get started with Docker

In your project directory (or in a new directory) add a `docker-compose.yaml` file, and copy the following:

```yaml
version:  '3'
services:
  # docker-compose run zm
  zm:
    image: xtellurian/zonegen:0.1 
    entrypoint: /bin/bash
    stdin_open: true
    tty: true
    container_name: zonemodel
    working_dir: /data
    volumes:
       - ./:/data
```

Run the CLI with `docker-compose run zm`

### Using the Docker container

* `zm --help` -> CLI help info
* `zm init -g product-group -r australiaeast -e devtest` -> Initialise a ZoneGen project
* `zm parse -g product-group` -> Parse and validate the project
* `zm parse -g product-group --write --strong` -> Write zone-variables.yaml with strongly names


## Run the CLI

Initialise a Zone Model project with directory structure and config files
` dotnet run --project src\NetworkZoneModel\NetworkZoneModelCli\NetworkZoneModelCli.csproj -- init -g testgroup -r australiaeast -e dev`

Parse the project files

`dotnet run --project src\NetworkZoneModel\NetworkZoneModelCli\NetworkZoneModelCli.csproj -- parse parse -d templates -g testgroup`

For strongly typed ids, use `--strong`

To write out the final model, use `--write`


Generate some sample output

`dotnet run --project src\NetworkZoneModel\NetworkZoneModelCli\NetworkZoneModelCli.csproj -- sample -o output.yaml`


## Visualise your Zone Graph

Add the switch `--graphviz` to generate a nice dot graph file in `zone.graph` that you can paste in to [http://www.webgraphviz.com/](http://www.webgraphviz.com/) and see what you're building before you deploy it!

![graph](https://user-images.githubusercontent.com/5225782/47061333-befd7900-d21c-11e8-9add-ec483f3fcc03.png)
