using System;
using System.Collections;
using Opc;
using OpcRcw.Comn;

namespace OpcCom
{
    public class Server : IServer, IDisposable
    {
        internal Server()
        {
            this.m_url = null;
            this.m_server = null;
            this.m_callback = new Server.Callback(this);
        }

        internal Server(URL url, object server)
        {
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }

            this.m_url = (URL)url.Clone();
            this.m_server = server;
            this.m_callback = new Server.Callback(this);
        }

        ~Server()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.m_disposed)
            {
                lock (this)
                {
                    if (disposing && this.m_connection != null)
                    {
                        this.m_connection.Dispose();
                        this.m_connection = null;
                    }

                    Interop.ReleaseServer(this.m_server);
                    this.m_server = null;
                }

                this.m_disposed = true;
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
                if (this.m_url == null || !this.m_url.Equals(url))
                {
                    if (this.m_server != null)
                    {
                        this.Uninitialize();
                    }

                    this.m_server = (IOPCCommon)Factory.Connect(url, connectData);
                }

                this.m_url = (URL)url.Clone();
            }
        }

        public virtual void Uninitialize()
        {
            lock (this)
            {
                this.Dispose();
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
                        this.Advise();
                        this.m_callback.ServerShutdown += value;
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
                    this.m_callback.ServerShutdown -= value;
                    this.Unadvise();
                }
            }
        }

        public virtual string GetLocale()
        {
            string locale;
            lock (this)
            {
                if (this.m_server == null)
                {
                    throw new NotConnectedException();
                }

                try
                {
                    int input = 0;
                    ((IOPCCommon)this.m_server).GetLocaleID(out input);
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
                if (this.m_server == null)
                {
                    throw new NotConnectedException();
                }

                int locale2 = Interop.GetLocale(locale);
                try
                {
                    ((IOPCCommon)this.m_server).SetLocaleID(locale2);
                }
                catch (Exception e)
                {
                    if (locale2 != 0)
                    {
                        throw Interop.CreateException("IOPCCommon.SetLocaleID", e);
                    }

                    try
                    {
                        ((IOPCCommon)this.m_server).SetLocaleID(2048);
                    }
                    catch
                    {
                    }
                }

                locale3 = this.GetLocale();
            }

            return locale3;
        }

        public virtual string[] GetSupportedLocales()
        {
            string[] result;
            lock (this)
            {
                if (this.m_server == null)
                {
                    throw new NotConnectedException();
                }

                try
                {
                    int size = 0;
                    IntPtr zero = IntPtr.Zero;
                    ((IOPCCommon)this.m_server).QueryAvailableLocaleIDs(out size, out zero);
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
                if (this.m_server == null)
                {
                    throw new NotConnectedException();
                }

                try
                {
                    string locale2 = this.GetLocale();
                    if (locale2 != locale)
                    {
                        this.SetLocale(locale);
                    }

                    string text = null;
                    ((IOPCCommon)this.m_server).GetErrorString(resultID.Code, out text);
                    if (locale2 != locale)
                    {
                        this.SetLocale(locale2);
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
            if (this.m_connection == null)
            {
                this.m_connection = new ConnectionPoint(this.m_server, typeof(IOPCShutdown).GUID);
                this.m_connection.Advise(this.m_callback);
            }
        }

        private void Unadvise()
        {
            if (this.m_connection != null && this.m_connection.Unadvise() == 0)
            {
                this.m_connection.Dispose();
                this.m_connection = null;
            }
        }

        private bool m_disposed;

        protected object m_server;

        protected URL m_url;

        private ConnectionPoint m_connection;

        private Server.Callback m_callback;

        private class Callback : IOPCShutdown
        {
            public Callback(Server server)
            {
                this.m_server = server;
            }

            public event ServerShutdownEventHandler ServerShutdown
            {
                add
                {
                    lock (this)
                    {
                        this.m_serverShutdown = (ServerShutdownEventHandler)Delegate.Combine(this.m_serverShutdown, value);
                    }
                }
                remove
                {
                    lock (this)
                    {
                        this.m_serverShutdown = (ServerShutdownEventHandler)Delegate.Remove(this.m_serverShutdown, value);
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
                        if (this.m_serverShutdown != null)
                        {
                            this.m_serverShutdown(reason);
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