using System;

namespace Opc.Ae
{
    [Serializable]
    public class BrowseElement
    {
        public string Name
        {
            get => m_name;
            set => m_name = value;
        }

        public string QualifiedName
        {
            get => m_qualifiedName;
            set => m_qualifiedName = value;
        }

        public BrowseType NodeType
        {
            get => m_nodeType;
            set => m_nodeType = value;
        }

        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        private string m_name;

        private string m_qualifiedName;

        private BrowseType m_nodeType;
    }
}