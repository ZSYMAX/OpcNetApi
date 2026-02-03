using System;

namespace OpcRcw.Ae
{
    public enum OPCEVENTSERVERSTATE
    {
        OPCAE_STATUS_RUNNING = 1,
        OPCAE_STATUS_FAILED,
        OPCAE_STATUS_NOCONFIG,
        OPCAE_STATUS_SUSPENDED,
        OPCAE_STATUS_TEST,
        OPCAE_STATUS_COMM_FAULT
    }
}