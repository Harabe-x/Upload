#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

ENV ASPNETCORE_Kestrel__Certificates__Default__Password=TestPassword
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx

RUN mkdir /https
COPY ../certs/aspnetapp.pfx /https/

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

RUN apt-get update && apt-get install -y curl \
    && curl -sL https://deb.nodesource.com/setup_14.x | bash - \
    && apt-get install -y nodejs

COPY ["ImageVault.AuthenticationService/ImageVault.AuthenticationService.csproj", "ImageVault.AuthenticationService/"]
COPY ["ImageVault.ClassLibrary/ImageVault.ClassLibrary.csproj", "ImageVault.ClassLibrary/"]
RUN dotnet restore "./ImageVault.AuthenticationService/ImageVault.AuthenticationService.csproj"
COPY . .
WORKDIR "/src/ImageVault.AuthenticationService"
RUN dotnet build "./ImageVault.AuthenticationService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ImageVault.AuthenticationService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ImageVault.AuthenticationService.dll"]
