using System;
using System.Collections;

namespace Opc.Hda
{
    [Serializable]
    public class TrendCollection : ICloneable, IList, ICollection, IEnumerable
    {
        public Trend this[int index]
        {
            get { return (Trend)this.m_trends[index]; }
        }

        public Trend this[string name]
        {
            get
            {
                foreach (object obj in this.m_trends)
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
                    this.Add(value);
                }
            }
        }

        public virtual object Clone()
        {
            TrendCollection trendCollection = (TrendCollection)base.MemberwiseClone();
            trendCollection.m_trends = new ArrayList();
            foreach (object obj in this.m_trends)
            {
                Trend trend = (Trend)obj;
                trendCollection.m_trends.Add(trend.Clone());
            }

            return trendCollection;
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public int Count
        {
            get
            {
                if (this.m_trends == null)
                {
                    return 0;
                }

                return this.m_trends.Count;
            }
        }

        public void CopyTo(Array array, int index)
        {
            if (this.m_trends != null)
            {
                this.m_trends.CopyTo(array, index);
            }
        }

        public void CopyTo(Trend[] array, int index)
        {
            this.CopyTo(array, index);
        }

        public object SyncRoot
        {
            get { return this; }
        }

        public IEnumerator GetEnumerator()
        {
            return this.m_trends.GetEnumerator();
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        object IList.this[int index]
        {
            get { return this.m_trends[index]; }
            set
            {
                if (!typeof(Trend).IsInstanceOfType(value))
                {
                    throw new ArgumentException("May only add Trend objects into the collection.");
                }

                this.m_trends[index] = value;
            }
        }

        public void RemoveAt(int index)
        {
            this.m_trends.RemoveAt(index);
        }

        public void Insert(int index, object value)
        {
            if (!typeof(Trend).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only add Trend objects into the collection.");
            }

            this.m_trends.Insert(index, value);
        }

        public void Remove(object value)
        {
            this.m_trends.Remove(value);
        }

        public bool Contains(object value)
        {
            return this.m_trends.Contains(value);
        }

        public void Clear()
        {
            this.m_trends.Clear();
        }

        public int IndexOf(object value)
        {
            return this.m_trends.IndexOf(value);
        }

        public int Add(object value)
        {
            if (!typeof(Trend).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only add Trend objects into the collection.");
            }

            return this.m_trends.Add(value);
        }

        public bool IsFixedSize
        {
            get { return false; }
        }

        public void Insert(int index, Trend value)
        {
            this.Insert(index, value);
        }

        public void Remove(Trend value)
        {
            this.Remove(value);
        }

        public bool Contains(Trend value)
        {
            return this.Contains(value);
        }

        public int IndexOf(Trend value)
        {
            return this.IndexOf(value);
        }

        public int Add(Trend value)
        {
            return this.Add(value);
        }

        private ArrayList m_trends = new ArrayList();
    }
}