#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["API/Coodesh.API/Coodesh.API.csproj", "API/Coodesh.API/"]
COPY ["API/Coodesh.Application/Coodesh.Application.csproj", "API/Coodesh.Application/"]
COPY ["API/Coodesh.Infrastructure/Coodesh.Infrastructure.csproj", "API/Coodesh.Infrastructure/"]
RUN dotnet restore "API/Coodesh.API/Coodesh.API.csproj"
COPY . .
WORKDIR "/src/API/Coodesh.API"
RUN dotnet build "Coodesh.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Coodesh.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Coodesh.API.dll"]