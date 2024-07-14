FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ImageVault.RequestMetricsService/ImageVault.RequestMetricsService.csproj", "ImageVault.RequestMetricsService/"]
RUN dotnet restore "ImageVault.RequestMetricsService/ImageVault.RequestMetricsService.csproj"
COPY . .
WORKDIR "/src/ImageVault.RequestMetricsService"
RUN dotnet build "ImageVault.RequestMetricsService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ImageVault.RequestMetricsService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ImageVault.RequestMetricsService.dll"]
