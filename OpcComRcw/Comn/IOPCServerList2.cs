using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Comn
{
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("9DD0B56C-AD9E-43ee-8305-487F3188BF7A")]
    [ComImport]
    public interface IOPCServerList2
    {
        void EnumClassesOfCategories([MarshalAs(UnmanagedType.I4)] int cImplemented, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStruct, SizeParamIndex = 0)] Guid[] rgcatidImpl,
            [MarshalAs(UnmanagedType.I4)] int cRequired, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStruct, SizeParamIndex = 0)] Guid[] rgcatidReq, out IOPCEnumGUID ppenumClsid);

        void GetClassDetails(ref Guid clsid, [MarshalAs(UnmanagedType.LPWStr)] out string ppszProgID, [MarshalAs(UnmanagedType.LPWStr)] out string ppszUserType, [MarshalAs(UnmanagedType.LPWStr)] out string ppszVerIndProgID);

        void CLSIDFromProgID([MarshalAs(UnmanagedType.LPWStr)] string szProgId, out Guid clsid);
    }
}