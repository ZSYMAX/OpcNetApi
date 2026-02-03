using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Batch
{
    [Guid("8BB4ED50-B314-11d3-B3EA-00C04F8ECEAA")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IOPCBatchServer
    {
        void GetDelimiter([MarshalAs(UnmanagedType.LPWStr)] [Out] string pszDelimiter);

        void CreateEnumerator(ref Guid riid, [MarshalAs(UnmanagedType.IUnknown, IidParameterIndex = 0)] out object ppUnk);
    }
}