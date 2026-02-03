using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using OpcRcw.Ae;
using OpcRcw.Da;
using OpcRcw.Hda;

namespace OpcRcw
{
    public static class Utils
    {
        //public static List<Type> RegisterComTypes(string filePath)
        //{
        //	Assembly assembly = Assembly.LoadFrom(filePath);
        //          VerifyCodebase(assembly, filePath);
        //	RegistrationServices registrationServices = new RegistrationServices();
        //	List<Type> list = new List<Type>(registrationServices.GetRegistrableTypesInAssembly(assembly));
        //	if (list.Count > 0)
        //	{
        //		if (!registrationServices.UnregisterAssembly(assembly))
        //		{
        //			throw new ApplicationException("Unregister COM Types Failed.");
        //		}
        //		if (!registrationServices.RegisterAssembly(assembly, AssemblyRegistrationFlags.SetCodeBase))
        //		{
        //			throw new ApplicationException("Register COM Types Failed.");
        //		}
        //	}
        //	return list;
        //}

        private static void VerifyCodebase(Assembly assembly, string filepath)
        {
            string text = assembly.CodeBase.ToLower();
            string text2 = filepath.Replace('\\', '/').Replace("//", "/").ToLower();
            if (!text2.StartsWith("file:///"))
            {
                text2 = "file:///" + text2;
            }

            if (text != text2)
            {
                throw new ApplicationException(string.Format("Duplicate assembly loaded. You need to restart the application.\r\n{0}\r\n{1}", text, text2));
            }
        }

        //public static List<Type> UnregisterComTypes(string filePath)
        //{
        //	Assembly assembly = Assembly.LoadFrom(filePath);
        //	Utils.VerifyCodebase(assembly, filePath);
        //	RegistrationServices registrationServices = new RegistrationServices();
        //	List<Type> result = new List<Type>(registrationServices.GetRegistrableTypesInAssembly(assembly));
        //	if (!registrationServices.UnregisterAssembly(assembly))
        //	{
        //		throw new ApplicationException("Unregister COM Types Failed.");
        //	}
        //	return result;
        //}

        public static string GetSystemMessage(int error, int localeId)
        {
            int dwLanguageId;
            if (localeId != 1024)
            {
                if (localeId == 2048)
                {
                    dwLanguageId = Utils.GetSystemDefaultLangID();
                }
                else
                {
                    dwLanguageId = (65535 & localeId);
                }
            }
            else
            {
                dwLanguageId = Utils.GetUserDefaultLangID();
            }

            IntPtr intPtr = Marshal.AllocCoTaskMem(1024);
            int num = Utils.FormatMessageW(4096, IntPtr.Zero, error, dwLanguageId, intPtr, 1023, IntPtr.Zero);
            if (num > 0)
            {
                string text = Marshal.PtrToStringUni(intPtr);
                Marshal.FreeCoTaskMem(intPtr);
                if (text != null && text.Length > 0)
                {
                    return text.Trim();
                }
            }

            return string.Format("0x{0:X8}", error);
        }

        public static string ProgIDFromCLSID(Guid clsid)
        {
            RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(string.Format("CLSID\\{{{0}}}\\ProgId", clsid));
            if (registryKey != null)
            {
                try
                {
                    return registryKey.GetValue("") as string;
                }
                finally
                {
                    registryKey.Close();
                }
            }

            return null;
        }

        public static Guid CLSIDFromProgID(string progID)
        {
            if (string.IsNullOrEmpty(progID))
            {
                return Guid.Empty;
            }

            RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(string.Format("{0}\\CLSID", progID));
            if (registryKey != null)
            {
                try
                {
                    string text = registryKey.GetValue(null) as string;
                    if (text != null)
                    {
                        return new Guid(text.Substring(1, text.Length - 2));
                    }
                }
                finally
                {
                    registryKey.Close();
                }
            }

            return Guid.Empty;
        }

