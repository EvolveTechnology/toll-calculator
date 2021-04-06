#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Toll.Calculator.WebAPI/Toll.Calculator.WebAPI.csproj", "Toll.Calculator.WebAPI/"]
RUN dotnet restore "Toll.Calculator.WebAPI/Toll.Calculator.WebAPI.csproj"
COPY . .
WORKDIR "/src/Toll.Calculator.WebAPI"
RUN dotnet build "Toll.Calculator.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Toll.Calculator.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Toll.Calculator.WebAPI.dll"]