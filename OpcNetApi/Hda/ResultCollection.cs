using System;
using System.Collections;

namespace Opc.Hda
{
    [Serializable]
    public class ResultCollection : ItemIdentifier, ICloneable, IList, ICollection, IEnumerable
    {
        public Result this[int index]
        {
            get { return (Result)this.m_results[index]; }
            set { this.m_results[index] = value; }
        }

        public ResultCollection()
        {
        }

        public ResultCollection(ItemIdentifier item) : base(item)
        {
        }

        public ResultCollection(ResultCollection item) : base(item)
        {
            this.m_results = new ArrayList(item.m_results.Count);
            foreach (object obj in item.m_results)
            {
                Result result = (Result)obj;
                this.m_results.Add(result.Clone());
            }
        }

        public override object Clone()
        {
            ResultCollection resultCollection = (ResultCollection)base.Clone();
            resultCollection.m_results = new ArrayList(this.m_results.Count);
            foreach (object obj in this.m_results)
            {
                ResultCollection resultCollection2 = (ResultCollection)obj;
                resultCollection.m_results.Add(resultCollection2.Clone());
            }

            return resultCollection;
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public int Count
        {
            get
            {
                if (this.m_results == null)
                {
                    return 0;
                }

                return this.m_results.Count;
            }
        }

        public void CopyTo(Array array, int index)
        {
            if (this.m_results != null)
            {
                this.m_results.CopyTo(array, index);
            }
        }

        public void CopyTo(Result[] array, int index)
        {
            this.CopyTo(array, index);
        }

        public object SyncRoot
        {
            get { return this; }
        }

        public IEnumerator GetEnumerator()
        {
            return this.m_results.GetEnumerator();
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        object IList.this[int index]
        {
            get { return this.m_results[index]; }
            set
            {
                if (!typeof(Result).IsInstanceOfType(value))
                {
                    throw new ArgumentException("May only add Result objects into the collection.");
                }

                this.m_results[index] = value;
            }
        }

        public void RemoveAt(int index)
        {
            this.m_results.RemoveAt(index);
        }

        public void Insert(int index, object value)
        {
            if (!typeof(Result).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only add Result objects into the collection.");
            }

            this.m_results.Insert(index, value);
        }

        public void Remove(object value)
        {
            this.m_results.Remove(value);
        }

        public bool Contains(object value)
        {
            return this.m_results.Contains(value);
        }

        public void Clear()
        {
            this.m_results.Clear();
        }

        public int IndexOf(object value)
        {
            return this.m_results.IndexOf(value);
        }

        public int Add(object value)
        {
            if (!typeof(Result).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only add Result objects into the collection.");
            }

            return this.m_results.Add(value);
        }

        public bool IsFixedSize
        {
            get { return false; }
        }

        public void Insert(int index, Result value)
        {
            this.Insert(index, value);
        }

        public void Remove(Result value)
        {
            this.Remove(value);
        }

        public bool Contains(Result value)
        {
            return this.Contains(value);
        }

        public int IndexOf(Result value)
        {
            return this.IndexOf(value);
        }

        public int Add(Result value)
        {
            return this.Add(value);
        }

        private ArrayList m_results = new ArrayList();
    }
}