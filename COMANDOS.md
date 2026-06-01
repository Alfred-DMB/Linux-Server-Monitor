# C-Server-Monitor Commands

## Configure local connection

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Database=LinuxMonitor;Username=postgres;Password=TU_PASSWORD"
```

## Start the API

```bash
dotnet run --launch-profile http
```

## Stop all C-Server-Monitor processes

```bash
pkill -f 'ServerMonitor|dotnet run --launch-profile http|dotnet run'
```

## Check ports 5052 and 7000

```bash
ss -ltnp '( sport = :5052 or sport = :7000 )'
```

## Build

```bash
dotnet build
```

## Apply database migrations

```bash
dotnet ef database update
```

## Test endpoints

```bash
curl -sS http://localhost:5052/api/metric
curl -sS http://localhost:5052/api/metric/history
curl -sS http://localhost:5052/api/alerts
```