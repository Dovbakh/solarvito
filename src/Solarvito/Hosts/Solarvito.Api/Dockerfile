FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "src/Solarvito/Hosts/Solarvito.Api/Solarvito.Api.csproj"
RUN dotnet publish "src/Solarvito/Hosts/Solarvito.Api/Solarvito.Api.csproj" -c Release -o /out /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /out
COPY --from=build /out .
ENTRYPOINT ["dotnet", "Solarvito.Api.dll"]
