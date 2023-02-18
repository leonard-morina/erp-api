FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Erp.Api/Erp.Api.csproj", "src/Erp.Api/"]
RUN dotnet restore "src/Erp.Api/Erp.Api.csproj"
COPY . .
WORKDIR "/src/src/Erp.Api"
RUN dotnet build "Erp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Erp.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Erp.Api.dll"]
