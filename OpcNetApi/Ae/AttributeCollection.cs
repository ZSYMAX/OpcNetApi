using System;

namespace Opc.Ae
{
    [Serializable]
    public class AttributeCollection : WriteableCollection
    {
        public int this[int index]
        {
            get { return (int)this.Array[index]; }
        }

        public new int[] ToArray()
        {
            return (int[])this.Array.ToArray(typeof(int));
        }

        internal AttributeCollection() : base(null, typeof(int))
        {
        }

        internal AttributeCollection(int[] attributeIDs) : base(attributeIDs, typeof(int))
        {
        }
    }
}