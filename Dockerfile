# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

COPY . .
RUN dotnet publish ./DockerDotNet.csproj -c release -o /app

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
#https://www.educba.com/docker-expose/
#EXPOSE is a way of documenting
EXPOSE 80
EXPOSE 443
COPY --from=build-env /app .
ENTRYPOINT ["dotnet", "DockerDotNet.dll"]