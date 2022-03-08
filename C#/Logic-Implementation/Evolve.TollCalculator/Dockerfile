FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY ["Evolve.TollCalculator/Evolve.TollCalculator.csproj", "Evolve.TollCalculator/"]
COPY ["Evolve.TollCalculator.Test/Evolve.TollCalculator.Test.csproj", "Evolve.TollCalculator.Test/"]

RUN dotnet restore "Evolve.TollCalculator/Evolve.TollCalculator.csproj"
RUN dotnet restore "Evolve.TollCalculator.Test/Evolve.TollCalculator.Test.csproj"

COPY . .

WORKDIR "/src"

RUN dotnet build "Evolve.TollCalculator/Evolve.TollCalculator.csproj" -c Release -o /app/build
RUN dotnet build "Evolve.TollCalculator.Test/Evolve.TollCalculator.Test.csproj" -c Release -o /app/build

RUN dotnet test "Evolve.TollCalculator.Test/Evolve.TollCalculator.Test.csproj" --logger "trx;LogFileName=TollCalculator.trx" 

FROM build AS publish
RUN dotnet publish "Evolve.TollCalculator/Evolve.TollCalculator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Evolve.TollCalculator.dll"]