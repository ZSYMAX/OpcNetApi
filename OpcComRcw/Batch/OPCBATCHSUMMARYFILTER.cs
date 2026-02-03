using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace OpcRcw.Batch
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct OPCBATCHSUMMARYFILTER
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string szID;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szDescription;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szOPCItemID;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szMasterRecipeID;

        [MarshalAs(UnmanagedType.R4)]
        public float fMinBatchSize;

        [MarshalAs(UnmanagedType.R4)]
        public float fMaxBatchSize;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szEU;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szExecutionState;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szExecutionMode;

        public System.Runtime.InteropServices.ComTypes.FILETIME ftMinStartTime;

        public System.Runtime.InteropServices.ComTypes.FILETIME ftMaxStartTime;

        public System.Runtime.InteropServices.ComTypes.FILETIME ftMinEndTime;

        public System.Runtime.InteropServices.ComTypes.FILETIME ftMaxEndTime;
    }
}