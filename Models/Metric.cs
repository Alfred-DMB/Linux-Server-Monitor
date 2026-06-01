using System;

namespace ServerMonitor;
public class Metric
{
public double CpuInfo { get; set;}
public int Id { get; set;}
public double RamUsage { get; set;}
public double DiskUsage { get; set; }
public int ActiveProcess { get; set;}
public DateTime Timestamp { get; set;}
public int ServerId { get; set; }
}