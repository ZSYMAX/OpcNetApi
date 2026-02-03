using System;
using System.Collections;
using System.Runtime.Serialization;

namespace Opc
{
    [Serializable]
    public class WriteableDictionary : IDictionary, ICollection, IEnumerable, ISerializable
    {
        protected WriteableDictionary(IDictionary dictionary, System.Type keyType, System.Type valueType)
        {
            this.m_keyType = ((keyType == null) ? typeof(object) : keyType);
            this.m_valueType = ((valueType == null) ? typeof(object) : valueType);
            this.Dictionary = dictionary;
        }

        protected virtual IDictionary Dictionary
        {
            get { return this.m_dictionary; }
            set
            {
                if (value != null)
                {
                    if (this.m_keyType != null)
                    {
                        foreach (object element in value.Keys)
                        {
                            this.ValidateKey(element, this.m_keyType);
                        }
                    }

                    if (this.m_valueType != null)
                    {
                        foreach (object element2 in value.Values)
                        {
                            this.ValidateValue(element2, this.m_valueType);
                        }
                    }

                    this.m_dictionary = new Hashtable(value);
                    return;
                }

                this.m_dictionary = new Hashtable();
            }
        }

        protected System.Type KeyType
        {
            get { return this.m_keyType; }
            set
            {
                foreach (object element in this.m_dictionary.Keys)
                {
                    this.ValidateKey(element, value);
                }

                this.m_keyType = value;
            }
        }

        protected System.Type ValueType
        {
            get { return this.m_valueType; }
            set
            {
                foreach (object element in this.m_dictionary.Values)
                {
                    this.ValidateValue(element, value);
                }

                this.m_valueType = value;
            }
        }

        protected virtual void ValidateKey(object element, System.Type type)
        {
            if (element == null)
            {
                throw new ArgumentException(string.Format("The {1} '{0}' cannot be added to the dictionary.", element, "key"));
            }

            if (!type.IsInstanceOfType(element))
            {
                throw new ArgumentException(string.Format("A {1} with type '{0}' cannot be added to the dictionary.", element.GetType(), "key"));
            }
        }

        protected virtual void ValidateValue(object element, System.Type type)
        {
            if (element != null && !type.IsInstanceOfType(element))
            {
                throw new ArgumentException(string.Format("A {1} with type '{0}' cannot be added to the dictionary.", element.GetType(), "value"));
            }
        }

        protected WriteableDictionary(SerializationInfo info, StreamingContext context)
        {
            this.m_keyType = (System.Type)info.GetValue("KT", typeof(Type));
            this.m_valueType = (System.Type)info.GetValue("VT", typeof(Type));
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
            info.AddValue("KT", this.m_keyType);
            info.AddValue("VT", this.m_valueType);
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
            get { return false; }
        }

        public virtual IDictionaryEnumerator GetEnumerator()
        {
            return this.m_dictionary.GetEnumerator();
        }

        public virtual object this[object key]
        {
            get { return this.m_dictionary[key]; }
            set
            {
                this.ValidateKey(key, this.m_keyType);
                this.ValidateValue(value, this.m_valueType);
                this.m_dictionary[key] = value;
            }
        }

        public virtual void Remove(object key)
        {
            this.m_dictionary.Remove(key);
        }

        public virtual bool Contains(object key)
        {
            return this.m_dictionary.Contains(key);
        }

        public virtual void Clear()
        {
            this.m_dictionary.Clear();
        }

        public virtual ICollection Values
        {
            get { return this.m_dictionary.Values; }
        }

        public virtual void Add(object key, object value)
        {
            this.ValidateKey(key, this.m_keyType);
            this.ValidateValue(value, this.m_valueType);
            this.m_dictionary.Add(key, value);
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
            WriteableDictionary writeableDictionary = (WriteableDictionary)base.MemberwiseClone();
            Hashtable hashtable = new Hashtable();
            IDictionaryEnumerator enumerator = this.m_dictionary.GetEnumerator();
            while (enumerator.MoveNext())
            {
                hashtable.Add(Convert.Clone(enumerator.Key), Convert.Clone(enumerator.Value));
            }

            writeableDictionary.m_dictionary = hashtable;
            return writeableDictionary;
        }

        protected const string INVALID_VALUE = "The {1} '{0}' cannot be added to the dictionary.";

        protected const string INVALID_TYPE = "A {1} with type '{0}' cannot be added to the dictionary.";

        private Hashtable m_dictionary = new Hashtable();

        private System.Type m_keyType;

        private System.Type m_valueType;

        private class Names
        {
            internal const string COUNT = "CT";

            internal const string KEY = "KY";

            internal const string VALUE = "VA";

            internal const string KEY_TYPE = "KT";

            internal const string VALUE_VALUE = "VT";
        }
    }
}