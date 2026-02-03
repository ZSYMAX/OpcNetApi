using System;

namespace Opc.Da
{
    public enum qualityBits
    {
        good = 192,
        goodLocalOverride = 216,
        bad = 0,
        badConfigurationError = 4,
        badNotConnected = 8,
        badDeviceFailure = 12,
        badSensorFailure = 16,
        badLastKnownValue = 20,
        badCommFailure = 24,
        badOutOfService = 28,
        badWaitingForInitialData = 32,
        uncertain = 64,
        uncertainLastUsableValue = 68,
        uncertainSensorNotAccurate = 80,
        uncertainEUExceeded = 84,
        uncertainSubNormal = 88
    }
}