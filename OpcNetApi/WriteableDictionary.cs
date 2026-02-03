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
            m_keyType = ((keyType == null) ? typeof(object) : keyType);
            m_valueType = ((valueType == null) ? typeof(object) : valueType);
            Dictionary = dictionary;
        }

        protected virtual IDictionary Dictionary
        {
            get => m_dictionary;
            set
            {
                if (value != null)
                {
                    if (m_keyType != null)
                    {
                        foreach (object element in value.Keys)
                        {
                            ValidateKey(element, m_keyType);
                        }
                    }

                    if (m_valueType != null)
                    {
                        foreach (object element2 in value.Values)
                        {
                            ValidateValue(element2, m_valueType);
                        }
                    }

                    m_dictionary = new Hashtable(value);
                    return;
                }

                m_dictionary = new Hashtable();
            }
        }

        protected System.Type KeyType
        {
            get => m_keyType;
            set
            {
                foreach (object element in m_dictionary.Keys)
                {
                    ValidateKey(element, value);
                }

                m_keyType = value;
            }
        }

        protected System.Type ValueType
        {
            get => m_valueType;
            set
            {
                foreach (object element in m_dictionary.Values)
                {
                    ValidateValue(element, value);
                }

                m_valueType = value;
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
            m_keyType = (System.Type)info.GetValue("KT", typeof(Type));
            m_valueType = (System.Type)info.GetValue("VT", typeof(Type));
            int num = (int)info.GetValue("CT", typeof(int));
            m_dictionary = new Hashtable();
            for (int i = 0; i < num; i++)
            {
                object value = info.GetValue("KY" + i.ToString(), typeof(object));
                object value2 = info.GetValue("VA" + i.ToString(), typeof(object));
                if (value != null)
                {
                    m_dictionary[value] = value2;
                }
            }
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("KT", m_keyType);
            info.AddValue("VT", m_valueType);
            info.AddValue("CT", m_dictionary.Count);
            int num = 0;
            IDictionaryEnumerator enumerator = m_dictionary.GetEnumerator();
            while (enumerator.MoveNext())
            {
                info.AddValue("KY" + num.ToString(), enumerator.Key);
                info.AddValue("VA" + num.ToString(), enumerator.Value);
                num++;
            }
        }

        public virtual bool IsReadOnly => false;

        public virtual IDictionaryEnumerator GetEnumerator()
        {
            return m_dictionary.GetEnumerator();
        }

        public virtual object this[object key]
        {
            get => m_dictionary[key];
            set
            {
                ValidateKey(key, m_keyType);
                ValidateValue(value, m_valueType);
                m_dictionary[key] = value;
            }
        }

        public virtual void Remove(object key)
        {
            m_dictionary.Remove(key);
        }

        public virtual bool Contains(object key)
        {
            return m_dictionary.Contains(key);
        }

        public virtual void Clear()
        {
            m_dictionary.Clear();
        }

        public virtual ICollection Values => m_dictionary.Values;

        public virtual void Add(object key, object value)
        {
            ValidateKey(key, m_keyType);
            ValidateValue(value, m_valueType);
            m_dictionary.Add(key, value);
        }

        public virtual ICollection Keys => m_dictionary.Keys;

        public virtual bool IsFixedSize => false;

        public virtual bool IsSynchronized => false;

        public virtual int Count => m_dictionary.Count;

        public virtual void CopyTo(Array array, int index)
        {
            if (m_dictionary != null)
            {
                m_dictionary.CopyTo(array, index);
            }
        }

        public virtual object SyncRoot => this;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public virtual object Clone()
        {
            WriteableDictionary writeableDictionary = (WriteableDictionary)MemberwiseClone();
            Hashtable hashtable = new Hashtable();
            IDictionaryEnumerator enumerator = m_dictionary.GetEnumerator();
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