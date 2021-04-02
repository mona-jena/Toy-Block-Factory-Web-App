FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source
EXPOSE 3000
COPY ./ /source
RUN dotnet publish -c release -o /release

FROM mcr.microsoft.com/dotnet/runtime:5.0
WORKDIR /webapp
COPY --from=build /release ./
#execute this when we go docker run, everything before this is executed at build time. 
#Enterpoint is equivalent to dotnet run.
ENTRYPOINT ["dotnet", "ToyBlockFactoryWebApp.dll"]