using System;

namespace Opc.Hda
{
    public class BrowseElement : ItemIdentifier
    {
        public string Name
        {
            get { return this.m_name; }
            set { this.m_name = value; }
        }

        public bool IsItem
        {
            get { return this.m_isItem; }
            set { this.m_isItem = value; }
        }

        public bool HasChildren
        {
            get { return this.m_hasChildren; }
            set { this.m_hasChildren = value; }
        }

        public AttributeValueCollection Attributes
        {
            get { return this.m_attributes; }
            set { this.m_attributes = value; }
        }

        public override object Clone()
        {
            BrowseElement browseElement = (BrowseElement)base.MemberwiseClone();
            browseElement.Attributes = (AttributeValueCollection)this.m_attributes.Clone();
            return browseElement;
        }

        private string m_name;

        private bool m_isItem;

        private bool m_hasChildren;

        private AttributeValueCollection m_attributes = new AttributeValueCollection();
    }
}