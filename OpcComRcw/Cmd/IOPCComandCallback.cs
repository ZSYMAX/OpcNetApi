using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Cmd
{
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("3104B527-2016-442d-9696-1275DE978778")]
    [ComImport]
    public interface IOPCComandCallback
    {
        void OnStateChange([MarshalAs(UnmanagedType.I4)] int dwNoOfEvents, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStruct, SizeParamIndex = 0)] OpcCmdStateChangeEvent[] pEvents,
            [MarshalAs(UnmanagedType.I4)] int dwNoOfPermittedControls, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 2)] string pszPermittedControls,
            [MarshalAs(UnmanagedType.I4)] int bNoStateChange);
    }
}