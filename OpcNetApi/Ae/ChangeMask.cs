using System;

namespace Opc.Ae
{
    [Flags]
    public enum ChangeMask
    {
        ActiveState = 1,
        AcknowledgeState = 2,
        EnableState = 4,
        Quality = 8,
        Severity = 16,
        SubCondition = 32,
        Message = 64,
        Attribute = 128
    }
}