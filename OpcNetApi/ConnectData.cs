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
            get => m_credentials;
            set => m_credentials = value;
        }

        public string LicenseKey
        {
            get => m_licenseKey;
            set => m_licenseKey = value;
        }

        public bool AlwaysUseDA20 { get; set; }

        public NetworkCredential GetCredential(Uri uri, string authenticationType)
        {
            if (m_credentials != null)
            {
                return new NetworkCredential(m_credentials.UserName, m_credentials.Password, m_credentials.Domain);
            }

            return null;
        }

        public IWebProxy GetProxy()
        {
            if (m_proxy != null)
            {
                return m_proxy;
            }

            return new WebProxy();
        }

        public void SetProxy(WebProxy proxy)
        {
            m_proxy = proxy;
        }

        public ConnectData(NetworkCredential credentials)
        {
            m_credentials = credentials;
            m_proxy = null;
        }

        public ConnectData(NetworkCredential credentials, WebProxy proxy)
        {
            m_credentials = credentials;
            m_proxy = proxy;
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
                m_credentials = new NetworkCredential(@string, string2, string3);
            }
            else
            {
                m_credentials = new NetworkCredential(@string, string2);
            }

            if (string4 != null)
            {
                m_proxy = new WebProxy(string4);
                return;
            }

            m_proxy = null;
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (m_credentials != null)
            {
                info.AddValue("UN", m_credentials.UserName);
                info.AddValue("PW", m_credentials.Password);
                info.AddValue("DO", m_credentials.Domain);
            }
            else
            {
                info.AddValue("UN", null);
                info.AddValue("PW", null);
                info.AddValue("DO", null);
            }

            if (m_proxy != null)
            {
                info.AddValue("PU", m_proxy.Address);
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