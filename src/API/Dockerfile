#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/API/Dama.Planner.API.csproj", "src/API/"]
COPY ["src/Core.Contracts/Dama.Planner.Core.Contracts.csproj", "src/Core.Contracts/"]
COPY ["src/Core.Entities/Dama.Planner.Core.csproj", "src/Core.Entities/"]
COPY ["core/Common/Core.Common.csproj", "core/Common/"]
COPY ["src/Infrastructure.Service/Dama.Planner.Infrastructure.Reporting.csproj", "src/Infrastructure.Service/"]
COPY ["core/Protectimus/Core.ProtectimusClient.csproj", "core/Protectimus/"]
COPY ["src/Core/Dama.Planner.Application.csproj", "src/Core/"]
COPY ["core/Logger/Core.Logger.csproj", "core/Logger/"]
COPY ["src/Infrastructure.Persistence/Dama.Planner.Infrastructure.Persistence.csproj", "src/Infrastructure.Persistence/"]
COPY ["src/Infrastructure.Persistence.Configuration/Dama.Planner.Infrastructure.Persistence.Configuration.csproj", "src/Infrastructure.Persistence.Configuration/"]
RUN dotnet restore "src/API/Dama.Planner.API.csproj"
COPY . .
WORKDIR "/src/src/API"
RUN dotnet build "Dama.Planner.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dama.Planner.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dama.Planner.API.dll"]