using System;

namespace Opc.Ae
{
    [Serializable]
    public class BrowseElement
    {
        public string Name
        {
            get { return this.m_name; }
            set { this.m_name = value; }
        }

        public string QualifiedName
        {
            get { return this.m_qualifiedName; }
            set { this.m_qualifiedName = value; }
        }

        public BrowseType NodeType
        {
            get { return this.m_nodeType; }
            set { this.m_nodeType = value; }
        }

        public virtual object Clone()
        {
            return base.MemberwiseClone();
        }

        private string m_name;

        private string m_qualifiedName;

        private BrowseType m_nodeType;
    }
}