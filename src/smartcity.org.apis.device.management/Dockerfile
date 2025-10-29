# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only the project files first (improves layer caching) - paths are relative to the build context (repo root)
COPY ["src/smartcity.org.apis.device.management/smartcity.org.apis.device.management.csproj", "src/smartcity.org.apis.device.management/"]
COPY ["src/smartcity.org.apis.arch/smartcity.org.apis.arch.csproj", "src/smartcity.org.apis.arch/"]
RUN dotnet restore "src/smartcity.org.apis.device.management/smartcity.org.apis.device.management.csproj"

# Copy the full repository into the image
COPY . .

RUN dotnet publish "src/smartcity.org.apis.device.management/smartcity.org.apis.device.management.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "smartcity.org.apis.device.management.dll"]
