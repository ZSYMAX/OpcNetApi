using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using OpcRcw.Comn;

namespace OpcRcw
{
    public class ServerEnumerator : IDisposable
    {
        public ServerEnumerator()
        {
            Initialize();
        }

        private void Initialize()
        {
            m_server = null;
            m_host = null;
        }

        ~ServerEnumerator()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (m_server != null)
            {
                Utils.ReleaseServer(m_server);
                m_server = null;
            }
        }

        public void Connect()
        {
            Connect(null, null, null, null);
        }

        public void Connect(string host, string username, string password, string domain)
        {
            Disconnect();
            object obj = null;
            try
            {
                obj = Utils.CreateInstance(OPCEnumCLSID, host, username, password, domain);
            }
            catch (Exception e)
            {
                throw Utils.CreateComException(e);
            }

            m_server = (obj as IOPCServerList2);
            if (m_server == null)
            {
                Utils.ReleaseServer(obj);
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("Server does not support IOPCServerList2. ");
                stringBuilder.Append("The OPC proxy/stubs may not be installed properly or the client or server machine. ");
                stringBuilder.Append("The also could be a problem with DCOM security configuration.");
                throw Utils.CreateComException(-2147467262, stringBuilder.ToString(), new object[0]);
            }

            m_host = host;
            if (string.IsNullOrEmpty(m_host))
            {
                m_host = "localhost";
            }
        }

        public void Disconnect()
        {
            try
            {
                if (m_server != null)
                {
                    Utils.ReleaseServer(m_server);
                    m_server = null;
                }
            }
            catch (Exception e)
            {
                throw Utils.CreateComException(e, -2147467259, "Could not release OPCEnum server.", new object[0]);
            }
        }

        public ServerDescription[] GetAvailableServers(params Guid[] catids)
        {
            ServerDescription[] result;
            try
            {
                m_server.EnumClassesOfCategories(catids.Length, catids, 0, null, out var iopcenumGUID);
                List<Guid> list = ReadClasses(iopcenumGUID);
                Utils.ReleaseServer(iopcenumGUID);
                iopcenumGUID = null;
                ServerDescription[] array = new ServerDescription[list.Count];
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = ReadServerDetails(list[i]);
                }

                result = array;
            }
            catch (Exception e)
            {
                throw Utils.CreateComException(e, -2147467259, "Could not enumerate COM servers.", new object[0]);
            }

            return result;
        }

        public Guid CLSIDFromProgID(string progID)
        {
            Guid empty;
            try
            {
                m_server.CLSIDFromProgID(progID, out empty);
            }
            catch
            {
                empty = Guid.Empty;
            }

            return empty;
        }

        private List<Guid> ReadClasses(IOPCEnumGUID enumerator)
        {
            List<Guid> list = new List<Guid>();
            int num = 0;
            Guid[] array = new Guid[10];
            do
            {
                try
                {
                    IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(Guid)) * array.Length);
                    try
                    {
                        enumerator.Next(array.Length, intPtr, out num);
                        if (num > 0)
                        {
                            IntPtr ptr = intPtr;
                            for (int i = 0; i < num; i++)
                            {
                                array[i] = (Guid)Marshal.PtrToStructure(ptr, typeof(Guid));
                                ptr = (IntPtr)(ptr.ToInt64() + (long)Marshal.SizeOf(typeof(Guid)));
                                list.Add(array[i]);
                            }
                        }
                    }
                    finally
                    {
                        Marshal.FreeCoTaskMem(intPtr);
                    }
                }
                catch
                {
                    break;
                }
            } while (num > 0);

            return list;
        }

        private ServerDescription ReadServerDetails(Guid clsid)
        {
            ServerDescription serverDescription = new ServerDescription();
            serverDescription.HostName = m_host;
            serverDescription.Clsid = clsid;
            string progId = null;
            try
            {
                m_server.GetClassDetails(ref clsid, out progId, out var description, out var text);
                if (!string.IsNullOrEmpty(text))
                {
                    progId = text;
                }

                serverDescription.Description = description;
                serverDescription.VersionIndependentProgId = text;
            }
            catch
            {
                progId = null;
            }

            serverDescription.ProgId = progId;
            return serverDescription;
        }

        private IOPCServerList2 m_server;

        private string m_host;

        private static readonly Guid OPCEnumCLSID = new Guid("13486D51-4821-11D2-A494-3CB306C10000");
    }
}