using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Ae
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct ONEVENTSTRUCT
    {
        [MarshalAs(UnmanagedType.I2)]
        public short wChangeMask;

        [MarshalAs(UnmanagedType.I2)]
        public short wNewState;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szSource;

        public FILETIME ftTime;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szMessage;

        [MarshalAs(UnmanagedType.I4)]
        public int dwEventType;

        [MarshalAs(UnmanagedType.I4)]
        public int dwEventCategory;

        [MarshalAs(UnmanagedType.I4)]
        public int dwSeverity;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szConditionName;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szSubconditionName;

        [MarshalAs(UnmanagedType.I2)]
        public short wQuality;

        [MarshalAs(UnmanagedType.I2)]
        public short wReserved;

        [MarshalAs(UnmanagedType.I4)]
        public int bAckRequired;

        public FILETIME ftActiveTime;

        [MarshalAs(UnmanagedType.I4)]
        public int dwCookie;

        [MarshalAs(UnmanagedType.I4)]
        public int dwNumEventAttrs;

        public IntPtr pEventAttributes;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szActorID;
    }
}