FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY MyGarden.Server/MyGarden.Server.csproj MyGarden.Server/
RUN dotnet restore MyGarden.Server/MyGarden.Server.csproj
COPY . .
WORKDIR /src/MyGarden.Server
RUN dotnet build MyGarden.Server.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish MyGarden.Server.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "MyGarden.Server.dll" ]
