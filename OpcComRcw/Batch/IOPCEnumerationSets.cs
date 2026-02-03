using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Batch
{
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("a8080da3-e23e-11d2-afa7-00c04f539421")]
    [ComImport]
    public interface IOPCEnumerationSets
    {
        void QueryEnumerationSets([MarshalAs(UnmanagedType.I4)] out int pdwCount, out IntPtr ppdwEnumSetId, out IntPtr ppszEnumSetName);

        void QueryEnumeration([MarshalAs(UnmanagedType.I4)] int dwEnumSetId, [MarshalAs(UnmanagedType.I4)] int dwEnumValue, [MarshalAs(UnmanagedType.LPWStr)] out string pszEnumName);

        void QueryEnumerationList([MarshalAs(UnmanagedType.I4)] int dwEnumSetId, [MarshalAs(UnmanagedType.I4)] out int pdwCount, out IntPtr ppdwEnumValue, out IntPtr ppszEnumName);
    }
}