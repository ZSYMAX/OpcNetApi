using System;
using System.Net;
using System.Runtime.Serialization;

namespace Opc
{
    [Serializable]
    public class ConnectData : ISerializable, ICredentials
    {
        public NetworkCredential Credentials
        {
            get { return this.m_credentials; }
            set { this.m_credentials = value; }
        }

        public string LicenseKey
        {
            get { return this.m_licenseKey; }
            set { this.m_licenseKey = value; }
        }

        public bool AlwaysUseDA20 { get; set; }

        public NetworkCredential GetCredential(Uri uri, string authenticationType)
        {
            if (this.m_credentials != null)
            {
                return new NetworkCredential(this.m_credentials.UserName, this.m_credentials.Password, this.m_credentials.Domain);
            }

            return null;
        }

        public IWebProxy GetProxy()
        {
            if (this.m_proxy != null)
            {
                return this.m_proxy;
            }

            return new WebProxy();
        }

        public void SetProxy(WebProxy proxy)
        {
            this.m_proxy = proxy;
        }

        public ConnectData(NetworkCredential credentials)
        {
            this.m_credentials = credentials;
            this.m_proxy = null;
        }

        public ConnectData(NetworkCredential credentials, WebProxy proxy)
        {
            this.m_credentials = credentials;
            this.m_proxy = proxy;
        }

        protected ConnectData(SerializationInfo info, StreamingContext context)
        {
            string @string = info.GetString("UN");
            string string2 = info.GetString("PW");
            string string3 = info.GetString("DO");
            string string4 = info.GetString("PU");
            info.GetString("LK");
            if (string3 != null)
            {
                this.m_credentials = new NetworkCredential(@string, string2, string3);
            }
            else
            {
                this.m_credentials = new NetworkCredential(@string, string2);
            }

            if (string4 != null)
            {
                this.m_proxy = new WebProxy(string4);
                return;
            }

            this.m_proxy = null;
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (this.m_credentials != null)
            {
                info.AddValue("UN", this.m_credentials.UserName);
                info.AddValue("PW", this.m_credentials.Password);
                info.AddValue("DO", this.m_credentials.Domain);
            }
            else
            {
                info.AddValue("UN", null);
                info.AddValue("PW", null);
                info.AddValue("DO", null);
            }

            if (this.m_proxy != null)
            {
                info.AddValue("PU", this.m_proxy.Address);
                return;
            }

            info.AddValue("PU", null);
        }

        private NetworkCredential m_credentials;

        private WebProxy m_proxy;

        private string m_licenseKey;

        private class Names
        {
            internal const string USER_NAME = "UN";

            internal const string PASSWORD = "PW";

            internal const string DOMAIN = "DO";

            internal const string PROXY_URI = "PU";

            internal const string LICENSE_KEY = "LK";
        }
    }
}