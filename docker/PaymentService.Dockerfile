FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
WORKDIR /app

COPY src/BuildingBlocks/SabakaMarket.Contracts/*.csproj src/BuildingBlocks/SabakaMarket.Contracts/
COPY src/Services/PaymentService/SabakaMarket.PaymentService.Domain/*.csproj src/Services/PaymentService/SabakaMarket.PaymentService.Domain/
COPY src/Services/PaymentService/SabakaMarket.PaymentService.Application/*.csproj src/Services/PaymentService/SabakaMarket.PaymentService.Application/
COPY src/Services/PaymentService/SabakaMarket.PaymentService.Infrastructure/*.csproj src/Services/PaymentService/SabakaMarket.PaymentService.Infrastructure/
COPY src/Services/PaymentService/SabakaMarket.PaymentService.API/*.csproj src/Services/PaymentService/SabakaMarket.PaymentService.API/

RUN dotnet restore src/Services/PaymentService/SabakaMarket.PaymentService.API/SabakaMarket.PaymentService.API.csproj

COPY . .
WORKDIR /app/src/Services/PaymentService/SabakaMarket.PaymentService.API
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS runtime
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SabakaMarket.PaymentService.API.dll"]