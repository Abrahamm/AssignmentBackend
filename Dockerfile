# Use the official ASP.NET Core 7.0 runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 7231

# Use the official ASP.NET Core 7.0 SDK as a parent image for building
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy the .csproj files and restore as distinct layers
COPY ["Assignment.API/Assignment.API.csproj", "Assignment.API/"]
COPY ["Assignment.Application/Assignment.Application.csproj", "Assignment.Application/"]
COPY ["Assignment.Infrastructure/Assignment.Infrastructure.csproj", "Assignment.Infrastructure/"]
COPY ["Assignment.Domain/Assignment.Domain.csproj", "Assignment.Domain/"]
COPY ["Assignment.Persistence/Assignment.Persistence.csproj", "Assignment.Persistence/"]
COPY ["Assignment.Shared/Assignment.Shared.csproj", "Assignment.Shared/"]

RUN dotnet restore "Assignment.API/Assignment.API.csproj"

# Copy the rest of the application code
COPY . .

# Build the application
WORKDIR "/src/Assignment.API"
RUN dotnet build "Assignment.API.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "Assignment.API.csproj" -c Release -o /app/publish

# Build the final image using the publish directory
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Assignment.API.dll"]
