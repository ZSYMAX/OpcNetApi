using System;

namespace OpcRcw.Dx
{
    public static class ServerStateName
    {
        public const string OPCDX_SERVER_STATE_RUNNING = "running";

        public const string OPCDX_SERVER_STATE_FAILED = "failed";

        public const string OPCDX_SERVER_STATE_NOCONFIG = "noConfig";

        public const string OPCDX_SERVER_STATE_SUSPENDED = "suspended";

        public const string OPCDX_SERVER_STATE_TEST = "test";

        public const string OPCDX_SERVER_STATE_COMM_FAULT = "commFault";

        public const string OPCDX_SERVER_STATE_UNKNOWN = "unknown";
    }
}