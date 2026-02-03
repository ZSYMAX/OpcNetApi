using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Cmd
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct OpcCmdCommandDescription
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string szDescription;

        [MarshalAs(UnmanagedType.I4)]
        public int bIsGlobal;

        [MarshalAs(UnmanagedType.R8)]
        public double dblExecutionTime;

        [MarshalAs(UnmanagedType.I4)]
        public int dwNoOfEventDefinitions;

        public IntPtr pEventDefinitions;

        [MarshalAs(UnmanagedType.I4)]
        public int dwNoOfStateDefinitions;

        public IntPtr pStateDefinitions;

        [MarshalAs(UnmanagedType.I4)]
        public int dwNoOfActionDefinitions;

        public IntPtr pActionDefinitions;

        [MarshalAs(UnmanagedType.I4)]
        public int dwNoOfTransitions;

        public IntPtr pTransitions;

        [MarshalAs(UnmanagedType.I4)]
        public int dwNoOfInArguments;

        public IntPtr pInArguments;

        [MarshalAs(UnmanagedType.I4)]
        public int dwNoOfOutArguments;

        public IntPtr pOutArguments;

        [MarshalAs(UnmanagedType.I4)]
        public int dwNoOfSupportedControls;

        public IntPtr pszSupportedControls;

        [MarshalAs(UnmanagedType.I4)]
        public int dwNoOfAndDependencies;

        public IntPtr pszAndDependencies;

        [MarshalAs(UnmanagedType.I4)]
        public int dwNoOfOrDependencies;

        public IntPtr pszOrDependencies;

        [MarshalAs(UnmanagedType.I4)]
        public int dwNoOfNotDependencies;

        public IntPtr pszNotDependencies;
    }
}