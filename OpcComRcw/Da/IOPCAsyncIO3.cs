using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Da
{
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("0967B97B-36EF-423e-B6F8-6BFF1E40D39D")]
    [ComImport]
    public interface IOPCAsyncIO3
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

        void ReadMaxAge([MarshalAs(UnmanagedType.I4)] int dwCount, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4, SizeParamIndex = 0)] int[] phServer,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4, SizeParamIndex = 0)] int[] pdwMaxAge, [MarshalAs(UnmanagedType.I4)] int dwTransactionID, [MarshalAs(UnmanagedType.I4)] out int pdwCancelID, out IntPtr ppErrors);

        void WriteVQT([MarshalAs(UnmanagedType.I4)] int dwCount, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4, SizeParamIndex = 0)] int[] phServer,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStruct, SizeParamIndex = 0)] OPCITEMVQT[] pItemVQT, [MarshalAs(UnmanagedType.I4)] int dwTransactionID, [MarshalAs(UnmanagedType.I4)] out int pdwCancelID,
            out IntPtr ppErrors);

        void RefreshMaxAge([MarshalAs(UnmanagedType.I4)] int dwMaxAge, [MarshalAs(UnmanagedType.I4)] int dwTransactionID, [MarshalAs(UnmanagedType.I4)] out int pdwCancelID);
    }
}