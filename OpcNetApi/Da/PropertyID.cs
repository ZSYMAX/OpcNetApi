using System;
using System.Runtime.Serialization;
using System.Xml;

namespace Opc.Da
{
    [Serializable]
    public struct PropertyID : ISerializable
    {
        private PropertyID(SerializationInfo info, StreamingContext context)
        {
            SerializationInfoEnumerator enumerator = info.GetEnumerator();
            string name = "";
            string ns = "";
            enumerator.Reset();
            while (enumerator.MoveNext())
            {
                SerializationEntry serializationEntry = enumerator.Current;
                if (serializationEntry.Name.Equals("NA"))
                {
                    SerializationEntry serializationEntry2 = enumerator.Current;
                    name = (string)serializationEntry2.Value;
                }
                else
                {
                    SerializationEntry serializationEntry3 = enumerator.Current;
                    if (serializationEntry3.Name.Equals("NS"))
                    {
                        SerializationEntry serializationEntry4 = enumerator.Current;
                        ns = (string)serializationEntry4.Value;
                    }
                }
            }

            this.m_name = new XmlQualifiedName(name, ns);
            this.m_code = (int)info.GetValue("CO", typeof(int));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (this.m_name != null)
            {
                info.AddValue("NA", this.m_name.Name);
                info.AddValue("NS", this.m_name.Namespace);
            }

            info.AddValue("CO", this.m_code);
        }

        public XmlQualifiedName Name
        {
            get { return this.m_name; }
        }

        public int Code
        {
            get { return this.m_code; }
        }

        public static bool operator ==(PropertyID a, PropertyID b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(PropertyID a, PropertyID b)
        {
            return !a.Equals(b);
        }

        public PropertyID(XmlQualifiedName name)
        {
            this.m_name = name;
            this.m_code = 0;
        }

        public PropertyID(int code)
        {
            this.m_name = null;
            this.m_code = code;
        }

        public PropertyID(string name, int code, string ns)
        {
            this.m_name = new XmlQualifiedName(name, ns);
            this.m_code = code;
        }

        public override bool Equals(object target)
        {
            if (target != null && target.GetType() == typeof(PropertyID))
            {
                PropertyID propertyID = (PropertyID)target;
                if (propertyID.Code != 0 && this.Code != 0)
                {
                    return propertyID.Code == this.Code;
                }

                if (propertyID.Name != null && this.Name != null)
                {
                    return propertyID.Name == this.Name;
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            if (this.Code != 0)
            {
                return this.Code.GetHashCode();
            }

            if (this.Name != null)
            {
                return this.Name.GetHashCode();
            }

            return base.GetHashCode();
        }

        public override string ToString()
        {
            if (this.Name != null && this.Code != 0)
            {
                return string.Format("{0} ({1})", this.Name.Name, this.Code);
            }

            if (this.Name != null)
            {
                return this.Name.Name;
            }

            if (this.Code != 0)
            {
                return string.Format("{0}", this.Code);
            }

            return "";
        }

        private int m_code;

        private XmlQualifiedName m_name;

        private class Names
        {
            internal const string NAME = "NA";

            internal const string NAMESPACE = "NS";

            internal const string CODE = "CO";
        }
    }
}