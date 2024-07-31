FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

ENV ASPNETCORE_Kestrel__Certificates__Default__Password=TestPassword
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx

RUN mkdir /https
COPY ../certs/aspnetapp.pfx /https/

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ImageVault.ImageService/ImageVault.ImageService.csproj", "ImageVault.ImageService/"]
RUN dotnet restore "ImageVault.ImageService/ImageVault.ImageService.csproj"
COPY . .
WORKDIR "/src/ImageVault.ImageService"
RUN dotnet build "ImageVault.ImageService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ImageVault.ImageService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ImageVault.ImageService.dll"]
