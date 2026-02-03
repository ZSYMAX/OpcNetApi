using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Hda
{
    [Guid("1F1217B4-DEE0-11d2-A5E5-000086339399")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IOPCHDA_SyncAnnotations
    {
        void QueryCapabilities(out OPCHDA_ANNOTATIONCAPABILITIES pCapabilities);

        void Read(ref OPCHDA_TIME htStartTime, ref OPCHDA_TIME htEndTime, [MarshalAs(UnmanagedType.I4)] int dwNumItems, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4, SizeParamIndex = 2)] int[] phServer,
            out IntPtr ppAnnotationValues, out IntPtr ppErrors);

        void Insert([MarshalAs(UnmanagedType.I4)] int dwNumItems, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4, SizeParamIndex = 0)] int[] phServer,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStruct, SizeParamIndex = 0)] OPCHDA_FILETIME[] ftTimeStamps,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStruct, SizeParamIndex = 0)] OPCHDA_ANNOTATION[] pAnnotationValues, out IntPtr ppErrors);
    }
}