#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Systems/Identity/Welisten.Identity/Welisten.Identity.csproj", "Systems/Identity/Welisten.Identity/"]
COPY ["Data/Welisten.Context/Welisten.Context.csproj", "Data/Welisten.Context/"]
COPY ["Services/Welisten.Services.Settings/Welisten.Services.Settings.csproj", "Services/Welisten.Services.Settings/"]
COPY ["Shared/Welisten.Common/Welisten.Common.csproj", "Shared/Welisten.Common/"]
COPY ["Data/Welisten.Context.Entities/Welisten.Context.Entities.csproj", "Data/Welisten.Context.Entities/"]
RUN dotnet restore "./Systems/Identity/Welisten.Identity/./Welisten.Identity.csproj"
COPY . .
WORKDIR "/src/Systems/Identity/Welisten.Identity"
RUN dotnet build "./Welisten.Identity.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Welisten.Identity.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Welisten.Identity.dll"]