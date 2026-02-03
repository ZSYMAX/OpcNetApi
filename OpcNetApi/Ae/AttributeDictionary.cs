using System;

namespace Opc.Ae
{
    [Serializable]
    public class AttributeDictionary : WriteableDictionary
    {
        public AttributeCollection this[int categoryID]
        {
            get { return (AttributeCollection)base[categoryID]; }
            set
            {
                if (value != null)
                {
                    base[categoryID] = value;
                    return;
                }

                base[categoryID] = new AttributeCollection();
            }
        }

        public virtual void Add(int key, int[] value)
        {
            if (value != null)
            {
                base.Add(key, new AttributeCollection(value));
                return;
            }

            base.Add(key, new AttributeCollection());
        }

        public AttributeDictionary() : base(null, typeof(int), typeof(AttributeCollection))
        {
        }

        public AttributeDictionary(int[] categoryIDs) : base(null, typeof(int), typeof(AttributeCollection))
        {
            for (int i = 0; i < categoryIDs.Length; i++)
            {
                this.Add(categoryIDs[i], null);
            }
        }
    }
}