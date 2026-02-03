using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.Serialization;

namespace Opc
{
    [Serializable]
    public class Server : IServer, IDisposable, ISerializable, ICloneable
    {
        public Server(Factory factory, URL url)
        {
            if (factory == null)
            {
                throw new ArgumentNullException("factory");
            }

            m_factory = (IFactory)factory.Clone();
            m_server = null;
            m_url = null;
            m_name = null;
            m_supportedLocales = null;
            m_resourceManager = new ResourceManager("Opc.Resources.Strings", Assembly.GetExecutingAssembly());
            if (url != null)
            {
                SetUrl(url);
            }
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
                if (disposing)
                {
                    if (m_factory != null)
                    {
                        m_factory.Dispose();
                        m_factory = null;
                    }

                    if (m_server != null)
                    {
                        try
                        {
                            Disconnect();
                        }
                        catch
                        {
                        }

                        m_server = null;
                    }
                }

                m_disposed = true;
            }
        }

        protected Server(SerializationInfo info, StreamingContext context)
        {
            m_name = info.GetString("Name");
            m_url = (URL)info.GetValue("Url", typeof(URL));
            m_factory = (IFactory)info.GetValue("Factory", typeof(IFactory));
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", m_name);
            info.AddValue("Url", m_url);
            info.AddValue("Factory", m_factory);
        }

        public virtual object Clone()
        {
            Server server = (Server)MemberwiseClone();
            server.m_server = null;
            server.m_supportedLocales = null;
            server.m_locale = null;
            server.m_resourceManager = new ResourceManager("Opc.Resources.Strings", Assembly.GetExecutingAssembly());
            return server;
        }

        public virtual string Name
        {
            get => m_name;
            set => m_name = value;
        }

        public virtual URL Url
        {
            get
            {
                if (m_url == null)
                {
                    return null;
                }

                return (URL)m_url.Clone();
            }
            set => SetUrl(value);
        }

        public virtual string Locale => m_locale;

        public virtual string[] SupportedLocales
        {
            get
            {
                if (m_supportedLocales == null)
                {
                    return null;
                }

                return (string[])m_supportedLocales.Clone();
            }
        }

        public virtual bool IsConnected => m_server != null;

        public virtual void Connect()
        {
            Connect(m_url, null);
        }

        public virtual void Connect(ConnectData connectData)
        {
            Connect(m_url, connectData);
        }

        public virtual void Connect(URL url, ConnectData connectData)
        {
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }

            if (m_server != null)
            {
                throw new AlreadyConnectedException();
            }

            SetUrl(url);
            try
            {
                m_server = m_factory.CreateInstance(url, connectData);
                m_connectData = connectData;
                GetSupportedLocales();
                SetLocale(m_locale);
            }
            catch (Exception ex)
            {
                if (m_server != null)
                {
                    try
                    {
                        Disconnect();
                    }
                    catch
                    {
                    }
                }

                throw;
            }
        }

        public virtual void Disconnect()
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            m_server.Dispose();
            m_server = null;
        }

        public virtual Server Duplicate()
        {
            Server server = (Server)Activator.CreateInstance(GetType(), new object[]
            {
                m_factory,
                m_url
            });
            server.m_connectData = m_connectData;
            server.m_locale = m_locale;
            return server;
        }

        public virtual event ServerShutdownEventHandler ServerShutdown
        {
            add => m_server.ServerShutdown += value;
            remove => m_server.ServerShutdown -= value;
        }

        public virtual string GetLocale()
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            m_locale = m_server.GetLocale();
            return m_locale;
        }

        public virtual string SetLocale(string locale)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            try
            {
                m_locale = m_server.SetLocale(locale);
            }
            catch
            {
                string text = FindBestLocale(locale, m_supportedLocales);
                if (text != locale)
                {
                    m_server.SetLocale(text);
                }

                m_locale = text;
            }

            return m_locale;
        }

        public virtual string[] GetSupportedLocales()
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            m_supportedLocales = m_server.GetSupportedLocales();
            return SupportedLocales;
        }

        public virtual string GetErrorText(string locale, ResultID resultID)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return m_server.GetErrorText((locale == null) ? m_locale : locale, resultID);
        }

        protected string GetString(string name)
        {
            CultureInfo culture = null;
            try
            {
                culture = new CultureInfo(Locale);
            }
            catch
            {
                culture = new CultureInfo("");
            }

            string result;
            try
            {
                result = m_resourceManager.GetString(name, culture);
            }
            catch
            {
                result = null;
            }

            return result;
        }

        protected void SetUrl(URL url)
        {
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }

            if (m_server != null)
            {
                throw new AlreadyConnectedException();
            }

            m_url = (URL)url.Clone();
            string text = "";
            if (m_url.HostName != null)
            {
                text = m_url.HostName.ToLower();
                if (text == "localhost" || text == "127.0.0.1")
                {
                    text = "";
                }
            }

            if (m_url.Port != 0)
            {
                text += string.Format(".{0}", m_url.Port);
            }

            if (text != "")
            {
                text += ".";
            }

            if (m_url.Scheme != "http")
            {
                string text2 = m_url.Path;
                int num = text2.LastIndexOf('/');
                if (num != -1)
                {
                    text2 = text2.Substring(0, num);
                }

                text += text2;
            }
            else
            {
                string text3 = m_url.Path;
                int num2 = text3.LastIndexOf('.');
                if (num2 != -1)
                {
                    text3 = text3.Substring(0, num2);
                }

                while (text3.IndexOf('/') != -1)
                {
                    text3 = text3.Replace('/', '-');
                }

                text += text3;
            }

            m_name = text;
        }

        public static string FindBestLocale(string requestedLocale, string[] supportedLocales)
        {
            string result;
            try
            {
                foreach (string a in supportedLocales)
                {
                    if (a == requestedLocale)
                    {
                        return requestedLocale;
                    }
                }

                CultureInfo cultureInfo = new CultureInfo(requestedLocale);
                foreach (string name in supportedLocales)
                {
                    try
                    {
                        CultureInfo cultureInfo2 = new CultureInfo(name);
                        if (cultureInfo.Parent.Name == cultureInfo2.Name)
                        {
                            return cultureInfo2.Name;
                        }
                    }
                    catch
                    {
                    }
                }

                result = ((supportedLocales != null && supportedLocales.Length > 0) ? supportedLocales[0] : "");
            }
            catch
            {
                result = ((supportedLocales != null && supportedLocales.Length > 0) ? supportedLocales[0] : "");
            }

            return result;
        }

        private bool m_disposed;

        protected IServer m_server;

        private URL m_url;

        protected IFactory m_factory;

        private ConnectData m_connectData;

        private string m_name;

        private string m_locale;

        private string[] m_supportedLocales;

        protected ResourceManager m_resourceManager;

        private class Names
        {
            internal const string NAME = "Name";

            internal const string URL = "Url";

            internal const string FACTORY = "Factory";
        }
    }
}