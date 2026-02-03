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
            this.Init(collection);
        }

        public Attribute this[int index]
        {
            get { return this.m_attributes[index]; }
            set { this.m_attributes[index] = value; }
        }

        public Attribute Find(int id)
        {
            foreach (Attribute attribute in this.m_attributes)
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
            this.Clear();
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

                this.m_attributes = (Attribute[])arrayList.ToArray(typeof(Attribute));
            }
        }

        public void Clear()
        {
            this.m_attributes = new Attribute[0];
        }

        public virtual object Clone()
        {
            return new AttributeCollection(this);
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public int Count
        {
            get
            {
                if (this.m_attributes == null)
                {
                    return 0;
                }

                return this.m_attributes.Length;
            }
        }

        public void CopyTo(Array array, int index)
        {
            if (this.m_attributes != null)
            {
                this.m_attributes.CopyTo(array, index);
            }
        }

        public void CopyTo(Attribute[] array, int index)
        {
            this.CopyTo(array, index);
        }

        public object SyncRoot
        {
            get { return this; }
        }

        public IEnumerator GetEnumerator()
        {
            return this.m_attributes.GetEnumerator();
        }

        private Attribute[] m_attributes = new Attribute[0];
    }
}