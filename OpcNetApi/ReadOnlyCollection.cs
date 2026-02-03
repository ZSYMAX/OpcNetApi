using System;
using System.Collections;
using System.Runtime.Serialization;

namespace Opc
{
    [Serializable]
    public class ReadOnlyCollection : ICollection, IEnumerable, ICloneable, ISerializable
    {
        public virtual object this[int index]
        {
            get { return this.m_array.GetValue(index); }
        }

        public virtual Array ToArray()
        {
            return (Array)Convert.Clone(this.m_array);
        }

        protected ReadOnlyCollection(Array array)
        {
            this.Array = array;
        }

        protected virtual Array Array
        {
            get { return this.m_array; }
            set
            {
                this.m_array = value;
                if (this.m_array == null)
                {
                    this.m_array = new object[0];
                }
            }
        }

        protected ReadOnlyCollection(SerializationInfo info, StreamingContext context)
        {
            this.m_array = (Array)info.GetValue("AR", typeof(Array));
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("AR", this.m_array);
        }

        public virtual bool IsSynchronized
        {
            get { return false; }
        }

        public virtual int Count
        {
            get { return this.m_array.Length; }
        }

        public virtual void CopyTo(Array array, int index)
        {
            if (this.m_array != null)
            {
                this.m_array.CopyTo(array, index);
            }
        }

        public virtual object SyncRoot
        {
            get { return this; }
        }

        public virtual IEnumerator GetEnumerator()
        {
            return this.m_array.GetEnumerator();
        }

        public virtual object Clone()
        {
            ReadOnlyCollection readOnlyCollection = (ReadOnlyCollection)base.MemberwiseClone();
            ArrayList arrayList = new ArrayList(this.m_array.Length);
            System.Type type = null;
            for (int i = 0; i < this.m_array.Length; i++)
            {
                object value = this.m_array.GetValue(i);
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