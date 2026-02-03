using System;
using System.Collections;

namespace Opc.Hda
{
    [Serializable]
    public class AttributeCollection : ICloneable, ICollection, IEnumerable
    {
        public AttributeCollection()
        {
        }

        public AttributeCollection(ICollection collection)
        {
            Init(collection);
        }

        public Attribute this[int index]
        {
            get => m_attributes[index];
            set => m_attributes[index] = value;
        }

        public Attribute Find(int id)
        {
            foreach (Attribute attribute in m_attributes)
            {
                if (attribute.ID == id)
                {
                    return attribute;
                }
            }

            return null;
        }

        public void Init(ICollection collection)
        {
            Clear();
            if (collection != null)
            {
                ArrayList arrayList = new ArrayList(collection.Count);
                foreach (object obj in collection)
                {
                    if (obj.GetType() == typeof(Attribute))
                    {
                        arrayList.Add(Convert.Clone(obj));
                    }
                }

                m_attributes = (Attribute[])arrayList.ToArray(typeof(Attribute));
            }
        }

        public void Clear()
        {
            m_attributes = new Attribute[0];
        }

        public virtual object Clone()
        {
            return new AttributeCollection(this);
        }

        public bool IsSynchronized => false;

        public int Count
        {
            get
            {
                if (m_attributes == null)
                {
                    return 0;
                }

                return m_attributes.Length;
            }
        }

        public void CopyTo(Array array, int index)
        {
            if (m_attributes != null)
            {
                m_attributes.CopyTo(array, index);
            }
        }

        public void CopyTo(Attribute[] array, int index)
        {
            CopyTo(array, index);
        }

        public object SyncRoot => this;

        public IEnumerator GetEnumerator()
        {
            return m_attributes.GetEnumerator();
        }

        private Attribute[] m_attributes = new Attribute[0];
    }
}