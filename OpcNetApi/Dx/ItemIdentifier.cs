using System;

namespace Opc.Dx
{
    [Serializable]
    public class ItemIdentifier : Opc.ItemIdentifier
    {
        public string Version
        {
            get => m_version;
            set => m_version = value;
        }

        public ItemIdentifier()
        {
        }

        public ItemIdentifier(string itemName) : base(itemName)
        {
        }

        public ItemIdentifier(string itemPath, string itemName) : base(itemPath, itemName)
        {
        }

        public ItemIdentifier(Opc.ItemIdentifier item) : base(item)
        {
        }

        public ItemIdentifier(ItemIdentifier item) : base(item)
        {
            if (item != null)
            {
                m_version = item.m_version;
            }
        }

        public override bool Equals(object target)
        {
            if (typeof(ItemIdentifier).IsInstanceOfType(target))
            {
                ItemIdentifier itemIdentifier = (ItemIdentifier)target;
                return itemIdentifier.ItemName == ItemName && itemIdentifier.ItemPath == ItemPath && itemIdentifier.Version == Version;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        private string m_version;
    }
}