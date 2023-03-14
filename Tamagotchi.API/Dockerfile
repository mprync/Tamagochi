FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Tamagotchi.API/Tamagotchi.API.csproj", "./"]
RUN dotnet restore "Tamagotchi.API.csproj"
COPY . .
WORKDIR "/src/Tamagotchi.API"
RUN dotnet build "Tamagotchi.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tamagotchi.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tamagotchi.API.dll"]