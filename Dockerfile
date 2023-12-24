FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY ./CryptoTestApi.WebApi/CryptoTestApi.WebApi.csproj ./CryptoTestApi.WebApi/CryptoTestApi.WebApi.csproj
COPY ./CryptoTestApi.Infrastructure/CryptoTestApi.Infrastructure.csproj ./CryptoTestApi.Infrastructure/CryptoTestApi.Infrastructure.csproj
COPY ./CryptoTestApi.Application/CryptoTestApi.Application.csproj ./CryptoTestApi.Application/CryptoTestApi.Application.csproj
COPY ./CryptoTestApi.Domain/CryptoTestApi.Domain.csproj ./CryptoTestApi.Domain/CryptoTestApi.Domain.csproj

# restore only main project, it references everything that is required
RUN dotnet restore ./CryptoTestApi.WebApi/CryptoTestApi.WebApi.csproj

COPY ./CryptoTestApi.WebApi ./CryptoTestApi.WebApi
COPY ./CryptoTestApi.Infrastructure ./CryptoTestApi.Infrastructure
COPY ./CryptoTestApi.Application ./CryptoTestApi.Application
COPY ./CryptoTestApi.Domain ./CryptoTestApi.Domain

RUN dotnet publish ./CryptoTestApi.WebApi/CryptoTestApi.WebApi.csproj -c Release -o out --no-restore

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out /app

ENTRYPOINT ["dotnet", "/app/CryptoTestApi.WebApi.dll"]