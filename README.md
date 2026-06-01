# C-Server-Monitor

API en ASP.NET Core para capturar metricas del sistema Linux y registrar alertas en PostgreSQL.

Repositorio: https://github.com/Alfred-DMB/C-Server-Monitor

## Incluye

- captura de CPU, RAM, disco y procesos activos
- guardado de metricas en PostgreSQL con Entity Framework Core
- creacion automatica de alertas por umbrales
- endpoints para ver metricas historicas y alertas
- Swagger en entorno de desarrollo

## Stack

- .NET 8
- ASP.NET Core Minimal API
- Entity Framework Core
- PostgreSQL

## Configuracion local

El proyecto ya no guarda la cadena de conexion dentro del codigo. Configurala con user secrets:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Database=LinuxMonitor;Username=postgres;Password=TU_PASSWORD"
```

Tambien puedes usar una variable de entorno:

```bash
export ConnectionStrings__DefaultConnection="Host=localhost;Database=LinuxMonitor;Username=postgres;Password=TU_PASSWORD"
```

## Ejecutar

```bash
git clone https://github.com/Alfred-DMB/C-Server-Monitor.git
cd C-Server-Monitor
dotnet restore
dotnet ef database update
dotnet run --launch-profile http
```

## Endpoints

- `GET /api/metric`
- `GET /api/metric/history`
- `GET /api/alerts`

## Notas

- Swagger queda disponible en `/swagger` cuando `ASPNETCORE_ENVIRONMENT=Development`.
- Los comandos rapidos del proyecto estan en `COMANDOS.md`.
- El nombre del repo en GitHub es `C-Server-Monitor`, aunque el proyecto .NET interno sigue usando `ServerMonitor`.