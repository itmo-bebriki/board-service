FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ./*.props ./

COPY ["src/Itmo.Bebriki.Boards/Itmo.Bebriki.Boards.csproj", "src/Itmo.Bebriki.Boards/"]

COPY ["src/Application/Itmo.Bebriki.Boards.Application/Itmo.Bebriki.Boards.Application.csproj", "src/Application/Itmo.Bebriki.Boards.Application/"]
COPY ["src/Application/Itmo.Bebriki.Boards.Application.Abstractions/Itmo.Bebriki.Boards.Application.Abstractions.csproj", "src/Application/Itmo.Bebriki.Boards.Application.Abstractions/"]
COPY ["src/Application/Itmo.Bebriki.Boards.Application.Contracts/Itmo.Bebriki.Boards.Application.Contracts.csproj", "src/Application/Itmo.Bebriki.Boards.Application.Contracts/"]
COPY ["src/Application/Itmo.Bebriki.Boards.Application.Models/Itmo.Bebriki.Boards.Application.Models.csproj", "src/Application/Itmo.Bebriki.Boards.Application.Models/"]

COPY ["src/Presentation/Itmo.Bebriki.Boards.Presentation.Kafka/Itmo.Bebriki.Boards.Presentation.Kafka.csproj", "src/Presentation/Itmo.Bebriki.Boards.Presentation.Kafka/"]
COPY ["src/Presentation/Itmo.Bebriki.Boards.Presentation.Grpc/Itmo.Bebriki.Boards.Presentation.Grpc.csproj", "src/Presentation/Itmo.Bebriki.Boards.Presentation.Grpc/"]

COPY ["src/Infrastructure/Itmo.Bebriki.Boards.Infrastructure.Persistence/Itmo.Bebriki.Boards.Infrastructure.Persistence.csproj", "src/Infrastructure/Itmo.Bebriki.Boards.Infrastructure.Persistence/"]

RUN dotnet restore "src/Itmo.Bebriki.Boards/Itmo.Bebriki.Boards.csproj"

COPY . .
WORKDIR "/src/src/Itmo.Bebriki.Boards"
RUN dotnet build "Itmo.Bebriki.Boards.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Itmo.Bebriki.Boards.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Itmo.Bebriki.Boards.dll"]