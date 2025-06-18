# SDK stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .

# Optional: Clean/restore with retry
RUN dotnet restore "LogisticsSolution/LogisticsSolution.Api.csproj" \
    --disable-parallel --interactive --verbosity minimal

RUN dotnet build "LogisticsSolution/LogisticsSolution.Api.csproj" -c Release -o /app/build
RUN dotnet publish "LogisticsSolution/LogisticsSolution.Api.csproj" -c Release -o /app/publish
