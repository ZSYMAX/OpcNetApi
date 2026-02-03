using System;

namespace OpcRcw.Da
{
    public static class Constants
    {
        public const string OPC_CATEGORY_DESCRIPTION_DA10 = "OPC Data Access Servers Version 1.0";

        public const string OPC_CATEGORY_DESCRIPTION_DA20 = "OPC Data Access Servers Version 2.0";

        public const string OPC_CATEGORY_DESCRIPTION_DA30 = "OPC Data Access Servers Version 3.0";

        public const string OPC_CATEGORY_DESCRIPTION_XMLDA10 = "OPC XML Data Access Servers Version 1.0";

        public const int OPC_READABLE = 1;

        public const int OPC_WRITEABLE = 2;

        public const int OPC_BROWSE_HASCHILDREN = 1;

        public const int OPC_BROWSE_ISITEM = 2;

        public const string OPC_TYPE_SYSTEM_OPCBINARY = "OPCBinary";

        public const string OPC_TYPE_SYSTEM_XMLSCHEMA = "XMLSchema";

        public const string OPC_CONSISTENCY_WINDOW_UNKNOWN = "Unknown";

        public const string OPC_CONSISTENCY_WINDOW_NOT_CONSISTENT = "Not Consistent";

        public const string OPC_WRITE_BEHAVIOR_BEST_EFFORT = "Best Effort";

        public const string OPC_WRITE_BEHAVIOR_ALL_OR_NOTHING = "All or Nothing";
    }
}