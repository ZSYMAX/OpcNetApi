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
            get { return this.m_array[index]; }
            set { this.m_array[index] = value; }
        }

        public virtual Array ToArray()
        {
            return this.m_array.ToArray(this.m_elementType);
        }

        public virtual void AddRange(ICollection collection)
        {
            if (collection != null)
            {
                foreach (object element in collection)
                {
                    this.ValidateElement(element);
                }

                this.m_array.AddRange(collection);
            }
        }

        protected WriteableCollection(ICollection array, System.Type elementType)
        {
            if (array != null)
            {
                this.m_array = new ArrayList(array);
            }
            else
            {
                this.m_array = new ArrayList();
            }

            this.m_elementType = typeof(object);
            if (elementType != null)
            {
                foreach (object element in this.m_array)
                {
                    this.ValidateElement(element);
                }

                this.m_elementType = elementType;
            }
        }

        protected virtual ArrayList Array
        {
            get { return this.m_array; }
            set
            {
                this.m_array = value;
                if (this.m_array == null)
                {
                    this.m_array = new ArrayList();
                }
            }
        }

        protected virtual System.Type ElementType
        {
            get { return this.m_elementType; }
            set
            {
                foreach (object element in this.m_array)
                {
                    this.ValidateElement(element);
                }

                this.m_elementType = value;
            }
        }

        protected virtual void ValidateElement(object element)
        {
            if (element == null)
            {
                throw new ArgumentException(string.Format("The value '{0}' cannot be added to the collection.", element));
            }

            if (!this.m_elementType.IsInstanceOfType(element))
            {
                throw new ArgumentException(string.Format("A value with type '{0}' cannot be added to the collection.", element.GetType()));
            }
        }

        protected WriteableCollection(SerializationInfo info, StreamingContext context)
        {
            this.m_elementType = (System.Type)info.GetValue("ET", typeof(Type));
            int num = (int)info.GetValue("CT", typeof(int));
            this.m_array = new ArrayList(num);
            for (int i = 0; i < num; i++)
            {
                this.m_array.Add(info.GetValue("EL" + i.ToString(), typeof(object)));
            }
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ET", this.m_elementType);
            info.AddValue("CT", this.m_array.Count);
            for (int i = 0; i < this.m_array.Count; i++)
            {
                info.AddValue("EL" + i.ToString(), this.m_array[i]);
            }
        }

        public virtual bool IsSynchronized
        {
            get { return false; }
        }

        public virtual int Count
        {
            get { return this.m_array.Count; }
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

        public IEnumerator GetEnumerator()
        {
            return this.m_array.GetEnumerator();
        }

        public virtual bool IsReadOnly
        {
            get { return false; }
        }

        object IList.this[int index]
        {
            get { return this[index]; }
            set { this[index] = value; }
        }

        public virtual void RemoveAt(int index)
        {
            this.m_array.RemoveAt(index);
        }

        public virtual void Insert(int index, object value)
        {
            this.ValidateElement(value);
            this.m_array.Insert(index, value);
        }

        public virtual void Remove(object value)
        {
            this.m_array.Remove(value);
        }

        public virtual bool Contains(object value)
        {
            return this.m_array.Contains(value);
        }

        public virtual void Clear()
        {
            this.m_array.Clear();
        }

        public virtual int IndexOf(object value)
        {
            return this.m_array.IndexOf(value);
        }

        public virtual int Add(object value)
        {
            this.ValidateElement(value);
            return this.m_array.Add(value);
        }

        public virtual bool IsFixedSize
        {
            get { return false; }
        }

        public virtual object Clone()
        {
            WriteableCollection writeableCollection = (WriteableCollection)base.MemberwiseClone();
            writeableCollection.m_array = new ArrayList();
            for (int i = 0; i < this.m_array.Count; i++)
            {
                writeableCollection.Add(Convert.Clone(this.m_array[i]));
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