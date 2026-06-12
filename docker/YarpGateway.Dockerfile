FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
WORKDIR /app

COPY src/Gateways/SabakaMarket.YarpGateway/*.csproj src/Gateways/SabakaMarket.YarpGateway/

RUN dotnet restore src/Gateways/SabakaMarket.YarpGateway/SabakaMarket.YarpGateway.csproj

COPY . .
WORKDIR /app/src/Gateways/SabakaMarket.YarpGateway
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS runtime
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SabakaMarket.YarpGateway.dll"]