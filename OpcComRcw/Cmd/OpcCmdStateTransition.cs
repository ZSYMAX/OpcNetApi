using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Cmd
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct OpcCmdStateTransition
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string szTransitionID;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szStartState;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szEndState;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szTriggerEvent;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szAction;

        [MarshalAs(UnmanagedType.I4)]
        public int dwReserved;
    }
}