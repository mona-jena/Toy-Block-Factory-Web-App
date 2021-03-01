#let's compile and build an image
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build 
COPY ./ /source
WORKDIR /source
RUN dotnet publish -c release -o /release

#create an image to run
FROM mcr.microsoft.com/dotnet/runtime:5.0 
WORKDIR /app
COPY --from=build /release ./
ENTRYPOINT ["dotnet", "ToyBlockFactoryConsole.dll"]
                                  