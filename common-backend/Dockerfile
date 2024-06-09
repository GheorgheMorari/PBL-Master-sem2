#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/MasterBackEnd.Api/MasterBackEnd.Api.csproj", "src/MasterBackEnd.Api/"]
COPY ["src/MasterBackEnd.Infrastructure/MasterBackEnd.Infrastructure.csproj", "src/MasterBackEnd.Infrastructure/"]
COPY ["src/Modules/Identity/MasterBackEnd.Identity.Infrastructure/MasterBackEnd.Identity.Infrastructure.csproj", "src/Modules/Identity/MasterBackEnd.Identity.Infrastructure/"]
COPY ["src/Modules/Identity/MasterBackEnd.Identity.Application/MasterBackEnd.Identity.Application.csproj", "src/Modules/Identity/MasterBackEnd.Identity.Application/"]
COPY ["src/Modules/Identity/MasterBackEnd.Identity.Contracts/MasterBackEnd.Identity.Contracts.csproj", "src/Modules/Identity/MasterBackEnd.Identity.Contracts/"]
COPY ["src/Modules/Identity/MasterBackEnd.Identity.Domain/MasterBackEnd.Identity.Domain.csproj", "src/Modules/Identity/MasterBackEnd.Identity.Domain/"]
RUN dotnet restore "./src/MasterBackEnd.Api/MasterBackEnd.Api.csproj"
COPY . .
WORKDIR "/src/src/MasterBackEnd.Api"
RUN dotnet build "./MasterBackEnd.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MasterBackEnd.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MasterBackEnd.Api.dll"]