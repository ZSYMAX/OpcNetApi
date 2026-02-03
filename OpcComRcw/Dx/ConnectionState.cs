using System;

namespace OpcRcw.Dx
{
    public enum ConnectionState
    {
        ConnectionState_INITIALIZING = 1,
        ConnectionState_OPERATIONAL,
        ConnectionState_DEACTIVATED,
        ConnectionState_SOURCE_SERVER_NOT_CONNECTED,
        ConnectionState_SUBSCRIPTION_FAILED,
        ConnectionState_TARGET_ITEM_NOT_FOUND
    }
}