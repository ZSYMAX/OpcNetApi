using System;

namespace OpcRcw.Da
{
    public static class Qualities
    {
        public const short OPC_QUALITY_MASK = 192;

        public const short OPC_STATUS_MASK = 252;

        public const short OPC_LIMIT_MASK = 3;

        public const short OPC_QUALITY_BAD = 0;

        public const short OPC_QUALITY_UNCERTAIN = 64;

        public const short OPC_QUALITY_GOOD = 192;

        public const short OPC_QUALITY_CONFIG_ERROR = 4;

        public const short OPC_QUALITY_NOT_CONNECTED = 8;

        public const short OPC_QUALITY_DEVICE_FAILURE = 12;

        public const short OPC_QUALITY_SENSOR_FAILURE = 16;

        public const short OPC_QUALITY_LAST_KNOWN = 20;

        public const short OPC_QUALITY_COMM_FAILURE = 24;

        public const short OPC_QUALITY_OUT_OF_SERVICE = 28;

        public const short OPC_QUALITY_WAITING_FOR_INITIAL_DATA = 32;

        public const short OPC_QUALITY_LAST_USABLE = 68;

        public const short OPC_QUALITY_SENSOR_CAL = 80;

        public const short OPC_QUALITY_EGU_EXCEEDED = 84;

        public const short OPC_QUALITY_SUB_NORMAL = 88;

        public const short OPC_QUALITY_LOCAL_OVERRIDE = 216;

        public const short OPC_LIMIT_OK = 0;

        public const short OPC_LIMIT_LOW = 1;

        public const short OPC_LIMIT_HIGH = 2;

        public const short OPC_LIMIT_CONST = 3;
    }
}