        public static List<Guid> GetImplementedCategories(Guid clsid)
        {
            List<Guid> list = new List<Guid>();
            string name = string.Format("CLSID\\{{{0}}}\\Implemented Categories", clsid);
            RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(name);
            if (registryKey != null)
            {
                try
                {
                    foreach (string text in registryKey.GetSubKeyNames())
                    {
                        try
                        {
                            Guid item = new Guid(text.Substring(1, text.Length - 2));
                            list.Add(item);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                finally
                {
                    registryKey.Close();
                }
            }

            return list;
        }

        public static List<Guid> EnumClassesInCategories(params Guid[] categories)
        {
            Utils.ICatInformation catInformation = (Utils.ICatInformation)Utils.CreateLocalServer(Utils.CLSID_StdComponentCategoriesMgr);
            object obj = null;
            List<Guid> result;
            try
            {
                catInformation.EnumClassesOfCategories(1, categories, 0, null, out obj);
                Utils.IEnumGUID enumGUID = (Utils.IEnumGUID)obj;
                List<Guid> list = new List<Guid>();
                Guid[] array = new Guid[10];
                for (;;)
                {
                    int num = 0;
                    IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(Guid)) * array.Length);
                    try
                    {
                        enumGUID.Next(array.Length, intPtr, out num);
                        if (num == 0)
                        {
                            break;
                        }

                        IntPtr ptr = intPtr;
                        for (int i = 0; i < num; i++)
                        {
                            array[i] = (Guid)Marshal.PtrToStructure(ptr, typeof(Guid));
                            ptr = (IntPtr)(ptr.ToInt64() + (long)Marshal.SizeOf(typeof(Guid)));
                        }
                    }
                    finally
                    {
                        Marshal.FreeCoTaskMem(intPtr);
                    }

                    for (int j = 0; j < num; j++)
                    {
                        list.Add(array[j]);
                    }
                }

                result = list;
            }
            finally
            {
                Utils.ReleaseServer(obj);
                Utils.ReleaseServer(catInformation);
            }

            return result;
        }

        public static string GetExecutablePath(Guid clsid)
        {
            RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(string.Format("CLSID\\{{{0}}}\\LocalServer32", clsid));
            if (registryKey == null)
            {
                registryKey = Registry.ClassesRoot.OpenSubKey(string.Format("CLSID\\{{{0}}}\\InprocServer32", clsid));
            }

            if (registryKey != null)
            {
                try
                {
                    string text = registryKey.GetValue("Codebase") as string;
                    if (text == null)
                    {
                        return registryKey.GetValue(null) as string;
                    }

                    return text;
                }
                finally
                {
                    registryKey.Close();
                }
            }

            return null;
        }

        public static object CreateLocalServer(Guid clsid)
        {
            Utils.COSERVERINFO coserverinfo = default(Utils.COSERVERINFO);
            coserverinfo.pwszName = null;
            coserverinfo.pAuthInfo = IntPtr.Zero;
            coserverinfo.dwReserved1 = 0U;
            coserverinfo.dwReserved2 = 0U;
            GCHandle gchandle = GCHandle.Alloc(Utils.IID_IUnknown, GCHandleType.Pinned);
            Utils.MULTI_QI[] array = new Utils.MULTI_QI[1];
            array[0].iid = gchandle.AddrOfPinnedObject();
            array[0].pItf = null;
            array[0].hr = 0U;
            try
            {
                Utils.CoCreateInstanceEx(ref clsid, null, 5U, ref coserverinfo, 1U, array);
            }
            finally
            {
                gchandle.Free();
            }

            if (array[0].hr != 0U)
            {
                throw new ExternalException("CoCreateInstanceEx: 0x{0:X8}" + array[0].hr);
            }

            return array[0].pItf;
        }

        public static object CreateInstance(Guid clsid, string hostName, string username, string password, string domain)
        {
            Utils.ServerInfo serverInfo = new Utils.ServerInfo();
            Utils.COSERVERINFO coserverinfo = serverInfo.Allocate(hostName, username, password, domain);
            GCHandle gchandle = GCHandle.Alloc(Utils.IID_IUnknown, GCHandleType.Pinned);
            Utils.MULTI_QI[] array = new Utils.MULTI_QI[1];
            array[0].iid = gchandle.AddrOfPinnedObject();
            array[0].pItf = null;
            array[0].hr = 0U;
            try
            {
                uint dwClsCtx = 5U;
                if (!string.IsNullOrEmpty(hostName) && hostName != "localhost")
                {
                    dwClsCtx = 20U;
                }

                Utils.CoCreateInstanceEx(ref clsid, null, dwClsCtx, ref coserverinfo, 1U, array);
            }
            finally
            {
                if (gchandle.IsAllocated)
                {
                    gchandle.Free();
                }

                serverInfo.Deallocate();
            }

            if (array[0].hr != 0U)
            {
                throw Utils.CreateComException(-2147467259, "Could not create COM server '{0}' on host '{1}'. Reason: {2}.", new object[]
                {
                    clsid,
                    hostName,
                    Utils.GetSystemMessage((int)array[0].hr, 2048)
                });
            }

            return array[0].pItf;
        }

        public static void ReleaseServer(object server)
        {
            if (server != null && server.GetType().IsCOMObject)
            {
                Marshal.ReleaseComObject(server);
            }
        }

        public static void RegisterClassInCategory(Guid clsid, Guid catid)
        {
            Utils.RegisterClassInCategory(clsid, catid, null);
        }

        public static void RegisterClassInCategory(Guid clsid, Guid catid, string description)
        {
            Utils.ICatRegister catRegister = (Utils.ICatRegister)Utils.CreateLocalServer(Utils.CLSID_StdComponentCategoriesMgr);
            try
            {
                string text = null;
                try
                {
                    ((Utils.ICatInformation)catRegister).GetCategoryDesc(catid, 0, out text);
                }
                catch (Exception innerException)
                {
                    text = description;
                    if (string.IsNullOrEmpty(text))
                    {
                        if (catid == Utils.CATID_OPCDAServer20)
                        {
                            text = "OPC Data Access Servers Version 2.0";
                        }
                        else if (catid == Utils.CATID_OPCDAServer30)
                        {
                            text = "OPC Data Access Servers Version 3.0";
                        }
                        else if (catid == Utils.CATID_OPCAEServer10)
                        {
                            text = "OPC Alarm & Event Server Version 1.0";
                        }
                        else
                        {
                            if (!(catid == Utils.CATID_OPCHDAServer10))
                            {
                                throw new ApplicationException("No description for category available", innerException);
                            }

                            text = "OPC History Data Access Servers Version 1.0";
                        }
                    }

                    Utils.CATEGORYINFO categoryinfo;
                    categoryinfo.catid = catid;
                    categoryinfo.lcid = 0;
                    categoryinfo.szDescription = text;
                    catRegister.RegisterCategories(1, new Utils.CATEGORYINFO[]
                    {
                        categoryinfo
                    });
                }

                catRegister.RegisterClassImplCategories(clsid, 1, new Guid[]
                {
                    catid
                });
            }
            finally
            {
                Utils.ReleaseServer(catRegister);
            }
        }

        public static void UnregisterComServer(Guid clsid)
        {
            string name = string.Format("CLSID\\{{{0}}}\\Implemented Categories", clsid);
            RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(name);
            if (registryKey != null)
            {
                try
                {
                    foreach (string text in registryKey.GetSubKeyNames())
                    {
                        try
                        {
                            Utils.UnregisterClassInCategory(clsid, new Guid(text.Substring(1, text.Length - 2)));
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                finally
                {
                    registryKey.Close();
                }
            }

            string name2 = string.Format("CLSID\\{{{0}}}\\ProgId", clsid);
            registryKey = Registry.ClassesRoot.OpenSubKey(name2);
            if (registryKey != null)
            {
                string text2 = registryKey.GetValue(null) as string;
                registryKey.Close();
                if (!string.IsNullOrEmpty(text2))
                {
                    try
                    {
                        Registry.ClassesRoot.DeleteSubKeyTree(text2);
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            try
            {
                Registry.ClassesRoot.DeleteSubKeyTree(string.Format("CLSID\\{{{0}}}", clsid));
            }
            catch (Exception)
            {
            }
        }

        public static void UnregisterClassInCategory(Guid clsid, Guid catid)
        {
            Utils.ICatRegister catRegister = (Utils.ICatRegister)Utils.CreateLocalServer(Utils.CLSID_StdComponentCategoriesMgr);
            try
            {
                catRegister.UnRegisterClassImplCategories(clsid, 1, new Guid[]
                {
                    catid
                });
            }
            finally
            {
                Utils.ReleaseServer(catRegister);
            }
        }

        public static Exception CreateComException(Exception e)
        {
            return Utils.CreateComException(e, 0, null, new object[0]);
        }

        public static Exception CreateComException(int code, string message, params object[] args)
        {
            return Utils.CreateComException(null, code, message, args);
        }

        public static Exception CreateComException(Exception e, int code, string message, params object[] args)
        {
            if (code == 0)
            {
                if (e is COMException)
                {
                    code = ((COMException)e).ErrorCode;
                }
                else
                {
                    code = -2147467259;
                }
            }

            if (!string.IsNullOrEmpty(message))
            {
                if (args != null && args.Length > 0)
                {
                    message = string.Format(CultureInfo.CurrentUICulture, message, args);
                }
            }
            else if (e != null)
            {
                message = e.Message;
            }
            else
            {
                message = Utils.GetSystemMessage(code, CultureInfo.CurrentUICulture.LCID);
            }

            return new COMException(message, code);
        }

        [DllImport("ole32.dll")]
        private static extern void CoCreateInstanceEx(ref Guid clsid, [MarshalAs(UnmanagedType.IUnknown)] object punkOuter, uint dwClsCtx, [In] ref Utils.COSERVERINFO pServerInfo, uint dwCount, [In] [Out] Utils.MULTI_QI[] pResults);

        [DllImport("Kernel32.dll")]
        private static extern int FormatMessageW(int dwFlags, IntPtr lpSource, int dwMessageId, int dwLanguageId, IntPtr lpBuffer, int nSize, IntPtr Arguments);

        [DllImport("Kernel32.dll")]
        private static extern int GetSystemDefaultLangID();

        [DllImport("Kernel32.dll")]
        private static extern int GetUserDefaultLangID();

        [DllImport("OleAut32.dll")]
        private static extern int VariantChangeTypeEx(IntPtr pvargDest, IntPtr pvarSrc, int lcid, ushort wFlags, short vt);

        [DllImport("oleaut32.dll")]
        private static extern void VariantInit(IntPtr pVariant);

        [DllImport("oleaut32.dll")]
        public static extern void VariantClear(IntPtr pVariant);

        [DllImport("ole32.dll")]
        private static extern int CoInitializeSecurity(IntPtr pSecDesc, int cAuthSvc, Utils.SOLE_AUTHENTICATION_SERVICE[] asAuthSvc, IntPtr pReserved1, uint dwAuthnLevel, uint dwImpLevel, IntPtr pAuthList, uint dwCapabilities,
            IntPtr pReserved3);

        [DllImport("ole32.dll")]
        private static extern int CoQueryProxyBlanket([MarshalAs(UnmanagedType.IUnknown)] object pProxy, ref uint pAuthnSvc, ref uint pAuthzSvc, [MarshalAs(UnmanagedType.LPWStr)] ref string pServerPrincName, ref uint pAuthnLevel,
            ref uint pImpLevel, ref IntPtr pAuthInfo, ref uint pCapabilities);

        [DllImport("ole32.dll")]
        private static extern int CoSetProxyBlanket([MarshalAs(UnmanagedType.IUnknown)] object pProxy, uint pAuthnSvc, uint pAuthzSvc, IntPtr pServerPrincName, uint pAuthnLevel, uint pImpLevel, IntPtr pAuthInfo, uint pCapabilities);

        [DllImport("ole32.dll")]
        private static extern void CoGetClassObject([MarshalAs(UnmanagedType.LPStruct)] Guid clsid, uint dwClsContext, [In] ref Utils.COSERVERINFO pServerInfo, [MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppv);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern bool CloseHandle(IntPtr handle);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool DuplicateToken(IntPtr ExistingTokenHandle, int SECURITY_IMPERSONATION_LEVEL, ref IntPtr DuplicateTokenHandle);

        private const uint CLSCTX_INPROC_SERVER = 1U;

        private const uint CLSCTX_INPROC_HANDLER = 2U;

        private const uint CLSCTX_LOCAL_SERVER = 4U;

        private const uint CLSCTX_REMOTE_SERVER = 16U;

        private const string CATID_OPCDAServer20_Description = "OPC Data Access Servers Version 2.0";

        private const string CATID_OPCDAServer30_Description = "OPC Data Access Servers Version 3.0";

        private const string CATID_OPCAEServer10_Description = "OPC Alarm & Event Server Version 1.0";

        private const string CATID_OPCHDAServer10_Description = "OPC History Data Access Servers Version 1.0";

        private const int MAX_MESSAGE_LENGTH = 1024;

        private const uint FORMAT_MESSAGE_IGNORE_INSERTS = 512U;

        private const uint FORMAT_MESSAGE_FROM_SYSTEM = 4096U;

        public const int LOCALE_SYSTEM_DEFAULT = 2048;

        public const int LOCALE_USER_DEFAULT = 1024;

        private const int VARIANT_SIZE = 16;

        private const int DISP_E_TYPEMISMATCH = -2147352571;

        private const int DISP_E_OVERFLOW = -2147352566;

        private const int VARIANT_NOVALUEPROP = 1;

        private const int VARIANT_ALPHABOOL = 2;

        private const uint RPC_C_AUTHN_NONE = 0U;

        private const uint RPC_C_AUTHN_DCE_PRIVATE = 1U;

        private const uint RPC_C_AUTHN_DCE_PUBLIC = 2U;

        private const uint RPC_C_AUTHN_DEC_PUBLIC = 4U;

        private const uint RPC_C_AUTHN_GSS_NEGOTIATE = 9U;

        private const uint RPC_C_AUTHN_WINNT = 10U;

        private const uint RPC_C_AUTHN_GSS_SCHANNEL = 14U;

        private const uint RPC_C_AUTHN_GSS_KERBEROS = 16U;

        private const uint RPC_C_AUTHN_DPA = 17U;

        private const uint RPC_C_AUTHN_MSN = 18U;

        private const uint RPC_C_AUTHN_DIGEST = 21U;

        private const uint RPC_C_AUTHN_MQ = 100U;

        private const uint RPC_C_AUTHN_DEFAULT = 4294967295U;

        private const uint RPC_C_AUTHZ_NONE = 0U;

        private const uint RPC_C_AUTHZ_NAME = 1U;

        private const uint RPC_C_AUTHZ_DCE = 2U;

        private const uint RPC_C_AUTHZ_DEFAULT = 4294967295U;

        private const uint RPC_C_AUTHN_LEVEL_DEFAULT = 0U;

        private const uint RPC_C_AUTHN_LEVEL_NONE = 1U;

        private const uint RPC_C_AUTHN_LEVEL_CONNECT = 2U;

        private const uint RPC_C_AUTHN_LEVEL_CALL = 3U;

        private const uint RPC_C_AUTHN_LEVEL_PKT = 4U;

        private const uint RPC_C_AUTHN_LEVEL_PKT_INTEGRITY = 5U;

        private const uint RPC_C_AUTHN_LEVEL_PKT_PRIVACY = 6U;

        private const uint RPC_C_IMP_LEVEL_ANONYMOUS = 1U;

        private const uint RPC_C_IMP_LEVEL_IDENTIFY = 2U;

        private const uint RPC_C_IMP_LEVEL_IMPERSONATE = 3U;

        private const uint RPC_C_IMP_LEVEL_DELEGATE = 4U;

        private const uint EOAC_NONE = 0U;

        private const uint EOAC_MUTUAL_AUTH = 1U;

        private const uint EOAC_CLOAKING = 16U;

        private const uint EOAC_STATIC_CLOAKING = 32U;

        private const uint EOAC_DYNAMIC_CLOAKING = 64U;

        private const uint EOAC_SECURE_REFS = 2U;

        private const uint EOAC_ACCESS_CONTROL = 4U;

        private const uint EOAC_APPID = 8U;

        private const uint SEC_WINNT_AUTH_IDENTITY_ANSI = 1U;

        private const uint SEC_WINNT_AUTH_IDENTITY_UNICODE = 2U;

        private const int LOGON32_PROVIDER_DEFAULT = 0;

        private const int LOGON32_LOGON_INTERACTIVE = 2;

        private const int LOGON32_LOGON_NETWORK = 3;

        private const int SECURITY_ANONYMOUS = 0;

        private const int SECURITY_IDENTIFICATION = 1;

        private const int SECURITY_IMPERSONATION = 2;

        private const int SECURITY_DELEGATION = 3;

        public static readonly Guid CATID_OPCDAServer20 = typeof(CATID_OPCDAServer20).GUID;

        public static readonly Guid CATID_OPCDAServer30 = typeof(CATID_OPCDAServer30).GUID;

        public static readonly Guid CATID_OPCAEServer10 = typeof(CATID_OPCAEServer10).GUID;

        public static readonly Guid CATID_OPCHDAServer10 = typeof(CATID_OPCHDAServer10).GUID;

        private static readonly Guid IID_IUnknown = new Guid("00000000-0000-0000-C000-000000000046");

        private static readonly Guid CLSID_StdComponentCategoriesMgr = new Guid("0002E005-0000-0000-C000-000000000046");

        private static readonly DateTime FILETIME_BaseTime = new DateTime(1601, 1, 1);

        private static readonly IntPtr COLE_DEFAULT_PRINCIPAL = new IntPtr(-1);

        private static readonly IntPtr COLE_DEFAULT_AUTHINFO = new IntPtr(-1);

        private class ServerInfo
        {
            public Utils.COSERVERINFO Allocate(string hostName, string username, string password, string domain)
            {
                Utils.COSERVERINFO result = default(Utils.COSERVERINFO);
                result.pwszName = hostName;
                result.pAuthInfo = IntPtr.Zero;
                result.dwReserved1 = 0U;
                result.dwReserved2 = 0U;
                if (string.IsNullOrEmpty(username))
                {
                    return result;
                }

                this.m_hUserName = GCHandle.Alloc(username, GCHandleType.Pinned);
                this.m_hPassword = GCHandle.Alloc(password, GCHandleType.Pinned);
                this.m_hDomain = GCHandle.Alloc(domain, GCHandleType.Pinned);
                this.m_hIdentity = default(GCHandle);
                this.m_hIdentity = GCHandle.Alloc(new Utils.COAUTHIDENTITY
                {
                    User = this.m_hUserName.AddrOfPinnedObject(),
                    UserLength = (uint)((username != null) ? username.Length : 0),
                    Password = this.m_hPassword.AddrOfPinnedObject(),
                    PasswordLength = (uint)((password != null) ? password.Length : 0),
                    Domain = this.m_hDomain.AddrOfPinnedObject(),
                    DomainLength = (uint)((domain != null) ? domain.Length : 0),
                    Flags = 2U
                }, GCHandleType.Pinned);
                this.m_hAuthInfo = GCHandle.Alloc(new Utils.COAUTHINFO
                {
                    dwAuthnSvc = 10U,
                    dwAuthzSvc = 0U,
                    pwszServerPrincName = IntPtr.Zero,
                    dwAuthnLevel = 2U,
                    dwImpersonationLevel = 3U,
                    pAuthIdentityData = this.m_hIdentity.AddrOfPinnedObject(),
                    dwCapabilities = 0U
                }, GCHandleType.Pinned);
                result.pAuthInfo = this.m_hAuthInfo.AddrOfPinnedObject();
                return result;
            }

            public void Deallocate()
            {
                if (this.m_hUserName.IsAllocated)
                {
                    this.m_hUserName.Free();
                }

                if (this.m_hPassword.IsAllocated)
                {
                    this.m_hPassword.Free();
                }

                if (this.m_hDomain.IsAllocated)
                {
                    this.m_hDomain.Free();
                }

                if (this.m_hIdentity.IsAllocated)
                {
                    this.m_hIdentity.Free();
                }

                if (this.m_hAuthInfo.IsAllocated)
                {
                    this.m_hAuthInfo.Free();
                }
            }

            private GCHandle m_hUserName;

            private GCHandle m_hPassword;

            private GCHandle m_hDomain;

            private GCHandle m_hIdentity;

            private GCHandle m_hAuthInfo;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct MULTI_QI
        {
            public IntPtr iid;

            [MarshalAs(UnmanagedType.IUnknown)]
            public object pItf;

            public uint hr;
        }

        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("0002E000-0000-0000-C000-000000000046")]
        [ComImport]
        private interface IEnumGUID
        {
            void Next([MarshalAs(UnmanagedType.I4)] int celt, [Out] IntPtr rgelt, [MarshalAs(UnmanagedType.I4)] out int pceltFetched);

            void Skip([MarshalAs(UnmanagedType.I4)] int celt);

            void Reset();

            void Clone(out Utils.IEnumGUID ppenum);
        }

        [Guid("0002E013-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [ComImport]
        private interface ICatInformation
        {
            void EnumCategories(int lcid, [MarshalAs(UnmanagedType.Interface)] out object ppenumCategoryInfo);

            void GetCategoryDesc([MarshalAs(UnmanagedType.LPStruct)] Guid rcatid, int lcid, [MarshalAs(UnmanagedType.LPWStr)] out string pszDesc);

            void EnumClassesOfCategories(int cImplemented, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStruct, SizeParamIndex = 0)] Guid[] rgcatidImpl, int cRequired,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStruct, SizeParamIndex = 2)] Guid[] rgcatidReq, [MarshalAs(UnmanagedType.Interface)] out object ppenumClsid);

            void IsClassOfCategories([MarshalAs(UnmanagedType.LPStruct)] Guid rclsid, int cImplemented, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStruct, SizeParamIndex = 1)] Guid[] rgcatidImpl, int cRequired,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStruct, SizeParamIndex = 3)] Guid[] rgcatidReq);

            void EnumImplCategoriesOfClass([MarshalAs(UnmanagedType.LPStruct)] Guid rclsid, [MarshalAs(UnmanagedType.Interface)] out object ppenumCatid);

            void EnumReqCategoriesOfClass([MarshalAs(UnmanagedType.LPStruct)] Guid rclsid, [MarshalAs(UnmanagedType.Interface)] out object ppenumCatid);
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct CATEGORYINFO
        {
            public Guid catid;

            public int lcid;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 127)]
            public string szDescription;
        }

        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("0002E012-0000-0000-C000-000000000046")]
        [ComImport]
        private interface ICatRegister
        {
            void RegisterCategories(int cCategories, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStruct, SizeParamIndex = 0)] Utils.CATEGORYINFO[] rgCategoryInfo);

