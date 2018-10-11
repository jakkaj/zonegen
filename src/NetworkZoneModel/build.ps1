dotnet build -c Release -r osx.10.12-x64
dotnet build -c Release -r linux-x64
dotnet build -c Release -r win10-x64

dotnet test ZoneModel.Tests -c Release --logger trx

dotnet publish -c Release -r osx.10.12-x64 --output Publish\osx 
dotnet publish -c Release -r linux-x64 --output Publish\linux 
dotnet publish -c Release -r win10-x64 --output Publish\win 