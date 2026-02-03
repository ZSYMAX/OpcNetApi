using System;
using System.Collections;

namespace Opc.Dx
{
    [Serializable]
    public class BrowsePathCollection : ArrayList
    {
        public string this[int index]
        {
            get => this[index];
            set => this[index] = value;
        }

        public new string[] ToArray()
        {
            return (string[])ToArray(typeof(string));
        }

        public int Add(string browsePath)
        {
            return base.Add(browsePath);
        }

        public void Insert(int index, string browsePath)
        {
            if (browsePath == null)
            {
                throw new ArgumentNullException("browsePath");
            }

            base.Insert(index, browsePath);
        }

        public BrowsePathCollection()
        {
        }

        public BrowsePathCollection(ICollection browsePaths)
        {
            if (browsePaths != null)
            {
                foreach (object obj in browsePaths)
                {
                    string browsePath = (string)obj;
                    Add(browsePath);
                }
            }
        }
    }
}