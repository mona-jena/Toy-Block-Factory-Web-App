FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build 
WORKDIR /source
EXPOSE 3000
COPY ./ /source
RUN dotnet publish -c release -o /release
RUN dotnet test

FROM mcr.microsoft.com/dotnet/runtime:5.0
WORKDIR /webapp
COPY --from=build /release ./
#execute this when we go docker run, everything before this is executed at build time
ENTRYPOINT ["dotnet", "ToyBlockFactoryWebApp.dll"]   