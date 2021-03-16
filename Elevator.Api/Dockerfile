FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Elevator.Api/Elevator.Api.csproj", "Elevator.Api/"]
RUN dotnet restore "Elevator.Api/Elevator.Api.csproj"
COPY . .
WORKDIR "/src/Elevator.Api"
RUN dotnet build "Elevator.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Elevator.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Elevator.Api.dll"]