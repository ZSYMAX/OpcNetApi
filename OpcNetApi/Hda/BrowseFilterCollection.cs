using System;
using System.Collections;

namespace Opc.Hda
{
    [Serializable]
    public class BrowseFilterCollection : ItemIdentifier, ICollection, IEnumerable
    {
        public BrowseFilterCollection()
        {
        }

        public BrowseFilterCollection(ICollection collection)
        {
            this.Init(collection);
        }

        public BrowseFilter this[int index]
        {
            get { return this.m_filters[index]; }
            set { this.m_filters[index] = value; }
        }

        public BrowseFilter Find(int id)
        {
            foreach (BrowseFilter browseFilter in this.m_filters)
            {
                if (browseFilter.AttributeID == id)
                {
                    return browseFilter;
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
                    if (obj.GetType() == typeof(BrowseFilter))
                    {
                        arrayList.Add(Convert.Clone(obj));
                    }
                }

                this.m_filters = (BrowseFilter[])arrayList.ToArray(typeof(BrowseFilter));
            }
        }

        public void Clear()
        {
            this.m_filters = new BrowseFilter[0];
        }

        public override object Clone()
        {
            return new BrowseFilterCollection(this);
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public int Count
        {
            get
            {
                if (this.m_filters == null)
                {
                    return 0;
                }

                return this.m_filters.Length;
            }
        }

        public void CopyTo(Array array, int index)
        {
            if (this.m_filters != null)
            {
                this.m_filters.CopyTo(array, index);
            }
        }

        public void CopyTo(BrowseFilter[] array, int index)
        {
            this.CopyTo(array, index);
        }

        public object SyncRoot
        {
            get { return this; }
        }

        public IEnumerator GetEnumerator()
        {
            return this.m_filters.GetEnumerator();
        }

        private BrowseFilter[] m_filters = new BrowseFilter[0];
    }
}