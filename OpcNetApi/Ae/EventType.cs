using System;

namespace Opc.Ae
{
    [Flags]
    public enum EventType
    {
        Simple = 1,
        Tracking = 2,
        Condition = 4,
        All = 65535
    }
}