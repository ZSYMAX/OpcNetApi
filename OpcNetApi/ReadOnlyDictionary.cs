using System;
using System.Collections;
using System.Runtime.Serialization;

namespace Opc
{
    [Serializable]
    public class ReadOnlyDictionary : IDictionary, ICollection, IEnumerable, ISerializable
    {
        protected ReadOnlyDictionary(Hashtable dictionary)
        {
            this.Dictionary = dictionary;
        }

        protected virtual Hashtable Dictionary
        {
            get { return this.m_dictionary; }
            set
            {
                this.m_dictionary = value;
                if (this.m_dictionary == null)
                {
                    this.m_dictionary = new Hashtable();
                }
            }
        }

        protected ReadOnlyDictionary(SerializationInfo info, StreamingContext context)
        {
            int num = (int)info.GetValue("CT", typeof(int));
            this.m_dictionary = new Hashtable();
            for (int i = 0; i < num; i++)
            {
                object value = info.GetValue("KY" + i.ToString(), typeof(object));
                object value2 = info.GetValue("VA" + i.ToString(), typeof(object));
                if (value != null)
                {
                    this.m_dictionary[value] = value2;
                }
            }
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("CT", this.m_dictionary.Count);
            int num = 0;
            IDictionaryEnumerator enumerator = this.m_dictionary.GetEnumerator();
            while (enumerator.MoveNext())
            {
                info.AddValue("KY" + num.ToString(), enumerator.Key);
                info.AddValue("VA" + num.ToString(), enumerator.Value);
                num++;
            }
        }

        public virtual bool IsReadOnly
        {
            get { return true; }
        }

        public virtual IDictionaryEnumerator GetEnumerator()
        {
            return this.m_dictionary.GetEnumerator();
        }

        public virtual object this[object key]
        {
            get { return this.m_dictionary[key]; }
            set { throw new InvalidOperationException("Cannot change the contents of a read-only dictionary"); }
        }

        public virtual void Remove(object key)
        {
            throw new InvalidOperationException("Cannot change the contents of a read-only dictionary");
        }

        public virtual bool Contains(object key)
        {
            return this.m_dictionary.Contains(key);
        }

        public virtual void Clear()
        {
            throw new InvalidOperationException("Cannot change the contents of a read-only dictionary");
        }

        public virtual ICollection Values
        {
            get { return this.m_dictionary.Values; }
        }

        public void Add(object key, object value)
        {
            throw new InvalidOperationException("Cannot change the contents of a read-only dictionary");
        }

        public virtual ICollection Keys
        {
            get { return this.m_dictionary.Keys; }
        }

        public virtual bool IsFixedSize
        {
            get { return false; }
        }

        public virtual bool IsSynchronized
        {
            get { return false; }
        }

        public virtual int Count
        {
            get { return this.m_dictionary.Count; }
        }

        public virtual void CopyTo(Array array, int index)
        {
            if (this.m_dictionary != null)
            {
                this.m_dictionary.CopyTo(array, index);
            }
        }

        public virtual object SyncRoot
        {
            get { return this; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public virtual object Clone()
        {
            ReadOnlyDictionary readOnlyDictionary = (ReadOnlyDictionary)base.MemberwiseClone();
            Hashtable hashtable = new Hashtable();
            IDictionaryEnumerator enumerator = this.m_dictionary.GetEnumerator();
            while (enumerator.MoveNext())
            {
                hashtable.Add(Convert.Clone(enumerator.Key), Convert.Clone(enumerator.Value));
            }

            readOnlyDictionary.m_dictionary = hashtable;
            return readOnlyDictionary;
        }

        private const string READ_ONLY_DICTIONARY = "Cannot change the contents of a read-only dictionary";

        private Hashtable m_dictionary = new Hashtable();

        private class Names
        {
            internal const string COUNT = "CT";

            internal const string KEY = "KY";

            internal const string VALUE = "VA";
        }
    }
}