# Comandos C-Server-Monitor

## Configurar la conexion local

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Database=LinuxMonitor;Username=postgres;Password=TU_PASSWORD"
```

## Arrancar la API

```bash
dotnet run --launch-profile http
```

## Parar todos los hosts de C-Server-Monitor

```bash
pkill -f 'ServerMonitor|dotnet run --launch-profile http|dotnet run'
```

## Verificar puertos 5052 y 7000

```bash
ss -ltnp '( sport = :5052 or sport = :7000 )'
```

## Compilar

```bash
dotnet build
```

## Aplicar migraciones a la base

```bash
dotnet ef database update
```

## Probar endpoints

```bash
curl -sS http://localhost:5052/api/metric
curl -sS http://localhost:5052/api/metric/history
curl -sS http://localhost:5052/api/alerts
```