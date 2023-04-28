FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /src
COPY . .
WORKDIR /src/host/Mc2.CrudTest.HttpApi.Host
RUN dotnet restore -nowarn:msb3202,nu1503
RUN dotnet build --no-restore -c Release -o /app

FROM build AS publish
RUN dotnet publish "Mc2.CrudTest.HttpApi.Host.csproj" --no-restore -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Mc2.CrudTest.HttpApi.Host.dll"]
