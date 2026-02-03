using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Hda
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct OPCHDA_ANNOTATION
    {
        [MarshalAs(UnmanagedType.I4)]
        public int hClient;

        [MarshalAs(UnmanagedType.I4)]
        public int dwNumValues;

        public IntPtr ftTimeStamps;

        public IntPtr szAnnotation;

        public IntPtr ftAnnotationTime;

        public IntPtr szUser;
    }
}