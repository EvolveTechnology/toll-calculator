#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/runtime:3.0-nanoserver-1709 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-nanoserver-1709 AS build
WORKDIR /src
COPY ["Evolve.TollFeeCalculator/Evolve.TollFeeCalculator.csproj", "Evolve.TollFeeCalculator/"]
RUN dotnet restore "Evolve.TollFeeCalculator/Evolve.TollFeeCalculator.csproj"
COPY . .
WORKDIR "/src/Evolve.TollFeeCalculator"
RUN dotnet build "Evolve.TollFeeCalculator.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Evolve.TollFeeCalculator.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Evolve.TollFeeCalculator.dll"]