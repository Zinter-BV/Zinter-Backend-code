# Use the official .NET SDK image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore "LogisticsSolution/LogisticsSolution.Api/LogisticsSolution.Api.csproj"
RUN dotnet build "LogisticsSolution/LogisticsSolution.Api/LogisticsSolution.Api.csproj" -c Release -o /app/build
RUN dotnet publish "LogisticsSolution/LogisticsSolution.Api/LogisticsSolution.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "LogisticsSolution.Api.dll"]
