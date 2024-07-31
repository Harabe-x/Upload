FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ImageVault.UploadService/ImageVault.UploadService.csproj", "ImageVault.UploadService/"]
RUN dotnet restore "ImageVault.UploadService/ImageVault.UploadService.csproj"
COPY . .
WORKDIR "/src/ImageVault.UploadService"
RUN dotnet build "ImageVault.UploadService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ImageVault.UploadService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ImageVault.UploadService.dll"]
