# ZoneGen:: VDC Secure Network Modeling 
Zonegen is a CLI tool for creating, managing, and visualising network security zone models for virtual data centres.

[![Build Status](https://dev.azure.com/jordownado/zonegen/_apis/build/status/zonegen-CI)](https://dev.azure.com/jordownado/zonegen/_build/latest?definitionId=2)

## Features

* Strong named zones: All zones have an unique and addressable id

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

![graph](https://user-images.githubusercontent.com/5225782/47061287-8f4e7100-d21c-11e8-9654-89fa35bcc272.png)
