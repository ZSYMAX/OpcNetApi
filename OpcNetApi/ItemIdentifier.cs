using System;
using System.Text;

namespace Opc
{
    [Serializable]
    public class ItemIdentifier : ICloneable
    {
        public string ItemName
        {
            get => m_itemName;
            set => m_itemName = value;
        }

        public string ItemPath
        {
            get => m_itemPath;
            set => m_itemPath = value;
        }

        public object ClientHandle
        {
            get => m_clientHandle;
            set => m_clientHandle = value;
        }

        public object ServerHandle
        {
            get => m_serverHandle;
            set => m_serverHandle = value;
        }

        public string Key => new StringBuilder(64).Append((ItemName == null) ? "null" : ItemName).Append("\r\n").Append((ItemPath == null) ? "null" : ItemPath).ToString();

        public ItemIdentifier()
        {
        }

        public ItemIdentifier(string itemName)
        {
            ItemPath = null;
            ItemName = itemName;
        }

        public ItemIdentifier(string itemPath, string itemName)
        {
            ItemPath = itemPath;
            ItemName = itemName;
        }

        public ItemIdentifier(ItemIdentifier itemID)
        {
            if (itemID != null)
            {
                ItemPath = itemID.ItemPath;
                ItemName = itemID.ItemName;
                ClientHandle = itemID.ClientHandle;
                ServerHandle = itemID.ServerHandle;
            }
        }

        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        private string m_itemName;

        private string m_itemPath;

        private object m_clientHandle;

        private object m_serverHandle;
    }
}