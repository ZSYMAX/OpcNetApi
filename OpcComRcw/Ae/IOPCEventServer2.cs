using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Ae
{
    [Guid("71BBE88E-9564-4bcd-BCFC-71C558D94F2D")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IOPCEventServer2
    {
        void GetStatus(out IntPtr ppEventServerStatus);

        void CreateEventSubscription([MarshalAs(UnmanagedType.I4)] int bActive, [MarshalAs(UnmanagedType.I4)] int dwBufferTime, [MarshalAs(UnmanagedType.I4)] int dwMaxSize, [MarshalAs(UnmanagedType.I4)] int hClientSubscription,
            ref Guid riid, [MarshalAs(UnmanagedType.IUnknown, IidParameterIndex = 4)] out object ppUnk, [MarshalAs(UnmanagedType.I4)] out int pdwRevisedBufferTime, [MarshalAs(UnmanagedType.I4)] out int pdwRevisedMaxSize);

        void QueryAvailableFilters([MarshalAs(UnmanagedType.I4)] out int pdwFilterMask);

        void QueryEventCategories([MarshalAs(UnmanagedType.I4)] int dwEventType, [MarshalAs(UnmanagedType.I4)] out int pdwCount, out IntPtr ppdwEventCategories, out IntPtr ppszEventCategoryDescs);

        [PreserveSig]
        int QueryConditionNames([MarshalAs(UnmanagedType.I4)] int dwEventCategory, [MarshalAs(UnmanagedType.I4)] out int pdwCount, out IntPtr ppszConditionNames);

        void QuerySubConditionNames([MarshalAs(UnmanagedType.LPWStr)] string szConditionName, [MarshalAs(UnmanagedType.I4)] out int pdwCount, out IntPtr ppszSubConditionNames);

        void QuerySourceConditions([MarshalAs(UnmanagedType.LPWStr)] string szSource, [MarshalAs(UnmanagedType.I4)] out int pdwCount, out IntPtr ppszConditionNames);

        void QueryEventAttributes([MarshalAs(UnmanagedType.I4)] int dwEventCategory, [MarshalAs(UnmanagedType.I4)] out int pdwCount, out IntPtr ppdwAttrIDs, out IntPtr ppszAttrDescs, out IntPtr ppvtAttrTypes);

        void TranslateToItemIDs([MarshalAs(UnmanagedType.LPWStr)] string szSource, [MarshalAs(UnmanagedType.I4)] int dwEventCategory, [MarshalAs(UnmanagedType.LPWStr)] string szConditionName,
            [MarshalAs(UnmanagedType.LPWStr)] string szSubconditionName, [MarshalAs(UnmanagedType.I4)] int dwCount, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4, SizeParamIndex = 4)] int[] pdwAssocAttrIDs,
            out IntPtr ppszAttrItemIDs, out IntPtr ppszNodeNames, out IntPtr ppCLSIDs);

        void GetConditionState([MarshalAs(UnmanagedType.LPWStr)] string szSource, [MarshalAs(UnmanagedType.LPWStr)] string szConditionName, [MarshalAs(UnmanagedType.I4)] int dwNumEventAttrs,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4, SizeParamIndex = 2)] int[] pdwAttributeIDs, out IntPtr ppConditionState);

        void EnableConditionByArea([MarshalAs(UnmanagedType.I4)] int dwNumAreas, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 0)] string[] pszAreas);

        void EnableConditionBySource([MarshalAs(UnmanagedType.I4)] int dwNumSources, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 0)] string[] pszSources);

        void DisableConditionByArea([MarshalAs(UnmanagedType.I4)] int dwNumAreas, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 0)] string[] pszAreas);

        void DisableConditionBySource([MarshalAs(UnmanagedType.I4)] int dwNumSources, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 0)] string[] pszSources);

        void AckCondition([MarshalAs(UnmanagedType.I4)] int dwCount, [MarshalAs(UnmanagedType.LPWStr)] string szAcknowledgerID, [MarshalAs(UnmanagedType.LPWStr)] string szComment,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 0)] string[] pszSource,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 0)] string[] szConditionName,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStruct, SizeParamIndex = 0)] FILETIME[] pftActiveTime, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4, SizeParamIndex = 0)] int[] pdwCookie,
            out IntPtr ppErrors);

        void CreateAreaBrowser(ref Guid riid, [MarshalAs(UnmanagedType.IUnknown, IidParameterIndex = 0)] out object ppUnk);

        void EnableConditionByArea2([MarshalAs(UnmanagedType.I4)] int dwNumAreas, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 0)] string[] pszAreas, out IntPtr ppErrors);

        void EnableConditionBySource2([MarshalAs(UnmanagedType.I4)] int dwNumSources, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 0)] string[] pszSources, out IntPtr ppErrors);

        void DisableConditionByArea2([MarshalAs(UnmanagedType.I4)] int dwNumAreas, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 0)] string[] pszAreas, out IntPtr ppErrors);

        void DisableConditionBySource2([MarshalAs(UnmanagedType.I4)] int dwNumSources, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 0)] string[] pszSources, out IntPtr ppErrors);

        void GetEnableStateByArea([MarshalAs(UnmanagedType.I4)] int dwNumAreas, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 0)] string[] pszAreas, out IntPtr pbEnabled,
            out IntPtr pbEffectivelyEnabled, out IntPtr ppErrors);

        void GetEnableStateBySource([MarshalAs(UnmanagedType.I4)] int dwNumSources, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 0)] string[] pszSources, out IntPtr pbEnabled,
            out IntPtr pbEffectivelyEnabled, out IntPtr ppErrors);
    }
}