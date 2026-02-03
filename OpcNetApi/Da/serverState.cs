using System;

namespace Opc.Da
{
    public enum serverState
    {
        unknown,
        running,
        failed,
        noConfig,
        suspended,
        test,
        commFault
    }
}