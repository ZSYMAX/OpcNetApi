using System;
using System.Runtime.InteropServices;
using OpcRcw.Comn;

namespace OpcRcw.Ae
{
    [Guid("65168857-5783-11D1-84A0-00608CB8A7E9")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IOPCEventAreaBrowser
    {
        void ChangeBrowsePosition(OPCAEBROWSEDIRECTION dwBrowseDirection, [MarshalAs(UnmanagedType.LPWStr)] string szString);

        void BrowseOPCAreas(OPCAEBROWSETYPE dwBrowseFilterType, [MarshalAs(UnmanagedType.LPWStr)] string szFilterCriteria, out IEnumString ppIEnumString);

        void GetQualifiedAreaName([MarshalAs(UnmanagedType.LPWStr)] string szAreaName, [MarshalAs(UnmanagedType.LPWStr)] out string pszQualifiedAreaName);

        void GetQualifiedSourceName([MarshalAs(UnmanagedType.LPWStr)] string szSourceName, [MarshalAs(UnmanagedType.LPWStr)] out string pszQualifiedSourceName);
    }
}