using System;
using System.Collections;
using Opc;
using OpcRcw.Comn;

namespace OpcCom
{
    public class Server : IServer
    {
        internal Server()
        {
            m_url = null;
            m_server = null;
            m_callback = new Callback(this);
        }

        internal Server(URL url, object server)
        {
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }

            m_url = (URL)url.Clone();
            m_server = server;
            m_callback = new Callback(this);
        }

        ~Server()
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
            if (!m_disposed)
            {
                lock (this)
                {
                    if (disposing && m_connection != null)
                    {
                        m_connection.Dispose();
                        m_connection = null;
                    }

                    Interop.ReleaseServer(m_server);
                    m_server = null;
                }

                m_disposed = true;
            }
        }

        public virtual void Initialize(URL url, ConnectData connectData)
        {
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }

            lock (this)
            {
                if (m_url == null || !m_url.Equals(url))
                {
                    if (m_server != null)
                    {
                        Uninitialize();
                    }

                    m_server = (IOPCCommon)Factory.Connect(url, connectData);
                }

                m_url = (URL)url.Clone();
            }
        }

        public virtual void Uninitialize()
        {
            lock (this)
            {
                Dispose();
            }
        }

        public virtual event ServerShutdownEventHandler ServerShutdown
        {
            add
            {
                lock (this)
                {
                    try
                    {
                        Advise();
                        m_callback.ServerShutdown += value;
                    }
                    catch
                    {
                    }
                }
            }
            remove
            {
                lock (this)
                {
                    m_callback.ServerShutdown -= value;
                    Unadvise();
                }
            }
        }

        public virtual string GetLocale()
        {
            string locale;
            lock (this)
            {
                if (m_server == null)
                {
                    throw new NotConnectedException();
                }

                try
                {
                    ((IOPCCommon)m_server).GetLocaleID(out var input);
                    locale = Interop.GetLocale(input);
                }
                catch (Exception e)
                {
                    throw Interop.CreateException("IOPCCommon.GetLocaleID", e);
                }
            }

            return locale;
        }

        public virtual string SetLocale(string locale)
        {
            string locale3;
            lock (this)
            {
                if (m_server == null)
                {
                    throw new NotConnectedException();
                }

                int locale2 = Interop.GetLocale(locale);
                try
                {
                    ((IOPCCommon)m_server).SetLocaleID(locale2);
                }
                catch (Exception e)
                {
                    if (locale2 != 0)
                    {
                        throw Interop.CreateException("IOPCCommon.SetLocaleID", e);
                    }

                    try
                    {
                        ((IOPCCommon)m_server).SetLocaleID(2048);
                    }
                    catch
                    {
                    }
                }

                locale3 = GetLocale();
            }

            return locale3;
        }

        public virtual string[] GetSupportedLocales()
        {
            string[] result;
            lock (this)
            {
                if (m_server == null)
                {
                    throw new NotConnectedException();
                }

                try
                {
                    ((IOPCCommon)m_server).QueryAvailableLocaleIDs(out var size, out var zero);
                    int[] int32s = Interop.GetInt32s(ref zero, size, true);
                    if (int32s != null)
                    {
                        ArrayList arrayList = new ArrayList();
                        foreach (int input in int32s)
                        {
                            try
                            {
                                arrayList.Add(Interop.GetLocale(input));
                            }
                            catch
                            {
                            }
                        }

                        result = (string[])arrayList.ToArray(typeof(string));
                    }
                    else
                    {
                        result = null;
                    }
                }
                catch (Exception e)
                {
                    throw Interop.CreateException("IOPCCommon.QueryAvailableLocaleIDs", e);
                }
            }

            return result;
        }

        public virtual string GetErrorText(string locale, ResultID resultID)
        {
            string result;
            lock (this)
            {
                if (m_server == null)
                {
                    throw new NotConnectedException();
                }

                try
                {
                    string locale2 = GetLocale();
                    if (locale2 != locale)
                    {
                        SetLocale(locale);
                    }

                    ((IOPCCommon)m_server).GetErrorString(resultID.Code, out var text);
                    if (locale2 != locale)
                    {
                        SetLocale(locale2);
                    }

                    result = text;
                }
                catch (Exception e)
                {
                    throw Interop.CreateException("IOPCServer.GetErrorString", e);
                }
            }

            return result;
        }

        private void Advise()
        {
            if (m_connection == null)
            {
                m_connection = new ConnectionPoint(m_server, typeof(IOPCShutdown).GUID);
                m_connection.Advise(m_callback);
            }
        }

        private void Unadvise()
        {
            if (m_connection != null && m_connection.Unadvise() == 0)
            {
                m_connection.Dispose();
                m_connection = null;
            }
        }

        private bool m_disposed;

        protected object m_server;

        protected URL m_url;

        private ConnectionPoint m_connection;

        private Callback m_callback;

        private class Callback : IOPCShutdown
        {
            public Callback(Server server)
            {
                m_server = server;
            }

            public event ServerShutdownEventHandler ServerShutdown
            {
                add
                {
                    lock (this)
                    {
                        m_serverShutdown = (ServerShutdownEventHandler)Delegate.Combine(m_serverShutdown, value);
                    }
                }
                remove
                {
                    lock (this)
                    {
                        m_serverShutdown = (ServerShutdownEventHandler)Delegate.Remove(m_serverShutdown, value);
                    }
                }
            }

            private event ServerShutdownEventHandler m_serverShutdown;

            public void ShutdownRequest(string reason)
            {
                try
                {
                    lock (this)
                    {
                        if (m_serverShutdown != null)
                        {
                            m_serverShutdown(reason);
                        }
                    }
                }
                catch (Exception ex)
                {
                    string stackTrace = ex.StackTrace;
                }
            }

            private Server m_server;
        }
    }
}