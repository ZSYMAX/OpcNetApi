using System;

namespace Opc.Da
{
    [Flags]
    public enum StateMask
    {
        Name = 1,
        ClientHandle = 2,
        Locale = 4,
        Active = 8,
        UpdateRate = 16,
        KeepAlive = 32,
        ReqType = 64,
        Deadband = 128,
        SamplingRate = 256,
        EnableBuffering = 512,
        All = 65535
    }
}