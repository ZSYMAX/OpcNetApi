using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Comn
{
    [Guid("13486D50-4821-11D2-A494-3CB306C10000")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IOPCServerList
    {
        void EnumClassesOfCategories([MarshalAs(UnmanagedType.I4)] int cImplemented, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStruct, SizeParamIndex = 0)] Guid[] rgcatidImpl,
            [MarshalAs(UnmanagedType.I4)] int cRequired, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStruct, SizeParamIndex = 2)] Guid[] rgcatidReq, [MarshalAs(UnmanagedType.IUnknown)] out object ppenumClsid);

        void GetClassDetails(ref Guid clsid, [MarshalAs(UnmanagedType.LPWStr)] out string ppszProgID, [MarshalAs(UnmanagedType.LPWStr)] out string ppszUserType);

        void CLSIDFromProgID([MarshalAs(UnmanagedType.LPWStr)] string szProgId, out Guid clsid);
    }
}