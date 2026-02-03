using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Hda
{
    [Guid("1F1217B9-DEE0-11d2-A5E5-000086339399")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IOPCHDA_DataCallback
    {
        void OnDataChange([MarshalAs(UnmanagedType.I4)] int dwTransactionID, [MarshalAs(UnmanagedType.I4)] int hrStatus, [MarshalAs(UnmanagedType.I4)] int dwNumItems,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStruct, SizeParamIndex = 2)] OPCHDA_ITEM[] pItemValues, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4, SizeParamIndex = 2)] int[] phrErrors);

        void OnReadComplete([MarshalAs(UnmanagedType.I4)] int dwTransactionID, [MarshalAs(UnmanagedType.I4)] int hrStatus, [MarshalAs(UnmanagedType.I4)] int dwNumItems,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStruct, SizeParamIndex = 2)] OPCHDA_ITEM[] pItemValues, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4, SizeParamIndex = 2)] int[] phrErrors);

        void OnReadModifiedComplete([MarshalAs(UnmanagedType.I4)] int dwTransactionID, [MarshalAs(UnmanagedType.I4)] int hrStatus, [MarshalAs(UnmanagedType.I4)] int dwNumItems,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStruct, SizeParamIndex = 2)] OPCHDA_MODIFIEDITEM[] pItemValues,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4, SizeParamIndex = 2)] int[] phrErrors);

        void OnReadAttributeComplete([MarshalAs(UnmanagedType.I4)] int dwTransactionID, [MarshalAs(UnmanagedType.I4)] int hrStatus, [MarshalAs(UnmanagedType.I4)] int hClient, [MarshalAs(UnmanagedType.I4)] int dwNumItems,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStruct, SizeParamIndex = 3)] OPCHDA_ATTRIBUTE[] pAttributeValues,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4, SizeParamIndex = 3)] int[] phrErrors);

        void OnReadAnnotations([MarshalAs(UnmanagedType.I4)] int dwTransactionID, [MarshalAs(UnmanagedType.I4)] int hrStatus, [MarshalAs(UnmanagedType.I4)] int dwNumItems,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStruct, SizeParamIndex = 2)] OPCHDA_ANNOTATION[] pAnnotationValues,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4, SizeParamIndex = 2)] int[] phrErrors);

        void OnInsertAnnotations([MarshalAs(UnmanagedType.I4)] int dwTransactionID, [MarshalAs(UnmanagedType.I4)] int hrStatus, [MarshalAs(UnmanagedType.I4)] int dwCount,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4, SizeParamIndex = 2)] int[] phClients, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4, SizeParamIndex = 2)] int[] phrErrors);

        void OnPlayback([MarshalAs(UnmanagedType.I4)] int dwTransactionID, [MarshalAs(UnmanagedType.I4)] int hrStatus, [MarshalAs(UnmanagedType.I4)] int dwNumItems, IntPtr ppItemValues,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4, SizeParamIndex = 2)] int[] phrErrors);

        void OnUpdateComplete([MarshalAs(UnmanagedType.I4)] int dwTransactionID, [MarshalAs(UnmanagedType.I4)] int hrStatus, [MarshalAs(UnmanagedType.I4)] int dwCount,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4, SizeParamIndex = 2)] int[] phClients, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4, SizeParamIndex = 2)] int[] phrErrors);

        void OnCancelComplete([MarshalAs(UnmanagedType.I4)] int dwCancelID);
    }
}