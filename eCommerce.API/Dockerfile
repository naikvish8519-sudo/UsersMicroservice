#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8090
EXPOSE 8091

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files (all referenced projects)
COPY ["eCommerce.API/eCommerce.API.csproj", "eCommerce.API/"]
COPY ["eCommerce.Core/eCommerce.Core.csproj", "eCommerce.Core/"]
COPY ["eCommerce.Infrastructure/eCommerce.Infrastructure.csproj", "eCommerce.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "eCommerce.API/eCommerce.API.csproj"

# Copy everything else
COPY . .
WORKDIR "/src/eCommerce.API"

# Build
RUN dotnet build "eCommerce.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "eCommerce.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# ✅ Force ASP.NET Core to listen on 8090 inside the container
ENV ASPNETCORE_URLS="http://+:8090"

# ✅ Inject SQL connection string (matches appsettings.json key: SqlServerConnection)
ENV ConnectionStrings__SqlServerConnection="Server=tcp:host.docker.internal,1433;Database=Microservices;User Id=dockeruser;Password=StrongPass123!;Encrypt=True;TrustServerCertificate=True;"

ENTRYPOINT ["dotnet", "eCommerce.API.dll"]
