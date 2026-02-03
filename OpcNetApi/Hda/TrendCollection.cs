using System;
using System.Collections;

namespace Opc.Hda
{
    [Serializable]
    public class TrendCollection : ICloneable, IList, ICollection, IEnumerable
    {
        public Trend this[int index] => (Trend)m_trends[index];

        public Trend this[string name]
        {
            get
            {
                foreach (object obj in m_trends)
                {
                    Trend trend = (Trend)obj;
                    if (trend.Name == name)
                    {
                        return trend;
                    }
                }

                return null;
            }
        }

        public TrendCollection()
        {
        }

        public TrendCollection(TrendCollection items)
        {
            if (items != null)
            {
                foreach (object obj in items)
                {
                    Trend value = (Trend)obj;
                    Add(value);
                }
            }
        }

        public virtual object Clone()
        {
            TrendCollection trendCollection = (TrendCollection)MemberwiseClone();
            trendCollection.m_trends = new ArrayList();
            foreach (object obj in m_trends)
            {
                Trend trend = (Trend)obj;
                trendCollection.m_trends.Add(trend.Clone());
            }

            return trendCollection;
        }

        public bool IsSynchronized => false;

        public int Count
        {
            get
            {
                if (m_trends == null)
                {
                    return 0;
                }

                return m_trends.Count;
            }
        }

        public void CopyTo(Array array, int index)
        {
            if (m_trends != null)
            {
                m_trends.CopyTo(array, index);
            }
        }

        public void CopyTo(Trend[] array, int index)
        {
            CopyTo(array, index);
        }

        public object SyncRoot => this;

        public IEnumerator GetEnumerator()
        {
            return m_trends.GetEnumerator();
        }

        public bool IsReadOnly => false;

        object IList.this[int index]
        {
            get => m_trends[index];
            set
            {
                if (!typeof(Trend).IsInstanceOfType(value))
                {
                    throw new ArgumentException("May only add Trend objects into the collection.");
                }

                m_trends[index] = value;
            }
        }

        public void RemoveAt(int index)
        {
            m_trends.RemoveAt(index);
        }

        public void Insert(int index, object value)
        {
            if (!typeof(Trend).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only add Trend objects into the collection.");
            }

            m_trends.Insert(index, value);
        }

        public void Remove(object value)
        {
            m_trends.Remove(value);
        }

        public bool Contains(object value)
        {
            return m_trends.Contains(value);
        }

        public void Clear()
        {
            m_trends.Clear();
        }

        public int IndexOf(object value)
        {
            return m_trends.IndexOf(value);
        }

        public int Add(object value)
        {
            if (!typeof(Trend).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only add Trend objects into the collection.");
            }

            return m_trends.Add(value);
        }

        public bool IsFixedSize => false;

        public void Insert(int index, Trend value)
        {
            Insert(index, value);
        }

        public void Remove(Trend value)
        {
            Remove(value);
        }

        public bool Contains(Trend value)
        {
            return Contains(value);
        }

        public int IndexOf(Trend value)
        {
            return IndexOf(value);
        }

        public int Add(Trend value)
        {
            return Add(value);
        }

        private ArrayList m_trends = new ArrayList();
    }
}