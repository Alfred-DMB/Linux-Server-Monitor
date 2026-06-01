# C-Server-Monitor

ASP.NET Core API for collecting Linux system metrics and storing alerts in PostgreSQL.

## Features

- CPU, RAM, disk, and active process monitoring
- metric persistence with Entity Framework Core and PostgreSQL
- automatic alert creation based on thresholds
- endpoints for metric history and alerts
- Swagger enabled in development

## Stack

- .NET 8
- ASP.NET Core Minimal API
- Entity Framework Core
- PostgreSQL

## Local Setup

The project no longer stores the connection string in source code. Configure it with user secrets:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Database=LinuxMonitor;Username=postgres;Password=TU_PASSWORD"
```

You can also use an environment variable:

```bash
export ConnectionStrings__DefaultConnection="Host=localhost;Database=LinuxMonitor;Username=postgres;Password=TU_PASSWORD"
```

## Run

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

## Notes

- Swagger is available at `/swagger` when `ASPNETCORE_ENVIRONMENT=Development`.
- Quick project commands are listed in `COMANDOS.md`.
- The GitHub repository is named `C-Server-Monitor`, while the internal .NET project name is still `ServerMonitor`.