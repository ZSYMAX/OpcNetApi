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
            Init(collection);
        }

        public BrowseFilter this[int index]
        {
            get => m_filters[index];
            set => m_filters[index] = value;
        }

        public BrowseFilter Find(int id)
        {
            foreach (BrowseFilter browseFilter in m_filters)
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
            Clear();
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

                m_filters = (BrowseFilter[])arrayList.ToArray(typeof(BrowseFilter));
            }
        }

        public void Clear()
        {
            m_filters = new BrowseFilter[0];
        }

        public override object Clone()
        {
            return new BrowseFilterCollection(this);
        }

        public bool IsSynchronized => false;

        public int Count
        {
            get
            {
                if (m_filters == null)
                {
                    return 0;
                }

                return m_filters.Length;
            }
        }

        public void CopyTo(Array array, int index)
        {
            if (m_filters != null)
            {
                m_filters.CopyTo(array, index);
            }
        }

        public void CopyTo(BrowseFilter[] array, int index)
        {
            CopyTo(array, index);
        }

        public object SyncRoot => this;

        public IEnumerator GetEnumerator()
        {
            return m_filters.GetEnumerator();
        }

        private BrowseFilter[] m_filters = new BrowseFilter[0];
    }
}