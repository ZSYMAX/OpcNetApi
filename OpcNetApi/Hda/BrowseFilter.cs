using System;

namespace Opc.Hda
{
    [Serializable]
    public class BrowseFilter : ICloneable
    {
        public int AttributeID
        {
            get { return this.m_attributeID; }
            set { this.m_attributeID = value; }
        }

        public Operator Operator
        {
            get { return this.m_operator; }
            set { this.m_operator = value; }
        }

        public object FilterValue
        {
            get { return this.m_filterValue; }
            set { this.m_filterValue = value; }
        }

        public virtual object Clone()
        {
            BrowseFilter browseFilter = (BrowseFilter)base.MemberwiseClone();
            browseFilter.FilterValue = Convert.Clone(this.FilterValue);
            return browseFilter;
        }

        private int m_attributeID;

        private Operator m_operator = Operator.Equal;

        private object m_filterValue;
    }
}