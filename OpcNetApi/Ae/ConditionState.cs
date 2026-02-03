using System;

namespace Opc.Ae
{
    [Flags]
    public enum ConditionState
    {
        Enabled = 1,
        Active = 2,
        Acknowledged = 4
    }
}