using System;

namespace Opc.Hda
{
    public class BrowseElement : ItemIdentifier
    {
        public string Name
        {
            get => m_name;
            set => m_name = value;
        }

        public bool IsItem
        {
            get => m_isItem;
            set => m_isItem = value;
        }

        public bool HasChildren
        {
            get => m_hasChildren;
            set => m_hasChildren = value;
        }

        public AttributeValueCollection Attributes
        {
            get => m_attributes;
            set => m_attributes = value;
        }

        public override object Clone()
        {
            BrowseElement browseElement = (BrowseElement)MemberwiseClone();
            browseElement.Attributes = (AttributeValueCollection)m_attributes.Clone();
            return browseElement;
        }

        private string m_name;

        private bool m_isItem;

        private bool m_hasChildren;

        private AttributeValueCollection m_attributes = new AttributeValueCollection();
    }
}