using System;
using System.Net;
using System.Net.Sockets;

namespace Opc
{
    [Serializable]
    public class URL : ICloneable
    {
        public string Scheme
        {
            get => m_scheme;
            set => m_scheme = value;
        }

        public string HostName
        {
            get => m_hostName;
            set => m_hostName = value;
        }

        public int Port
        {
            get => m_port;
            set => m_port = value;
        }

        public string Path
        {
            get => m_path;
            set => m_path = value;
        }

        public URL()
        {
            Scheme = "http";
            HostName = "localhost";
            Port = 0;
            Path = null;
        }

        public URL(string url)
        {
            Scheme = "http";
            HostName = "localhost";
            Port = 0;
            Path = null;
            string text = url;
            int num = text.IndexOf("://");
            if (num >= 0)
            {
                Scheme = text.Substring(0, num);
                text = text.Substring(num + 3);
            }

            num = text.IndexOfAny(new char[]
            {
                '/'
            });
            if (num < 0)
            {
                Path = text;
                return;
            }

            string text2 = text.Substring(0, num);
            IPAddress ipaddress;
            try
            {
                ipaddress = IPAddress.Parse(text2);
            }
            catch
            {
                ipaddress = null;
            }

            if (ipaddress != null && ipaddress.AddressFamily == AddressFamily.InterNetworkV6)
            {
                if (text2.Contains("]"))
                {
                    HostName = text2.Substring(0, text2.IndexOf("]") + 1);
                    if (text2.Substring(text2.IndexOf(']')).Contains(":"))
                    {
                        string text3 = text2.Substring(text2.LastIndexOf(':') + 1);
                        if (text3 != "")
                        {
                            try
                            {
                                Port = (int)System.Convert.ToUInt16(text3);
                                goto IL_12E;
                            }
                            catch
                            {
                                Port = 0;
                                goto IL_12E;
                            }
                        }

                        Port = 0;
                    }
                    else
                    {
                        Port = 0;
                    }

                    IL_12E:
                    Path = text.Substring(num + 1);
                }
                else
                {
                    HostName = "[" + text2 + "]";
                    Port = 0;
                }

                Path = text.Substring(num + 1);
                return;
            }

            num = text.IndexOfAny(new char[]
            {
                ':',
                '/'
            });
            if (num < 0)
            {
                Path = text;
                return;
            }

            HostName = text.Substring(0, num);
            if (text[num] == ':')
            {
                text = text.Substring(num + 1);
                num = text.IndexOf("/");
                string value;
                if (num >= 0)
                {
                    value = text.Substring(0, num);
                    text = text.Substring(num + 1);
                }
                else
                {
                    value = text;
                    text = "";
                }

                try
                {
                    Port = (int)System.Convert.ToUInt16(value);
                    goto IL_20D;
                }
                catch
                {
                    Port = 0;
                    goto IL_20D;
                }
            }

            text = text.Substring(num + 1);
            IL_20D:
            Path = text;
        }

        public override string ToString()
        {
            string text = (HostName == null || HostName == "") ? "localhost" : HostName;
            if (Port > 0)
            {
                return string.Format("{0}://{1}:{2}/{3}", new object[]
                {
                    Scheme,
                    text,
                    Port,
                    Path
                });
            }

            return string.Format("{0}://{1}/{2}", new object[]
            {
                Scheme,
                text,
                Path
            });
        }

        public override bool Equals(object target)
        {
            URL url = null;
            if (target != null && target.GetType() == typeof(URL))
            {
                url = (URL)target;
            }

            if (target != null && target.GetType() == typeof(string))
            {
                url = new URL((string)target);
            }

            return url != null && !(url.Path != Path) && !(url.Scheme != Scheme) && !(url.HostName != HostName) && url.Port == Port;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        private string m_scheme;

        private string m_hostName;

        private int m_port;

        private string m_path;
    }
}