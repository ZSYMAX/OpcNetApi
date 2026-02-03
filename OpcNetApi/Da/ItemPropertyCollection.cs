using System;
using System.Collections;

namespace Opc.Da
{
    [Serializable]
    public class ItemPropertyCollection : ArrayList, IResult
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

        public ItemProperty this[int index]
        {
            get => (ItemProperty)base[index];
            set => base[index] = value;
        }

        public ItemPropertyCollection()
        {
        }

        public ItemPropertyCollection(ItemIdentifier itemID)
        {
            if (itemID != null)
            {
                m_itemName = itemID.ItemName;
                m_itemPath = itemID.ItemPath;
            }
        }

        public ItemPropertyCollection(ItemIdentifier itemID, ResultID resultID)
        {
            if (itemID != null)
            {
                m_itemName = itemID.ItemName;
                m_itemPath = itemID.ItemPath;
            }

            ResultID = resultID;
        }

        public ResultID ResultID
        {
            get => m_resultID;
            set => m_resultID = value;
        }

        public string DiagnosticInfo
        {
            get => m_diagnosticInfo;
            set => m_diagnosticInfo = value;
        }

        public void CopyTo(ItemProperty[] array, int index)
        {
            CopyTo(array, index);
        }

        public void Insert(int index, ItemProperty value)
        {
            Insert(index, value);
        }

        public void Remove(ItemProperty value)
        {
            Remove(value);
        }

        public bool Contains(ItemProperty value)
        {
            return Contains(value);
        }

        public int IndexOf(ItemProperty value)
        {
            return IndexOf(value);
        }

        public int Add(ItemProperty value)
        {
            return Add(value);
        }

        private string m_itemName;

        private string m_itemPath;

        private ResultID m_resultID = ResultID.S_OK;

        private string m_diagnosticInfo;
    }
}