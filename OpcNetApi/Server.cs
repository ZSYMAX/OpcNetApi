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

            this.m_factory = (IFactory)factory.Clone();
            this.m_server = null;
            this.m_url = null;
            this.m_name = null;
            this.m_supportedLocales = null;
            this.m_resourceManager = new ResourceManager("Opc.Resources.Strings", Assembly.GetExecutingAssembly());
            if (url != null)
            {
                this.SetUrl(url);
            }
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
                if (disposing)
                {
                    if (this.m_factory != null)
                    {
                        this.m_factory.Dispose();
                        this.m_factory = null;
                    }

                    if (this.m_server != null)
                    {
                        try
                        {
                            this.Disconnect();
                        }
                        catch
                        {
                        }

                        this.m_server = null;
                    }
                }

                this.m_disposed = true;
            }
        }

        protected Server(SerializationInfo info, StreamingContext context)
        {
            this.m_name = info.GetString("Name");
            this.m_url = (URL)info.GetValue("Url", typeof(URL));
            this.m_factory = (IFactory)info.GetValue("Factory", typeof(IFactory));
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", this.m_name);
            info.AddValue("Url", this.m_url);
            info.AddValue("Factory", this.m_factory);
        }

        public virtual object Clone()
        {
            Server server = (Server)base.MemberwiseClone();
            server.m_server = null;
            server.m_supportedLocales = null;
            server.m_locale = null;
            server.m_resourceManager = new ResourceManager("Opc.Resources.Strings", Assembly.GetExecutingAssembly());
            return server;
        }

        public virtual string Name
        {
            get { return this.m_name; }
            set { this.m_name = value; }
        }

        public virtual URL Url
        {
            get
            {
                if (this.m_url == null)
                {
                    return null;
                }

                return (URL)this.m_url.Clone();
            }
            set { this.SetUrl(value); }
        }

        public virtual string Locale
        {
            get { return this.m_locale; }
        }

        public virtual string[] SupportedLocales
        {
            get
            {
                if (this.m_supportedLocales == null)
                {
                    return null;
                }

                return (string[])this.m_supportedLocales.Clone();
            }
        }

        public virtual bool IsConnected
        {
            get { return this.m_server != null; }
        }

        public virtual void Connect()
        {
            this.Connect(this.m_url, null);
        }

        public virtual void Connect(ConnectData connectData)
        {
            this.Connect(this.m_url, connectData);
        }

        public virtual void Connect(URL url, ConnectData connectData)
        {
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }

            if (this.m_server != null)
            {
                throw new AlreadyConnectedException();
            }

            this.SetUrl(url);
            try
            {
                this.m_server = this.m_factory.CreateInstance(url, connectData);
                this.m_connectData = connectData;
                this.GetSupportedLocales();
                this.SetLocale(this.m_locale);
            }
            catch (Exception ex)
            {
                if (this.m_server != null)
                {
                    try
                    {
                        this.Disconnect();
                    }
                    catch
                    {
                    }
                }

                throw ex;
            }
        }

        public virtual void Disconnect()
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            this.m_server.Dispose();
            this.m_server = null;
        }

        public virtual Server Duplicate()
        {
            Server server = (Server)Activator.CreateInstance(base.GetType(), new object[]
            {
                this.m_factory,
                this.m_url
            });
            server.m_connectData = this.m_connectData;
            server.m_locale = this.m_locale;
            return server;
        }

        public virtual event ServerShutdownEventHandler ServerShutdown
        {
            add { this.m_server.ServerShutdown += value; }
            remove { this.m_server.ServerShutdown -= value; }
        }

        public virtual string GetLocale()
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            this.m_locale = this.m_server.GetLocale();
            return this.m_locale;
        }

        public virtual string SetLocale(string locale)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            try
            {
                this.m_locale = this.m_server.SetLocale(locale);
            }
            catch
            {
                string text = Server.FindBestLocale(locale, this.m_supportedLocales);
                if (text != locale)
                {
                    this.m_server.SetLocale(text);
                }

                this.m_locale = text;
            }

            return this.m_locale;
        }

        public virtual string[] GetSupportedLocales()
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            this.m_supportedLocales = this.m_server.GetSupportedLocales();
            return this.SupportedLocales;
        }

        public virtual string GetErrorText(string locale, ResultID resultID)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            return this.m_server.GetErrorText((locale == null) ? this.m_locale : locale, resultID);
        }

        protected string GetString(string name)
        {
            CultureInfo culture = null;
            try
            {
                culture = new CultureInfo(this.Locale);
            }
            catch
            {
                culture = new CultureInfo("");
            }

            string result;
            try
            {
                result = this.m_resourceManager.GetString(name, culture);
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

            if (this.m_server != null)
            {
                throw new AlreadyConnectedException();
            }

            this.m_url = (URL)url.Clone();
            string text = "";
            if (this.m_url.HostName != null)
            {
                text = this.m_url.HostName.ToLower();
                if (text == "localhost" || text == "127.0.0.1")
                {
                    text = "";
                }
            }

            if (this.m_url.Port != 0)
            {
                text += string.Format(".{0}", this.m_url.Port);
            }

            if (text != "")
            {
                text += ".";
            }

            if (this.m_url.Scheme != "http")
            {
                string text2 = this.m_url.Path;
                int num = text2.LastIndexOf('/');
                if (num != -1)
                {
                    text2 = text2.Substring(0, num);
                }

                text += text2;
            }
            else
            {
                string text3 = this.m_url.Path;
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

            this.m_name = text;
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