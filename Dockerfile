FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore
COPY ["LogisticsSolution/LogisticsSolution.Api.csproj", "LogisticsSolution/"]
RUN dotnet restore "LogisticsSolution/LogisticsSolution.Api.csproj"

# Copy the rest of the project
COPY . .


# Set working directory to the project folder before publishing
WORKDIR /src/LogisticsSolution
RUN dotnet publish "LogisticsSolution.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "LogisticsSolution.Api.dll"]
