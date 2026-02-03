using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Cmd
{
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("3104B526-2016-442d-9696-1275DE978778")]
    [ComImport]
    public interface IOPCCommandExecution
    {
        void SyncInvoke([MarshalAs(UnmanagedType.LPWStr)] string szCommandName, [MarshalAs(UnmanagedType.LPWStr)] string szNamespaceUri, [MarshalAs(UnmanagedType.LPWStr)] string szTargetID, [MarshalAs(UnmanagedType.I4)] int dwNoOfArguments,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStruct, SizeParamIndex = 3)] OpcCmdArgument[] pArguments, [MarshalAs(UnmanagedType.I4)] int dwNoOfFilters,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 5)] string[] pszFilters, [MarshalAs(UnmanagedType.I4)] out int pdwNoOfEvents, out IntPtr ppEvents);

        void AsyncInvoke([MarshalAs(UnmanagedType.LPWStr)] string szCommandName, [MarshalAs(UnmanagedType.LPWStr)] string szNamespaceUri, [MarshalAs(UnmanagedType.LPWStr)] string szTargetID,
            [MarshalAs(UnmanagedType.I4)] int dwNoOfArguments, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStruct, SizeParamIndex = 3)] OpcCmdArgument[] pArguments, [MarshalAs(UnmanagedType.I4)] int dwNoOfFilters,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 5)] string[] pszFilters, IOPCComandCallback ipCallback, [MarshalAs(UnmanagedType.I4)] int dwUpdateFrequency,
            [MarshalAs(UnmanagedType.I4)] int dwKeepAliveTime, [MarshalAs(UnmanagedType.LPWStr)] out string pszInvokeUUID, [MarshalAs(UnmanagedType.I4)] out int pdwRevisedUpdateFrequency);

        void Connect([MarshalAs(UnmanagedType.LPWStr)] string szInvokeUUID, IOPCComandCallback ipCallback, [MarshalAs(UnmanagedType.I4)] int dwUpdateFrequency, [MarshalAs(UnmanagedType.I4)] int dwKeepAliveTime,
            [MarshalAs(UnmanagedType.I4)] out int pdwRevisedUpdateFrequency);

        void Disconnect([MarshalAs(UnmanagedType.LPWStr)] string szInvokeUUID);

        void QueryState([MarshalAs(UnmanagedType.LPWStr)] string szInvokeUUID, [MarshalAs(UnmanagedType.I4)] int dwWaitTime, [MarshalAs(UnmanagedType.I4)] out int pdwNoOfEvents, out IntPtr ppEvents,
            [MarshalAs(UnmanagedType.I4)] out int pdwNoOfPermittedControls, out IntPtr ppszPermittedControls, [MarshalAs(UnmanagedType.I4)] out int pbNoStateChange);

        void Control([MarshalAs(UnmanagedType.LPWStr)] string szInvokeUUID, [MarshalAs(UnmanagedType.LPWStr)] string szControl);
    }
}