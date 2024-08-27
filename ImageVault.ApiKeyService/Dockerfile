FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

ENV ASPNETCORE_Kestrel__Certificates__Default__Password=TestPassword
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx

ENV RABBITMQ_USERNAME = YOUR_USERNAME
ENV RABBITMQ_PASSWORD = YOUR_PASSWORD
ENV DB_CONNECTION_STRING = YOUR_CONNECTION_STRING
ENV JWT_SIGNING_KEY = YOUR_SIGNING_KEY

RUN mkdir /https
COPY ../certs/aspnetapp.pfx /https/

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ImageVault.ApiKeyService/ImageVault.ApiKeyService.csproj", "ImageVault.ApiKeyService/"]
RUN dotnet restore "ImageVault.ApiKeyService/ImageVault.ApiKeyService.csproj"
COPY . .
WORKDIR "/src/ImageVault.ApiKeyService"
RUN dotnet build "ImageVault.ApiKeyService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ImageVault.ApiKeyService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ImageVault.ApiKeyService.dll"]
