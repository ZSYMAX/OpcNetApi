using System;

namespace OpcRcw.Dx
{
    public enum ConnectStatus
    {
        ConnectStatus_CONNECTED = 1,
        ConnectStatus_DISCONNECTED,
        ConnectStatus_CONNECTING,
        ConnectStatus_FAILED
    }
}