using System;
using System.Collections;
using System.Runtime.Serialization;

namespace Opc
{
    [Serializable]
    public class ReadOnlyCollection : ICollection, IEnumerable, ICloneable, ISerializable
    {
        public virtual object this[int index] => m_array.GetValue(index);

        public virtual Array ToArray()
        {
            return (Array)Convert.Clone(m_array);
        }

        protected ReadOnlyCollection(Array array)
        {
            Array = array;
        }

        protected virtual Array Array
        {
            get => m_array;
            set
            {
                m_array = value;
                if (m_array == null)
                {
                    m_array = new object[0];
                }
            }
        }

        protected ReadOnlyCollection(SerializationInfo info, StreamingContext context)
        {
            m_array = (Array)info.GetValue("AR", typeof(Array));
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("AR", m_array);
        }

        public virtual bool IsSynchronized => false;

        public virtual int Count => m_array.Length;

        public virtual void CopyTo(Array array, int index)
        {
            if (m_array != null)
            {
                m_array.CopyTo(array, index);
            }
        }

        public virtual object SyncRoot => this;

        public virtual IEnumerator GetEnumerator()
        {
            return m_array.GetEnumerator();
        }

        public virtual object Clone()
        {
            ReadOnlyCollection readOnlyCollection = (ReadOnlyCollection)MemberwiseClone();
            ArrayList arrayList = new ArrayList(m_array.Length);
            System.Type type = null;
            for (int i = 0; i < m_array.Length; i++)
            {
                object value = m_array.GetValue(i);
                if (type == null)
                {
                    type = value.GetType();
                }
                else if (type != typeof(object))
                {
                    while (!type.IsInstanceOfType(value))
                    {
                        type = type.BaseType;
                    }
                }

                arrayList.Add(Convert.Clone(value));
            }

            readOnlyCollection.Array = arrayList.ToArray(type);
            return readOnlyCollection;
        }

        private Array m_array;

        private class Names
        {
            internal const string ARRAY = "AR";
        }
    }
}