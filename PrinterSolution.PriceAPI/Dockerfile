#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PrinterSolution.PriceAPI/PrinterSolution.PriceAPI.csproj", "PrinterSolution.PriceAPI/"]
RUN dotnet restore "PrinterSolution.PriceAPI/PrinterSolution.PriceAPI.csproj"
COPY . .
WORKDIR "/src/PrinterSolution.PriceAPI"
RUN dotnet build "PrinterSolution.PriceAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PrinterSolution.PriceAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PrinterSolution.PriceAPI.dll"]