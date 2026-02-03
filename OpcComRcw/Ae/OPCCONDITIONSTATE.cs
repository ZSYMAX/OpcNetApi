using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Ae
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct OPCCONDITIONSTATE
    {
        [MarshalAs(UnmanagedType.I2)]
        public short wState;

        [MarshalAs(UnmanagedType.I2)]
        public short wReserved1;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szActiveSubCondition;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szASCDefinition;

        [MarshalAs(UnmanagedType.I4)]
        public int dwASCSeverity;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szASCDescription;

        [MarshalAs(UnmanagedType.I2)]
        public short wQuality;

        [MarshalAs(UnmanagedType.I2)]
        public short wReserved2;

        public FILETIME ftLastAckTime;

        public FILETIME ftSubCondLastActive;

        public FILETIME ftCondLastActive;

        public FILETIME ftCondLastInactive;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szAcknowledgerID;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szComment;

        [MarshalAs(UnmanagedType.I4)]
        public int dwNumSCs;

        public IntPtr pszSCNames;

        public IntPtr pszSCDefinitions;

        public IntPtr pdwSCSeverities;

        public IntPtr pszSCDescriptions;

        public int dwNumEventAttrs;

        public IntPtr pEventAttributes;

        public IntPtr pErrors;
    }
}