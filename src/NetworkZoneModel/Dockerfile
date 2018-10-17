FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

COPY . ./
RUN dotnet restore NetworkZoneModel.sln -r linux-x64
RUN dotnet build -c Release -r linux-x64
RUN dotnet publish -c Release -r linux-x64 --output Publish/linux 

# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/NetworkZoneModelCli/Publish/linux .
RUN echo '[ ! -z "$TERM" -a -r /etc/motd ] && cat /etc/issue && cat /etc/motd' \
    >> /etc/bash.bashrc \
    ; echo "\
===================================================================\n\
=   ZoneGen:: VDC Secure Network Modeling                         =\n\
===================================================================\n\
\n\
ZoneGen: Model Network Security .\n\
\n\
Get started, run 'zm' \n"\
> /etc/motd
RUN echo 'alias zm="dotnet /app/NetworkZoneModelCli.dll"' >> ~/.bashrc
ENTRYPOINT ["dotnet", "NetworkZoneModelCli.dll"]
