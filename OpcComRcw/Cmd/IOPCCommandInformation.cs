using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Cmd
{
    [Guid("3104B525-2016-442d-9696-1275DE978778")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IOPCCommandInformation
    {
        void QueryCapabilities([MarshalAs(UnmanagedType.R8)] out double pdblMaxStorageTime, [MarshalAs(UnmanagedType.I4)] out int pbSupportsEventFilter);

        void QueryComands([MarshalAs(UnmanagedType.I4)] out int pdwCount, out IntPtr ppNamespaces);

        void BrowseCommandTargets([MarshalAs(UnmanagedType.LPWStr)] string szTargetID, [MarshalAs(UnmanagedType.LPWStr)] string szNamespaceUri, OpcCmdBrowseFilter eBrowseFilter, [MarshalAs(UnmanagedType.I4)] out int pdwCount,
            out IntPtr ppTargets);

        void GetCommandDescription([MarshalAs(UnmanagedType.LPWStr)] string szCommandName, [MarshalAs(UnmanagedType.LPWStr)] string szNamespaceUri, out OpcCmdCommandDescription pDescription);
    }
}