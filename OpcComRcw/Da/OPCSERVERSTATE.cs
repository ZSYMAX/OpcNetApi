using System;

namespace OpcRcw.Da
{
    public enum OPCSERVERSTATE
    {
        OPC_STATUS_RUNNING = 1,
        OPC_STATUS_FAILED,
        OPC_STATUS_NOCONFIG,
        OPC_STATUS_SUSPENDED,
        OPC_STATUS_TEST,
        OPC_STATUS_COMM_FAULT
    }
}