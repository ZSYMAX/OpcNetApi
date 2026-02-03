using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Da
{
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("39c13a71-011e-11d0-9675-0020afd8adb3")]
    [ComImport]
    public interface IOPCAsyncIO2
    {
        void Read([MarshalAs(UnmanagedType.I4)] int dwCount, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4, SizeParamIndex = 0)] int[] phServer, [MarshalAs(UnmanagedType.I4)] int dwTransactionID,
            [MarshalAs(UnmanagedType.I4)] out int pdwCancelID, out IntPtr ppErrors);

        void Write([MarshalAs(UnmanagedType.I4)] int dwCount, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4, SizeParamIndex = 0)] int[] phServer,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct, SizeParamIndex = 0)] object[] pItemValues, [MarshalAs(UnmanagedType.I4)] int dwTransactionID, [MarshalAs(UnmanagedType.I4)] out int pdwCancelID,
            out IntPtr ppErrors);

        void Refresh2(OPCDATASOURCE dwSource, [MarshalAs(UnmanagedType.I4)] int dwTransactionID, [MarshalAs(UnmanagedType.I4)] out int pdwCancelID);

        void Cancel2([MarshalAs(UnmanagedType.I4)] int dwCancelID);

        void SetEnable([MarshalAs(UnmanagedType.I4)] int bEnable);

        void GetEnable([MarshalAs(UnmanagedType.I4)] out int pbEnable);
    }
}