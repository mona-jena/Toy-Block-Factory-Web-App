FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build 
COPY App/ /source
WORKDIR /source
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/runtime:5.0 
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["docker", "run"]
