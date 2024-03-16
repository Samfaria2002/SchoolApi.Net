FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /App

# Copy everything
COPY *.csproj ./
# Restore as distinct layers
RUN dotnet restore

# Copy everything
COPY . ./
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /App

# Copy files from the build environment to the runtime environment
COPY --from=build-env /App/out .
# Copy the XML file to the runtime environment
COPY UdemyApiDotNet.xml .

ENTRYPOINT ["dotnet", "UdemyApiDotNet.dll"]
