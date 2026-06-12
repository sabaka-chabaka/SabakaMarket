FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
WORKDIR /app

COPY src/BuildingBlocks/SabakaMarket.Contracts/*.csproj src/BuildingBlocks/SabakaMarket.Contracts/
COPY src/Services/OrderService/SabakaMarket.OrderService.Domain/*.csproj src/Services/OrderService/SabakaMarket.OrderService.Domain/
COPY src/Services/OrderService/SabakaMarket.OrderService.Application/*.csproj src/Services/OrderService/SabakaMarket.OrderService.Application/
COPY src/Services/OrderService/SabakaMarket.OrderService.Infrastructure/*.csproj src/Services/OrderService/SabakaMarket.OrderService.Infrastructure/
COPY src/Services/OrderService/SabakaMarket.OrderService.API/*.csproj src/Services/OrderService/SabakaMarket.OrderService.API/

RUN dotnet restore src/Services/OrderService/SabakaMarket.OrderService.API/SabakaMarket.OrderService.API.csproj

COPY . .
WORKDIR /app/src/Services/OrderService/SabakaMarket.OrderService.API
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS runtime
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SabakaMarket.OrderService.API.dll"]