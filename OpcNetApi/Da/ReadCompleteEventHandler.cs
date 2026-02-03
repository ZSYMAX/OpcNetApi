using System;

namespace Opc.Da
{
    public delegate void ReadCompleteEventHandler(object requestHandle, ItemValueResult[] results);
}