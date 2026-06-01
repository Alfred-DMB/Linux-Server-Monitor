using System;

namespace ServerMonitor;
    public class Server
    {
    public int Id { get; set; }
    public string Name { get; set;} = "";
    public string IpAddress { get; set;} = "";
    public DateTime RegistrationData { get; set;}
    }