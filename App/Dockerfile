FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build 
COPY App/ /source
WORKDIR /source
RUN dotnet publish -c release -o /App --no-restore

FROM mcr.microsoft.com/dotnet/runtime:5.0 
WORKDIR /App
COPY --from=build /App ./
ENTRYPOINT ["dotnet", "build"]