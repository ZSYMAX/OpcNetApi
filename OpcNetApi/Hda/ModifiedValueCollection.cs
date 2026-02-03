using System;

namespace Opc.Hda
{
    [Serializable]
    public class ModifiedValueCollection : ItemValueCollection
    {
        public ModifiedValue this[int index]
        {
            get => this[index];
            set => this[index] = value;
        }

        public ModifiedValueCollection()
        {
        }

        public ModifiedValueCollection(ItemIdentifier item) : base(item)
        {
        }

        public ModifiedValueCollection(Item item) : base(item)
        {
        }

        public ModifiedValueCollection(ItemValueCollection item) : base(item)
        {
        }
    }
}