FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy and restore only the API project first
COPY ["LogisticsSolution/LogisticsSolution.Api.csproj", "LogisticsSolution/"]
WORKDIR /src/LogisticsSolution
RUN dotnet restore "LogisticsSolution.Api.csproj"

# Copy the rest of the solution
COPY . .
RUN dotnet publish "LogisticsSolution.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "LogisticsSolution.Api.dll"]
