# Imagen base con el SDK de .NET 9 (actualmente preview)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia archivos y restaura paquetes
COPY . .
RUN dotnet restore "./SmartEdu.Demy.Platform.API/SmartEdu.Demy.Platform.API.csproj"

# Publicar la aplicación
RUN dotnet publish "./SmartEdu.Demy.Platform.API/SmartEdu.Demy.Platform.API.csproj" -c Release -o /app/publish

# Imagen de runtime para producción
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Railway usa la variable PORT
ENV ASPNETCORE_URLS=http://+:${PORT}
EXPOSE 8080

ENTRYPOINT ["dotnet", "SmartEdu.Demy.Platform.API.dll"]
