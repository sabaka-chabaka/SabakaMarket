FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
WORKDIR /app

COPY src/BuildingBlocks/SabakaMarket.Contracts/*.csproj src/BuildingBlocks/SabakaMarket.Contracts/
COPY src/Services/UserService/SabakaMarket.UserService.Domain/*.csproj src/Services/UserService/SabakaMarket.UserService.Domain/
COPY src/Services/UserService/SabakaMarket.UserService.Application/*.csproj src/Services/UserService/SabakaMarket.UserService.Application/
COPY src/Services/UserService/SabakaMarket.UserService.Infrastructure/*.csproj src/Services/UserService/SabakaMarket.UserService.Infrastructure/
COPY src/Services/UserService/SabakaMarket.UserService.API/*.csproj src/Services/UserService/SabakaMarket.UserService.API/

RUN dotnet restore src/Services/UserService/SabakaMarket.UserService.API/SabakaMarket.UserService.API.csproj

COPY . .
WORKDIR /app/src/Services/UserService/SabakaMarket.UserService.API
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS runtime
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SabakaMarket.UserService.API.dll"]