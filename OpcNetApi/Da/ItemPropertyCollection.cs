using System;
using System.Collections;

namespace Opc.Da
{
    [Serializable]
    public class ItemPropertyCollection : ArrayList, IResult
    {
        public string ItemName
        {
            get { return this.m_itemName; }
            set { this.m_itemName = value; }
        }

        public string ItemPath
        {
            get { return this.m_itemPath; }
            set { this.m_itemPath = value; }
        }

        public ItemProperty this[int index]
        {
            get { return (ItemProperty)base[index]; }
            set { base[index] = value; }
        }

        public ItemPropertyCollection()
        {
        }

        public ItemPropertyCollection(ItemIdentifier itemID)
        {
            if (itemID != null)
            {
                this.m_itemName = itemID.ItemName;
                this.m_itemPath = itemID.ItemPath;
            }
        }

        public ItemPropertyCollection(ItemIdentifier itemID, ResultID resultID)
        {
            if (itemID != null)
            {
                this.m_itemName = itemID.ItemName;
                this.m_itemPath = itemID.ItemPath;
            }

            this.ResultID = resultID;
        }

        public ResultID ResultID
        {
            get { return this.m_resultID; }
            set { this.m_resultID = value; }
        }

        public string DiagnosticInfo
        {
            get { return this.m_diagnosticInfo; }
            set { this.m_diagnosticInfo = value; }
        }

        public void CopyTo(ItemProperty[] array, int index)
        {
            this.CopyTo(array, index);
        }

        public void Insert(int index, ItemProperty value)
        {
            this.Insert(index, value);
        }

        public void Remove(ItemProperty value)
        {
            this.Remove(value);
        }

        public bool Contains(ItemProperty value)
        {
            return this.Contains(value);
        }

        public int IndexOf(ItemProperty value)
        {
            return this.IndexOf(value);
        }

        public int Add(ItemProperty value)
        {
            return this.Add(value);
        }

        private string m_itemName;

        private string m_itemPath;

        private ResultID m_resultID = ResultID.S_OK;

        private string m_diagnosticInfo;
    }
}