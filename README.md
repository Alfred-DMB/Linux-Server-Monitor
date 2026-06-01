# C-Server-Monitor

ASP.NET Core API for collecting Linux system metrics and storing alerts in PostgreSQL.

## Project Goal

This project focuses on a small and readable monitoring API for Linux environments. Instead of introducing layers and abstractions too early, it favors a straightforward implementation that is easy to inspect, explain, and extend.

The simplicity is intentional. For a project of this size, keeping the code direct makes the monitoring flow easier to validate, debug, and evolve.

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

## Design Approach

- simple code over premature abstraction
- explicit request flow instead of hidden framework magic
- direct access to Linux system data through `/proc` and `DriveInfo`
- persistence kept close to the API flow for clarity
- thresholds defined in code to keep the alerting behavior easy to understand

## How It Works

When `GET /api/metric` is called, the API performs the full monitoring cycle in one request:

1. `MonitorService` reads Linux system information.
2. A `Metric` entity is created with CPU, RAM, disk, and process data.
3. The metric is stored in PostgreSQL.
4. Threshold rules are evaluated.
5. If a threshold is exceeded, one or more `Alertador` records are created.
6. The metric is returned in the HTTP response.

The additional endpoints expose stored data:

- `GET /api/metric/history` returns saved metric history.
- `GET /api/alerts` returns stored alerts ordered by creation date.

## Code Structure

- `Program.cs`: application startup, dependency injection, and HTTP endpoints
- `Services/MonitorService.cs`: Linux metric collection logic
- `Models/`: entity classes for metrics, servers, and alerts
- `Data/AppDbContext.cs`: Entity Framework database context
- `Migrations/`: versioned database schema history

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
- The codebase is intentionally small and direct; the goal is clarity first, then incremental improvement if the project grows.