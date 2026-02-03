using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Cmd
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct OpcCmdTargetElement
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string szLabel;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szTargetID;

        [MarshalAs(UnmanagedType.I4)]
        public int bIsTarget;

        [MarshalAs(UnmanagedType.I4)]
        public int bHasChildren;

        [MarshalAs(UnmanagedType.I4)]
        public int dwNoOfNamespaceUris;

        public IntPtr pszNamespaceUris;
    }
}