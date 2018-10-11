# ZoneGen:: VDC Secure Network Modeling 
Zonegen is a CLI tool for creating, managing, and visualising network security zone models for virtual data centres.

## Run the CLI

Initialise a Zone Model project with directory structure and config files
` dotnet run --project src\NetworkZoneModel\NetworkZoneModelCli\NetworkZoneModelCli.csproj -- init -g testgroup -r australiaeast -e dev`

Parse the project files

`dotnet run --project src\NetworkZoneModel\NetworkZoneModelCli\NetworkZoneModelCli.csproj -- parse parse -d templates -z testgroup`

To write out the final model, use `--write`

Generate some sample output

`dotnet run --project src\NetworkZoneModel\NetworkZoneModelCli\NetworkZoneModelCli.csproj -- sample -o output.yaml`
