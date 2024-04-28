FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Systems/API/Welisten.API/Welisten.API.csproj", "Systems/API/Welisten.API/"]
RUN dotnet restore "Systems/API/Welisten.API/Welisten.API.csproj"
COPY . .
WORKDIR "/src/Systems/API/Welisten.API"
RUN dotnet build "Welisten.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Welisten.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Copy files to temporary directory
COPY Data/Welisten.Context.Seeder/Demo/articles.csv /sample_data/articles.csv
COPY Data/Welisten.Context.Seeder/Demo/categories.csv /sample_data/categories.csv

# RUN chmod 777 sample_data/articles.csv
# RUN chmod 777 sample_data/categories.csv


# Set the entry point
ENTRYPOINT ["dotnet", "Welisten.API.dll"]
