using System;

namespace OpcRcw.Dx
{
    public static class ConnectionStateName
    {
        public const string OPCDX_CONNECTION_STATE_INITIALIZING = "initializing";

        public const string OPCDX_CONNECTION_STATE_OPERATIONAL = "operational";

        public const string OPCDX_CONNECTION_STATE_DEACTIVATED = "deactivated";

        public const string OPCDX_CONNECTION_STATE_SOURCE_SERVER_NOT_CONNECTED = "sourceServerNotConnected";

        public const string OPCDX_CONNECTION_STATE_SUBSCRIPTION_FAILED = "subscriptionFailed";

        public const string OPCDX_CONNECTION_STATE_TARGET_ITEM_NOT_FOUND = "targetItemNotFound";
    }
}