# Why It Failed

The API was not failing because of variables or compilation errors.
The project built correctly, but ASP.NET Core was not exposing the MVC endpoints at runtime.

## Symptoms

- `dotnet build` completed without errors.
- Swagger returned an empty `"paths": { }` section.
- `GET /api/metric` responded with `404 Not Found`.

## Root Cause

The application relied on `AddControllers()` and `MapControllers()`, but in this setup no controller was being discovered at runtime.
As a result, `MetricController` existed in the codebase, but no active route was available when the app started.

## Applied Fix

- Added `AddEndpointsApiExplorer()` so Swagger could publish the endpoints.
- Exposed `GET /api/metric` directly from `Program.cs` using Minimal API.
- After that change, the API returned `200 OK` and Swagger displayed the route correctly.

## Note

The exact reason MVC was not discovering `MetricController` is still unresolved if the project later moves back to controllers instead of Minimal API.