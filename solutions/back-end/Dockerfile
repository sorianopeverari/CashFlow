FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src

COPY ["CashFlow.Api/CashFlow.Api.csproj", "CashFlow.Api/"]
COPY ["CashFlow.Domain/CashFlow.Domain.Models/CashFlow.Domain.Models.csproj", "CashFlow.Domain/CashFlow.Domain.Models/"]
COPY ["CashFlow.Domain/CashFlow.Domain.Business/CashFlow.Domain.Business.csproj", "CashFlow.Domain/CashFlow.Domain.Business/"]
COPY ["CashFlow.Domain/CashFlow.Domain.Repositories/CashFlow.Domain.Repositories.csproj", "CashFlow.Domain/CashFlow.Domain.Repositories/"]
COPY ["CashFlow.Infra/CashFlow.Infra.Repositories.PgDW/CashFlow.Infra.Repositories.PgDW.csproj", "CashFlow.Infra/CashFlow.Infra.Repositories.PgDW/"]
COPY ["CashFlow.Infra/CashFlow.Infra.Repositories.PgRDS/CashFlow.Infra.Repositories.PgRDS.csproj", "CashFlow.Infra/CashFlow.Infra.Repositories.PgRDS/"]
COPY ["CashFlow.Infra/CashFlow.Infra.Repositories.Providers/CashFlow.Infra.Repositories.Providers.csproj", "CashFlow.Infra/CashFlow.Infra.Repositories.Providers/"]

RUN dotnet restore "CashFlow.Api/CashFlow.Api.csproj"
COPY . .
WORKDIR "/src/CashFlow.Api"
RUN dotnet build "CashFlow.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CashFlow.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CashFlow.Api.dll"]
