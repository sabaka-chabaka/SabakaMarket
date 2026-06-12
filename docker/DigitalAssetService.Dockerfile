FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
WORKDIR /app

COPY src/BuildingBlocks/SabakaMarket.Contracts/*.csproj src/BuildingBlocks/SabakaMarket.Contracts/
COPY src/Services/DigitalAssetService/SabakaMarket.DigitalAssetService.Domain/*.csproj src/Services/DigitalAssetService/SabakaMarket.DigitalAssetService.Domain/
COPY src/Services/DigitalAssetService/SabakaMarket.DigitalAssetService.Application/*.csproj src/Services/DigitalAssetService/SabakaMarket.DigitalAssetService.Application/
COPY src/Services/DigitalAssetService/SabakaMarket.DigitalAssetService.Infrastructure/*.csproj src/Services/DigitalAssetService/SabakaMarket.DigitalAssetService.Infrastructure/
COPY src/Services/DigitalAssetService/SabakaMarket.DigitalAssetService.API/*.csproj src/Services/DigitalAssetService/SabakaMarket.DigitalAssetService.API/

RUN dotnet restore src/Services/DigitalAssetService/SabakaMarket.DigitalAssetService.API/SabakaMarket.DigitalAssetService.API.csproj

COPY . .
WORKDIR /app/src/Services/DigitalAssetService/SabakaMarket.DigitalAssetService.API
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS runtime
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SabakaMarket.DigitalAssetService.API.dll"]