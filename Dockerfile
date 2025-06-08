FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["APDB_Kolokwium_template.csproj", "./"]
RUN dotnet restore "APDB_Kolokwium_template.csproj"

COPY . .
RUN dotnet build "APDB_Kolokwium_template.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "APDB_Kolokwium_template.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY --from=publish /app/publish .
COPY --from=build /src .

RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

ENTRYPOINT ["dotnet", "APDB_Kolokwium_template.dll"]