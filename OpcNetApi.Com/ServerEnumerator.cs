using System;
using System.Collections;
using System.Net;
using System.Runtime.InteropServices;
using Opc;
using Opc.Ae;
using Opc.Da;
using Opc.Dx;
using Opc.Hda;
using OpcRcw.Comn;

namespace OpcCom
{
    public class ServerEnumerator : IDiscovery, IDisposable
    {
        public void Dispose()
        {
        }

        public string[] EnumerateHosts()
        {
            return Interop.EnumComputers();
        }

        public Opc.Server[] GetAvailableServers(Specification specification)
        {
            return this.GetAvailableServers(specification, null, null);
        }

        public Opc.Server[] GetAvailableServers(Specification specification, string host, ConnectData connectData)
        {
            Opc.Server[] result;
            lock (this)
            {
                NetworkCredential credential = (connectData != null) ? connectData.GetCredential(null, null) : null;
                this.m_server = (IOPCServerList2)Interop.CreateInstance(ServerEnumerator.CLSID, host, credential);
                this.m_host = host;
                try
                {
                    ArrayList arrayList = new ArrayList();
                    Guid guid = new Guid(specification.ID);
                    IOPCEnumGUID iopcenumGUID = null;
                    this.m_server.EnumClassesOfCategories(1, new Guid[]
                    {
                        guid
                    }, 0, null, out iopcenumGUID);
                    Guid[] array = this.ReadClasses(iopcenumGUID);
                    Interop.ReleaseServer(iopcenumGUID);
                    iopcenumGUID = null;
                    foreach (Guid clsid in array)
                    {
                        Factory factory = new Factory();
                        try
                        {
                            URL url = this.CreateUrl(specification, clsid);
                            Opc.Server value = null;
                            if (specification == Specification.COM_DA_30)
                            {
                                value = new Opc.Da.Server(factory, url);
                            }
                            else if (specification == Specification.COM_DA_20)
                            {
                                value = new Opc.Da.Server(factory, url);
                            }
                            else if (specification == Specification.COM_AE_10)
                            {
                                value = new Opc.Ae.Server(factory, url);
                            }
                            else if (specification == Specification.COM_HDA_10)
                            {
                                value = new Opc.Hda.Server(factory, url);
                            }
                            else if (specification == Specification.COM_DX_10)
                            {
                                value = new Opc.Dx.Server(factory, url);
                            }

                            arrayList.Add(value);
                        }
                        catch (Exception)
                        {
                        }
                    }

                    result = (Opc.Server[])arrayList.ToArray(typeof(Opc.Server));
                }
                finally
                {
                    Interop.ReleaseServer(this.m_server);
                    this.m_server = null;
                }
            }

            return result;
        }

        public Guid CLSIDFromProgID(string progID, string host, ConnectData connectData)
        {
            Guid result;
            lock (this)
            {
                NetworkCredential credential = (connectData != null) ? connectData.GetCredential(null, null) : null;
                this.m_server = (IOPCServerList2)Interop.CreateInstance(ServerEnumerator.CLSID, host, credential);
                this.m_host = host;
                Guid empty;
                try
                {
                    this.m_server.CLSIDFromProgID(progID, out empty);
                }
                catch
                {
                    empty = Guid.Empty;
                }
                finally
                {
                    Interop.ReleaseServer(this.m_server);
                    this.m_server = null;
                }

                result = empty;
            }

            return result;
        }

        private Guid[] ReadClasses(IOPCEnumGUID enumerator)
        {
            ArrayList arrayList = new ArrayList();
            int num = 0;
            int num2 = 10;
            IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(Guid)) * num2);
            Guid[] result;
            try
            {
                do
                {
                    try
                    {
                        enumerator.Next(num2, intPtr, out num);
                        IntPtr ptr = intPtr;
                        for (int i = 0; i < num; i++)
                        {
                            Guid guid = (Guid)Marshal.PtrToStructure(ptr, typeof(Guid));
                            arrayList.Add(guid);
                            ptr = (IntPtr)(ptr.ToInt64() + (long)Marshal.SizeOf(typeof(Guid)));
                        }
                    }
                    catch
                    {
                        break;
                    }
                } while (num > 0);

                result = (Guid[])arrayList.ToArray(typeof(Guid));
            }
            finally
            {
                Marshal.FreeCoTaskMem(intPtr);
            }

            return result;
        }

        private URL CreateUrl(Specification specification, Guid clsid)
        {
            URL url = new URL();
            url.HostName = this.m_host;
            url.Port = 0;
            url.Path = null;
            if (specification == Specification.COM_DA_30)
            {
                url.Scheme = "opcda";
            }
            else if (specification == Specification.COM_DA_20)
            {
                url.Scheme = "opcda";
            }
            else if (specification == Specification.COM_DA_10)
            {
                url.Scheme = "opcda";
            }
            else if (specification == Specification.COM_DX_10)
            {
                url.Scheme = "opcdx";
            }
            else if (specification == Specification.COM_AE_10)
            {
                url.Scheme = "opcae";
            }
            else if (specification == Specification.COM_HDA_10)
            {
                url.Scheme = "opchda";
            }
            else if (specification == Specification.COM_BATCH_10)
            {
                url.Scheme = "opcbatch";
            }
            else if (specification == Specification.COM_BATCH_20)
            {
                url.Scheme = "opcbatch";
            }

            try
            {
                string text = null;
                string text2 = null;
                string text3 = null;
                this.m_server.GetClassDetails(ref clsid, out text, out text2, out text3);
                if (text3 != null)
                {
                    url.Path = string.Format("{0}/{1}", text3, "{" + clsid.ToString() + "}");
                }
                else if (text != null)
                {
                    url.Path = string.Format("{0}/{1}", text, "{" + clsid.ToString() + "}");
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                if (url.Path == null)
                {
                    url.Path = string.Format("{0}", "{" + clsid.ToString() + "}");
                }
            }

            return url;
        }

        private const string ProgID = "OPC.ServerList.1";

        private IOPCServerList2 m_server;

        private string m_host;

        private static readonly Guid CLSID = new Guid("13486D51-4821-11D2-A494-3CB306C10000");
    }
}