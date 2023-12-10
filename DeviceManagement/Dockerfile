#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DeviceManagement/DeviceManagement.csproj", "DeviceManagement/"]
COPY ["DeviceManagementService/DeviceManagementService.csproj", "DeviceManagementService/"]
COPY ["DevicesManagementData/DevicesManagementData.csproj", "DevicesManagementData/"]
COPY ["DevicesManagementDomain/DevicesManagementDomain.csproj", "DevicesManagementDomain/"]
RUN dotnet restore "DeviceManagement/DeviceManagement.csproj"
COPY . .
WORKDIR "/src/DeviceManagement"
RUN dotnet build "DeviceManagement.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeviceManagement.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeviceManagement.dll"]