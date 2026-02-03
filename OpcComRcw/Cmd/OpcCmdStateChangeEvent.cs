using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace OpcRcw.Cmd
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct OpcCmdStateChangeEvent
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string szEventName;

        [MarshalAs(UnmanagedType.I4)]
        public int dwReserved;

        public FILETIME ftEventTime;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szEventData;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szOldState;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szNewState;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szStateData;

        [MarshalAs(UnmanagedType.I4)]
        public int dwNoOfInArguments;

        public IntPtr pInArguments;

        [MarshalAs(UnmanagedType.I4)]
        public int dwNoOfOutArguments;

        public IntPtr pOutArguments;
    }
}