using System;

namespace OpcRcw
{
    public class ServerDescription
    {
        public string HostName { get; set; }

        public Guid Clsid { get; set; }

        public string ProgId { get; set; }

        public string VersionIndependentProgId { get; set; }

        public string Description { get; set; }
    }
}