FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ElegantRobotRover/ElegantRobotRover.csproj", "ElegantRobotRover/"]
RUN dotnet restore "ElegantRobotRover/ElegantRobotRover.csproj"
COPY . .
WORKDIR "/src/ElegantRobotRover"
RUN dotnet build "ElegantRobotRover.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ElegantRobotRover.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ElegantRobotRover.dll"]