            void UnRegisterCategories(int cCategories, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStruct, SizeParamIndex = 0)] Guid[] rgcatid);

            void RegisterClassImplCategories([MarshalAs(UnmanagedType.LPStruct)] Guid rclsid, int cCategories, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStruct, SizeParamIndex = 1)] Guid[] rgcatid);

            void UnRegisterClassImplCategories([MarshalAs(UnmanagedType.LPStruct)] Guid rclsid, int cCategories, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStruct, SizeParamIndex = 1)] Guid[] rgcatid);

            void RegisterClassReqCategories([MarshalAs(UnmanagedType.LPStruct)] Guid rclsid, int cCategories, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStruct, SizeParamIndex = 1)] Guid[] rgcatid);

            void UnRegisterClassReqCategories([MarshalAs(UnmanagedType.LPStruct)] Guid rclsid, int cCategories, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStruct, SizeParamIndex = 1)] Guid[] rgcatid);
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct GUID
        {
            public int Data1;

            public short Data2;

            public short Data3;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Data4;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct SOLE_AUTHENTICATION_SERVICE
        {
            public uint dwAuthnSvc;

            public uint dwAuthzSvc;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string pPrincipalName;

            public int hr;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct COSERVERINFO
        {
            public uint dwReserved1;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string pwszName;

            public IntPtr pAuthInfo;

            public uint dwReserved2;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct COAUTHINFO
        {
            public uint dwAuthnSvc;

            public uint dwAuthzSvc;

            public IntPtr pwszServerPrincName;

            public uint dwAuthnLevel;

            public uint dwImpersonationLevel;

            public IntPtr pAuthIdentityData;

            public uint dwCapabilities;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct COAUTHIDENTITY
        {
            public IntPtr User;

            public uint UserLength;

            public IntPtr Domain;

            public uint DomainLength;

            public IntPtr Password;

            public uint PasswordLength;

            public uint Flags;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct LICINFO
        {
            public int cbLicInfo;

            [MarshalAs(UnmanagedType.Bool)]
            public bool fRuntimeKeyAvail;

            [MarshalAs(UnmanagedType.Bool)]
            public bool fLicVerified;
        }

        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("00000001-0000-0000-C000-000000000046")]
        [ComImport]
        private interface IClassFactory
        {
            void CreateInstance([MarshalAs(UnmanagedType.IUnknown)] object punkOuter, [MarshalAs(UnmanagedType.LPStruct)] Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppvObject);

            void LockServer([MarshalAs(UnmanagedType.Bool)] bool fLock);
        }

        [Guid("B196B28F-BAB4-101A-B69C-00AA00341D07")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [ComImport]
        private interface IClassFactory2
        {
            void CreateInstance([MarshalAs(UnmanagedType.IUnknown)] object punkOuter, [MarshalAs(UnmanagedType.LPStruct)] Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppvObject);

            void LockServer([MarshalAs(UnmanagedType.Bool)] bool fLock);

            void GetLicInfo([In] [Out] ref Utils.LICINFO pLicInfo);

            void RequestLicKey(int dwReserved, [MarshalAs(UnmanagedType.BStr)] string pbstrKey);

            void CreateInstanceLic([MarshalAs(UnmanagedType.IUnknown)] object punkOuter, [MarshalAs(UnmanagedType.IUnknown)] object punkReserved, [MarshalAs(UnmanagedType.LPStruct)] Guid riid, [MarshalAs(UnmanagedType.BStr)] string bstrKey,
                [MarshalAs(UnmanagedType.IUnknown)] out object ppvObject);
        }
    }
}