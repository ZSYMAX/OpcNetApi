using System;
using System.Collections;
using System.Runtime.Serialization;

namespace Opc
{
    [Serializable]
    public class WriteableCollection : IList, ICollection, IEnumerable, ICloneable, ISerializable
    {
        public virtual object this[int index]
        {
            get => m_array[index];
            set => m_array[index] = value;
        }

        public virtual Array ToArray()
        {
            return m_array.ToArray(m_elementType);
        }

        public virtual void AddRange(ICollection collection)
        {
            if (collection != null)
            {
                foreach (object element in collection)
                {
                    ValidateElement(element);
                }

                m_array.AddRange(collection);
            }
        }

        protected WriteableCollection(ICollection array, System.Type elementType)
        {
            if (array != null)
            {
                m_array = new ArrayList(array);
            }
            else
            {
                m_array = new ArrayList();
            }

            m_elementType = typeof(object);
            if (elementType != null)
            {
                foreach (object element in m_array)
                {
                    ValidateElement(element);
                }

                m_elementType = elementType;
            }
        }

        protected virtual ArrayList Array
        {
            get => m_array;
            set
            {
                m_array = value;
                if (m_array == null)
                {
                    m_array = new ArrayList();
                }
            }
        }

        protected virtual System.Type ElementType
        {
            get => m_elementType;
            set
            {
                foreach (object element in m_array)
                {
                    ValidateElement(element);
                }

                m_elementType = value;
            }
        }

        protected virtual void ValidateElement(object element)
        {
            if (element == null)
            {
                throw new ArgumentException(string.Format("The value '{0}' cannot be added to the collection.", element));
            }

            if (!m_elementType.IsInstanceOfType(element))
            {
                throw new ArgumentException(string.Format("A value with type '{0}' cannot be added to the collection.", element.GetType()));
            }
        }

        protected WriteableCollection(SerializationInfo info, StreamingContext context)
        {
            m_elementType = (System.Type)info.GetValue("ET", typeof(Type));
            int num = (int)info.GetValue("CT", typeof(int));
            m_array = new ArrayList(num);
            for (int i = 0; i < num; i++)
            {
                m_array.Add(info.GetValue("EL" + i.ToString(), typeof(object)));
            }
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ET", m_elementType);
            info.AddValue("CT", m_array.Count);
            for (int i = 0; i < m_array.Count; i++)
            {
                info.AddValue("EL" + i.ToString(), m_array[i]);
            }
        }

        public virtual bool IsSynchronized => false;

        public virtual int Count => m_array.Count;

        public virtual void CopyTo(Array array, int index)
        {
            if (m_array != null)
            {
                m_array.CopyTo(array, index);
            }
        }

        public virtual object SyncRoot => this;

        public IEnumerator GetEnumerator()
        {
            return m_array.GetEnumerator();
        }

        public virtual bool IsReadOnly => false;

        object IList.this[int index]
        {
            get => this[index];
            set => this[index] = value;
        }

        public virtual void RemoveAt(int index)
        {
            m_array.RemoveAt(index);
        }

        public virtual void Insert(int index, object value)
        {
            ValidateElement(value);
            m_array.Insert(index, value);
        }

        public virtual void Remove(object value)
        {
            m_array.Remove(value);
        }

        public virtual bool Contains(object value)
        {
            return m_array.Contains(value);
        }

        public virtual void Clear()
        {
            m_array.Clear();
        }

        public virtual int IndexOf(object value)
        {
            return m_array.IndexOf(value);
        }

        public virtual int Add(object value)
        {
            ValidateElement(value);
            return m_array.Add(value);
        }

        public virtual bool IsFixedSize => false;

        public virtual object Clone()
        {
            WriteableCollection writeableCollection = (WriteableCollection)MemberwiseClone();
            writeableCollection.m_array = new ArrayList();
            for (int i = 0; i < m_array.Count; i++)
            {
                writeableCollection.Add(Convert.Clone(m_array[i]));
            }

            return writeableCollection;
        }

        protected const string INVALID_VALUE = "The value '{0}' cannot be added to the collection.";

        protected const string INVALID_TYPE = "A value with type '{0}' cannot be added to the collection.";

        private ArrayList m_array;

        private System.Type m_elementType;

        private class Names
        {
            internal const string COUNT = "CT";

            internal const string ELEMENT = "EL";

            internal const string ELEMENT_TYPE = "ET";
        }
    }
}