# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "LogisticsSolution/LogisticsSolution.Api.csproj"
RUN dotnet build "LogisticsSolution/LogisticsSolution.Api.csproj" -c Release -o /app/build
RUN dotnet publish "LogisticsSolution/LogisticsSolution.Api.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT ["dotnet", "LogisticsSolution.Api.dll"]
