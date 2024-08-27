#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app


ENV RABBITMQ_USERNAME = YOUR_USERNAME
ENV RABBITMQ_PASSWORD = YOUR_PASSWORD
ENV DB_CONNECTION_STRING = YOUR_CONNECTION_STRING
ENV JWT_SIGNING_KEY = YOUR_SIGNING_KEY

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ImageVault.UserService/ImageVault.UserService.csproj", "ImageVault.UserService/"]
RUN dotnet restore "./ImageVault.UserService/ImageVault.UserService.csproj"
COPY . .
WORKDIR "/src/ImageVault.UserService"
RUN dotnet build "./ImageVault.UserService.csproj" -c $BUILD_CONFIGURATION -o /app/build

ENV ASPNETCORE_Kestrel__Certificates__Default__Password=TestPassword
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx

RUN mkdir /https
COPY ../certs/aspnetapp.pfx /https/



FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ImageVault.UserService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ImageVault.UserService.dll"]