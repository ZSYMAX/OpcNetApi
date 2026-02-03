using System;

namespace Opc.Ae
{
    [Flags]
    public enum FilterType
    {
        Event = 1,
        Category = 2,
        Severity = 4,
        Area = 8,
        Source = 16,
        All = 65535
    }
}