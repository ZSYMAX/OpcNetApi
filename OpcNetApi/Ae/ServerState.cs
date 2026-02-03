using System;

namespace Opc.Ae
{
    public enum ServerState
    {
        Unknown,
        Running,
        Failed,
        NoConfig,
        Suspended,
        Test,
        CommFault
    }
}