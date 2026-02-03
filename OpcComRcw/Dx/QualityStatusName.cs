using System;

namespace OpcRcw.Dx
{
    public static class QualityStatusName
    {
        public const string OPCDX_QUALITY_BAD = "bad";

        public const string OPCDX_QUALITY_BAD_CONFIG_ERROR = "badConfigurationError";

        public const string OPCDX_QUALITY_BAD_NOT_CONNECTED = "badNotConnected";

        public const string OPCDX_QUALITY_BAD_DEVICE_FAILURE = "badDeviceFailure";

        public const string OPCDX_QUALITY_BAD_SENSOR_FAILURE = "badSensorFailure";

        public const string OPCDX_QUALITY_BAD_LAST_KNOWN_VALUE = "badLastKnownValue";

        public const string OPCDX_QUALITY_BAD_COMM_FAILURE = "badCommFailure";

        public const string OPCDX_QUALITY_BAD_OUT_OF_SERVICE = "badOutOfService";

        public const string OPCDX_QUALITY_UNCERTAIN = "uncertain";

        public const string OPCDX_QUALITY_UNCERTAIN_LAST_USABLE_VALUE = "uncertainLastUsableValue";

        public const string OPCDX_QUALITY_UNCERTAIN_SENSOR_NOT_ACCURATE = "uncertainSensorNotAccurate";

        public const string OPCDX_QUALITY_UNCERTAIN_EU_EXCEEDED = "uncertainEUExceeded";

        public const string OPCDX_QUALITY_UNCERTAIN_SUB_NORMAL = "uncertainSubNormal";

        public const string OPCDX_QUALITY_GOOD = "good";

        public const string OPCDX_QUALITY_GOOD_LOCAL_OVERRIDE = "goodLocalOverride";
    }
}