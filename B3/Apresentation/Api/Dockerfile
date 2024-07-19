# Use the official .NET SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the .csproj file and restore dependencies
COPY Applications/Application.csproj .
RUN dotnet restore

# Copy the rest of the application code
COPY Applications/ .
RUN dotnet publish -c Release -o out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Set environment variables (if needed)
ENV ASPNETCORE_ENVIRONMENT=Production

# Expose the port your app listens on
EXPOSE 80

# Start your .NET Core app
ENTRYPOINT ["dotnet", "Application.dll"]
