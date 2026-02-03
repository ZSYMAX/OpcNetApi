using System;

namespace OpcRcw.Cmd
{
    public static class StateName
    {
        public const string OPCCMD_STATE_NAME_IDLE = "Idle";

        public const string OPCCMD_STATE_NAME_EXECUTING = "Executing";

        public const string OPCCMD_STATE_NAME_COMPLETE = "Complete";

        public const string OPCCMD_STATE_NAME_ABNORMAL_FAILURE = "AbnormalFailure";

        public const string OPCCMD_STATE_NAME_HALTED = "Halted";
    }
}