using System;

namespace OpcRcw.Cmd
{
    public static class EventName
    {
        public const string OPCCMD_EVENT_NAME_INVOKE = "Invoke";

        public const string OPCCMD_EVENT_NAME_FINISHED = "Finished";

        public const string OPCCMD_EVENT_NAME_ABORTED = "Aborted";

        public const string OPCCMD_EVENT_NAME_RESET = "Reset";

        public const string OPCCMD_EVENT_NAME_HALTED = "Halted";

        public const string OPCCMD_EVENT_NAME_RESUMED = "Resumed";

        public const string OPCCMD_EVENT_NAME_CANCELLED = "Cancelled";
    }
}