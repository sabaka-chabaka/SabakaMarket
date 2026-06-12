FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
WORKDIR /app

COPY src/BuildingBlocks/SabakaMarket.Contracts/*.csproj src/BuildingBlocks/SabakaMarket.Contracts/
COPY src/Services/CatalogService/SabakaMarket.CatalogService.Domain/*.csproj src/Services/CatalogService/SabakaMarket.CatalogService.Domain/
COPY src/Services/CatalogService/SabakaMarket.CatalogService.Application/*.csproj src/Services/CatalogService/SabakaMarket.CatalogService.Application/
COPY src/Services/CatalogService/SabakaMarket.CatalogService.Infrastructure/*.csproj src/Services/CatalogService/SabakaMarket.CatalogService.Infrastructure/
COPY src/Services/CatalogService/SabakaMarket.CatalogService.API/*.csproj src/Services/CatalogService/SabakaMarket.CatalogService.API/

RUN dotnet restore src/Services/CatalogService/SabakaMarket.CatalogService.API/SabakaMarket.CatalogService.API.csproj

COPY . .
WORKDIR /app/src/Services/CatalogService/SabakaMarket.CatalogService.API
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS runtime
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SabakaMarket.CatalogService.API.dll"]