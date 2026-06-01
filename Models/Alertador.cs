using System;

namespace ServerMonitor;
public class Alertador
{
public int Id { get; set; }
public string Message { get; set; } = "";
public string Level { get; set; } = "Info";
public string Tipo { get; set; } = "General";
public double Valor { get; set; }
public bool IsActive { get; set; } = true;
public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}