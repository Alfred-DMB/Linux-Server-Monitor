using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
namespace ServerMonitor;
public class MonitorService
{
public double GetRamUsage()
{
   var memoryStats = File.ReadLines("/proc/meminfo")
       .Select(line => line.Split(':', 2))
       .Where(parts => parts.Length == 2)
       .ToDictionary(parts => parts[0], parts => long.Parse(parts[1].Trim().Split()[0]));

   if (!memoryStats.TryGetValue("MemTotal", out var memTotal) ||
       !memoryStats.TryGetValue("MemAvailable", out var memAvailable))
   {
       throw new InvalidOperationException("No se pudieron leer MemTotal y MemAvailable desde /proc/meminfo.");
   }

   var memUsed = memTotal - memAvailable;
   var percentage = (memUsed * 100.0) / memTotal;
   return Math.Round(percentage, 2);
  }


private long[] LeerCpu()
{
    var linea = File.ReadAllLines("/proc/stat")[0];
    return linea.Split(' ').Skip(1)
        .Where(x => !string.IsNullOrWhiteSpace(x))
        .Select(long.Parse)
        .ToArray();
}

public double GetCpuPercent()
{
var firstSample = LeerCpu();
long firstTotal = firstSample.Sum();
long firstIdle = firstSample[3];

Thread.Sleep(1000);

var secondSample = LeerCpu();
long secondTotal = secondSample.Sum();
long secondIdle = secondSample[3];

long totalDiff = secondTotal - firstTotal;
long idleDiff = secondIdle - firstIdle;
long usedTicks = totalDiff - idleDiff;
double percentage = (usedTicks * 100.0) / totalDiff;
return Math.Round(percentage, 2);
  }

public double GetDiskUsage()
    {
        var disk = new DriveInfo("/");
        long usedBytes = disk.TotalSize - disk.AvailableFreeSpace;
        double percentage = (usedBytes * 100.0) / disk.TotalSize;
        return Math.Round(percentage, 2);
    }
public int GetProcess()
    {
        var directories = new DirectoryInfo("/proc").GetDirectories();
        int processCount = directories.Count(d => int.TryParse(d.Name, out _));
        return processCount;
    }
}   