FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY TestConnectDB/TestConnectDB.csproj TestConnectDB/
RUN dotnet restore "TestConnectDB/TestConnectDB.csproj"
COPY . .
WORKDIR "/src/TestConnectDB"
RUN dotnet build "TestConnectDB.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestConnectDB.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestConnectDB.dll"]
