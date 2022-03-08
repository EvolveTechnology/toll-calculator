FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Evolve.TollCalculator.API/Evolve.TollCalculator.API.csproj", "Evolve.TollCalculator.API/"]
RUN dotnet restore "Evolve.TollCalculator.API/Evolve.TollCalculator.API.csproj"
COPY . .
WORKDIR "/src/Evolve.TollCalculator.API"
RUN dotnet build "Evolve.TollCalculator.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Evolve.TollCalculator.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Evolve.TollCalculator.API.dll"]