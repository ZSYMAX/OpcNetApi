using System;

namespace OpcRcw.Dx
{
    public enum ServerState
    {
        ServerState_RUNNING = 1,
        ServerState_FAILED,
        ServerState_NOCONFIG,
        ServerState_SUSPENDED,
        ServerState_TEST,
        ServerState_COMM_FAULT,
        ServerState_UNKNOWN
    }
}