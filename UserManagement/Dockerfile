# Generate Runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app

# Get base sdk image from microsoft
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

#EXPOSE 80
#EXPOSE 443

# clean all existing binaries
RUN find -type d -name bin -prune -exec rm -rf {} \; && find -type d -name obj -prune -exec rm -rf {} \;

# Copy required files and folders
COPY . /src/UserManagement

# Declare WorkDirectory
WORKDIR /src/UserManagement

# Fetch supporting DLLs
RUN dotnet restore UserManagement.csproj

# COPY Current Directory
#COPY . .

# Build the project
RUN dotnet build UserManagement.csproj -c Release -o /app/build

# Publish the project
FROM build AS publish
RUN dotnet publish UserManagement.csproj -c Release -o /app/publish

# ENTRYPOINT
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "UserManagement.dll"]