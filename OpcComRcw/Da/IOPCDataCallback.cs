using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Da
{
    [Guid("39c13a70-011e-11d0-9675-0020afd8adb3")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IOPCDataCallback
    {
        void OnDataChange([MarshalAs(UnmanagedType.I4)] int dwTransid, [MarshalAs(UnmanagedType.I4)] int hGroup, [MarshalAs(UnmanagedType.I4)] int hrMasterquality, [MarshalAs(UnmanagedType.I4)] int hrMastererror,
            [MarshalAs(UnmanagedType.I4)] int dwCount, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4, SizeParamIndex = 4)] int[] phClientItems,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct, SizeParamIndex = 4)] object[] pvValues, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I2, SizeParamIndex = 4)] short[] pwQualities,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStruct, SizeParamIndex = 4)] FILETIME[] pftTimeStamps, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4, SizeParamIndex = 4)] int[] pErrors);

        void OnReadComplete([MarshalAs(UnmanagedType.I4)] int dwTransid, [MarshalAs(UnmanagedType.I4)] int hGroup, [MarshalAs(UnmanagedType.I4)] int hrMasterquality, [MarshalAs(UnmanagedType.I4)] int hrMastererror,
            [MarshalAs(UnmanagedType.I4)] int dwCount, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4, SizeParamIndex = 4)] int[] phClientItems,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct, SizeParamIndex = 4)] object[] pvValues, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I2, SizeParamIndex = 4)] short[] pwQualities,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStruct, SizeParamIndex = 4)] FILETIME[] pftTimeStamps, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4, SizeParamIndex = 4)] int[] pErrors);

        void OnWriteComplete([MarshalAs(UnmanagedType.I4)] int dwTransid, [MarshalAs(UnmanagedType.I4)] int hGroup, [MarshalAs(UnmanagedType.I4)] int hrMastererr, [MarshalAs(UnmanagedType.I4)] int dwCount,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4, SizeParamIndex = 3)] int[] pClienthandles, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4, SizeParamIndex = 3)] int[] pErrors);

        void OnCancelComplete([MarshalAs(UnmanagedType.I4)] int dwTransid, [MarshalAs(UnmanagedType.I4)] int hGroup);
    }
}