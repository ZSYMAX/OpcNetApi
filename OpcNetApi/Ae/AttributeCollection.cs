using System;

namespace Opc.Ae
{
    [Serializable]
    public class AttributeCollection : WriteableCollection
    {
        public int this[int index] => (int)Array[index];

        public new int[] ToArray()
        {
            return (int[])Array.ToArray(typeof(int));
        }

        internal AttributeCollection() : base(null, typeof(int))
        {
        }

        internal AttributeCollection(int[] attributeIDs) : base(attributeIDs, typeof(int))
        {
        }
    }
}