using System;

namespace Opc.Hda
{
    [Flags]
    public enum Quality
    {
        ExtraData = 65536,
        Interpolated = 131072,
        Raw = 262144,
        Calculated = 524288,
        NoBound = 1048576,
        NoData = 2097152,
        DataLost = 4194304,
        Conversion = 8388608,
        Partial = 16777216
    }
}