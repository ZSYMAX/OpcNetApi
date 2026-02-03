using System;

namespace Opc.Da
{
    public delegate void WriteCompleteEventHandler(object requestHandle, IdentifiedResult[] results);
}