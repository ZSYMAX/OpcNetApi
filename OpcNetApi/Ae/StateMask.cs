using System;

namespace Opc.Ae
{
    [Flags]
    public enum StateMask
    {
        Name = 1,
        ClientHandle = 2,
        Active = 4,
        BufferTime = 8,
        MaxSize = 16,
        KeepAlive = 32,
        All = 65535
    }
}