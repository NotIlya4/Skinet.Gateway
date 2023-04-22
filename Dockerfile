﻿FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Restore
WORKDIR /src
COPY src/Api/Api.csproj Api/
COPY src/Infrastructure/Infrastructure.csproj Infrastructure/
COPY src/Domain/Domain.csproj Domain/
WORKDIR /src/Api
RUN dotnet restore

# Copy rest files
WORKDIR /src
COPY src/ .

# Build
WORKDIR /src/Api
RUN dotnet build "Api.csproj" -c Release -o /app/build
RUN dotnet publish "Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
EXPOSE 80
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]