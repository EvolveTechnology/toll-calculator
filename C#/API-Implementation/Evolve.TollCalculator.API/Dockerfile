FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY ["Evolve.TollCalculator.API/Evolve.TollCalculator.API.csproj", "Evolve.TollCalculator.API/"]
COPY ["Evolve.TollCalculation.Application.UnitTest/Evolve.TollCalculation.Application.UnitTest.csproj", "Evolve.TollCalculator.Test/"]

RUN dotnet restore "Evolve.TollCalculator.API/Evolve.TollCalculator.API.csproj"
RUN dotnet restore "Evolve.TollCalculator.Test/Evolve.TollCalculation.Application.UnitTest.csproj"

COPY . .

WORKDIR "/src"

RUN dotnet build "Evolve.TollCalculator.API/Evolve.TollCalculator.API.csproj" -c Release -o /app/build
RUN dotnet build "Evolve.TollCalculation.Application.UnitTest/Evolve.TollCalculation.Application.UnitTest.csproj" -c Release -o /app/build

RUN dotnet test "Evolve.TollCalculation.Application.UnitTest/Evolve.TollCalculation.Application.UnitTest.csproj" --logger "trx;LogFileName=TollCalculator.trx" 

FROM build AS publish
RUN dotnet publish "Evolve.TollCalculator.API/Evolve.TollCalculator.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Evolve.TollCalculator.API.dll"]