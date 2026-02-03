using System;
using System.Runtime.InteropServices;
using OpcRcw.Comn;

namespace OpcRcw.Hda
{
    [Guid("1F1217B1-DEE0-11d2-A5E5-000086339399")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IOPCHDA_Browser
    {
        void GetEnum(OPCHDA_BROWSETYPE dwBrowseType, out IEnumString ppIEnumString);

        void ChangeBrowsePosition(OPCHDA_BROWSEDIRECTION dwBrowseDirection, [MarshalAs(UnmanagedType.LPWStr)] string szString);

        void GetItemID([MarshalAs(UnmanagedType.LPWStr)] string szNode, [MarshalAs(UnmanagedType.LPWStr)] out string pszItemID);

        void GetBranchPosition([MarshalAs(UnmanagedType.LPWStr)] out string pszBranchPos);
    }
}