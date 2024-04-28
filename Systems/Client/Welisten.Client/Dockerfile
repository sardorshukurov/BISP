FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Systems/Client/Welisten.Client/Welisten.Client.csproj", "Systems/Client/Welisten.Client/"]
RUN dotnet restore "Systems/Client/Welisten.Client/Welisten.Client.csproj"
COPY . .
WORKDIR "/src/Systems/Client/Welisten.Client"
RUN dotnet build "Welisten.Client.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Welisten.Client.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM nginx:alpine
WORKDIR /var/www/web

COPY --from=publish /app/publish/wwwroot .
COPY Systems/Client/Welisten.Client/nginx.conf /etc/nginx/nginx.conf
EXPOSE 82
