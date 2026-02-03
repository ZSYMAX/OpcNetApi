using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Dx
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct DXConnection
    {
        [MarshalAs(UnmanagedType.U4)]
        public uint dwMask;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szItemPath;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szItemName;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szVersion;

        [MarshalAs(UnmanagedType.I4)]
        public int dwBrowsePathCount;

        public IntPtr pszBrowsePaths;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szName;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szDescription;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szKeyword;

        [MarshalAs(UnmanagedType.I4)]
        public int bDefaultSourceItemConnected;

        [MarshalAs(UnmanagedType.I4)]
        public int bDefaultTargetItemConnected;

        [MarshalAs(UnmanagedType.I4)]
        public int bDefaultOverridden;

        [MarshalAs(UnmanagedType.Struct)]
        public object vDefaultOverrideValue;

        [MarshalAs(UnmanagedType.Struct)]
        public object vSubstituteValue;

        [MarshalAs(UnmanagedType.I4)]
        public int bEnableSubstituteValue;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szTargetItemPath;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szTargetItemName;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szSourceServerName;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szSourceItemPath;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szSourceItemName;

        [MarshalAs(UnmanagedType.I4)]
        public int dwSourceItemQueueSize;

        [MarshalAs(UnmanagedType.I4)]
        public int dwUpdateRate;

        [MarshalAs(UnmanagedType.R4)]
        public float fltDeadBand;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szVendorData;
    }
}