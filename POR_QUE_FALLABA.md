# Por Que Fallaba

La API no estaba fallando por variables ni por compilacion.
El proyecto compilaba bien, pero ASP.NET Core no estaba publicando endpoints MVC.

## Sintomas

- `dotnet build` terminaba sin errores.
- Swagger devolvia `"paths": { }`.
- `GET /api/metric` respondia `404 Not Found`.

## Causa

La app dependia de `AddControllers()` y `MapControllers()`, pero en esta configuracion no se estaba descubriendo ningun controller en runtime.
Por eso `MetricController` existia en el codigo, pero no habia rutas activas cuando la app arrancaba.

## Solucion Aplicada

- Se agrego `AddEndpointsApiExplorer()` para que Swagger publique endpoints.
- Se expuso `GET /api/metric` directamente desde `Program.cs` con Minimal API.
- Despues de eso, la API paso a responder `200 OK` y Swagger mostro la ruta.

## Nota

El motivo exacto por el que MVC no descubria `MetricController` sigue pendiente si mas adelante quieres volver a usar controllers en vez de Minimal API.