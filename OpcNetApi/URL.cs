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
            get { return this.m_scheme; }
            set { this.m_scheme = value; }
        }

        public string HostName
        {
            get { return this.m_hostName; }
            set { this.m_hostName = value; }
        }

        public int Port
        {
            get { return this.m_port; }
            set { this.m_port = value; }
        }

        public string Path
        {
            get { return this.m_path; }
            set { this.m_path = value; }
        }

        public URL()
        {
            this.Scheme = "http";
            this.HostName = "localhost";
            this.Port = 0;
            this.Path = null;
        }

        public URL(string url)
        {
            this.Scheme = "http";
            this.HostName = "localhost";
            this.Port = 0;
            this.Path = null;
            string text = url;
            int num = text.IndexOf("://");
            if (num >= 0)
            {
                this.Scheme = text.Substring(0, num);
                text = text.Substring(num + 3);
            }

            num = text.IndexOfAny(new char[]
            {
                '/'
            });
            if (num < 0)
            {
                this.Path = text;
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
                    this.HostName = text2.Substring(0, text2.IndexOf("]") + 1);
                    if (text2.Substring(text2.IndexOf(']')).Contains(":"))
                    {
                        string text3 = text2.Substring(text2.LastIndexOf(':') + 1);
                        if (text3 != "")
                        {
                            try
                            {
                                this.Port = (int)System.Convert.ToUInt16(text3);
                                goto IL_12E;
                            }
                            catch
                            {
                                this.Port = 0;
                                goto IL_12E;
                            }
                        }

                        this.Port = 0;
                    }
                    else
                    {
                        this.Port = 0;
                    }

                    IL_12E:
                    this.Path = text.Substring(num + 1);
                }
                else
                {
                    this.HostName = "[" + text2 + "]";
                    this.Port = 0;
                }

                this.Path = text.Substring(num + 1);
                return;
            }

            num = text.IndexOfAny(new char[]
            {
                ':',
                '/'
            });
            if (num < 0)
            {
                this.Path = text;
                return;
            }

            this.HostName = text.Substring(0, num);
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
                    this.Port = (int)System.Convert.ToUInt16(value);
                    goto IL_20D;
                }
                catch
                {
                    this.Port = 0;
                    goto IL_20D;
                }
            }

            text = text.Substring(num + 1);
            IL_20D:
            this.Path = text;
        }

        public override string ToString()
        {
            string text = (this.HostName == null || this.HostName == "") ? "localhost" : this.HostName;
            if (this.Port > 0)
            {
                return string.Format("{0}://{1}:{2}/{3}", new object[]
                {
                    this.Scheme,
                    text,
                    this.Port,
                    this.Path
                });
            }

            return string.Format("{0}://{1}/{2}", new object[]
            {
                this.Scheme,
                text,
                this.Path
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

            return url != null && !(url.Path != this.Path) && !(url.Scheme != this.Scheme) && !(url.HostName != this.HostName) && url.Port == this.Port;
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public virtual object Clone()
        {
            return base.MemberwiseClone();
        }

        private string m_scheme;

        private string m_hostName;

        private int m_port;

        private string m_path;
    }
}