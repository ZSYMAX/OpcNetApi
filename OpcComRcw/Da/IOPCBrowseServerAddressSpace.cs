using System;
using System.Runtime.InteropServices;
using OpcRcw.Comn;

namespace OpcRcw.Da
{
    [Guid("39c13a4f-011e-11d0-9675-0020afd8adb3")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IOPCBrowseServerAddressSpace
    {
        void QueryOrganization(out OPCNAMESPACETYPE pNameSpaceType);

        void ChangeBrowsePosition(OPCBROWSEDIRECTION dwBrowseDirection, [MarshalAs(UnmanagedType.LPWStr)] string szString);

        void BrowseOPCItemIDs(OPCBROWSETYPE dwBrowseFilterType, [MarshalAs(UnmanagedType.LPWStr)] string szFilterCriteria, [MarshalAs(UnmanagedType.I2)] short vtDataTypeFilter, [MarshalAs(UnmanagedType.I4)] int dwAccessRightsFilter,
            out IEnumString ppIEnumString);

        void GetItemID([MarshalAs(UnmanagedType.LPWStr)] string szItemDataID, [MarshalAs(UnmanagedType.LPWStr)] out string szItemID);

        void BrowseAccessPaths([MarshalAs(UnmanagedType.LPWStr)] string szItemID, out IEnumString pIEnumString);
    }
}