using System;
using Newtonsoft.Json;

namespace DeadCellsMultiplayer
{
    [Serializable]
    public class ModConfig
    {
        public bool EnableMultiplayer { get; set; } = false;
        public bool IsHost { get; set; } = true;
        public string HostAddress { get; set; } = "127.0.0.1";
        public int Port { get; set; } = 8052;
        public float UpdateInterval { get; set; } = 0.1f;
    }
}