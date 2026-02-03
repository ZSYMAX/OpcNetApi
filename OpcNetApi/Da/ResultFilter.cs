using System;

namespace Opc.Da
{
    [Flags]
    public enum ResultFilter
    {
        ItemName = 1,
        ItemPath = 2,
        ClientHandle = 4,
        ItemTime = 8,
        ErrorText = 16,
        DiagnosticInfo = 32,
        Minimal = 9,
        All = 63
    }
}