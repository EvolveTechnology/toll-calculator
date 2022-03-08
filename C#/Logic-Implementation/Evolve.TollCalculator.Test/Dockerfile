FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Evolve.TollCalculator.Test/Evolve.TollCalculator.Test.csproj", "Evolve.TollCalculator.Test/"]
COPY ["Evolve.TollCalculator/Evolve.TollCalculator.csproj", "Evolve.TollCalculator/"]
RUN dotnet restore "Evolve.TollCalculator.Test/Evolve.TollCalculator.Test.csproj"
COPY . .
WORKDIR "/src/Evolve.TollCalculator.Test"
RUN dotnet build "Evolve.TollCalculator.Test.csproj" -c Release -o /app/build

RUN dotnet test