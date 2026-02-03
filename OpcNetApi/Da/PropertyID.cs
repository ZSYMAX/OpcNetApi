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

            m_name = new XmlQualifiedName(name, ns);
            m_code = (int)info.GetValue("CO", typeof(int));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (m_name != null)
            {
                info.AddValue("NA", m_name.Name);
                info.AddValue("NS", m_name.Namespace);
            }

            info.AddValue("CO", m_code);
        }

        public XmlQualifiedName Name => m_name;

        public int Code => m_code;

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
            m_name = name;
            m_code = 0;
        }

        public PropertyID(int code)
        {
            m_name = null;
            m_code = code;
        }

        public PropertyID(string name, int code, string ns)
        {
            m_name = new XmlQualifiedName(name, ns);
            m_code = code;
        }

        public override bool Equals(object target)
        {
            if (target != null && target.GetType() == typeof(PropertyID))
            {
                PropertyID propertyID = (PropertyID)target;
                if (propertyID.Code != 0 && Code != 0)
                {
                    return propertyID.Code == Code;
                }

                if (propertyID.Name != null && Name != null)
                {
                    return propertyID.Name == Name;
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            if (Code != 0)
            {
                return Code.GetHashCode();
            }

            if (Name != null)
            {
                return Name.GetHashCode();
            }

            return base.GetHashCode();
        }

        public override string ToString()
        {
            if (Name != null && Code != 0)
            {
                return string.Format("{0} ({1})", Name.Name, Code);
            }

            if (Name != null)
            {
                return Name.Name;
            }

            if (Code != 0)
            {
                return string.Format("{0}", Code);
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