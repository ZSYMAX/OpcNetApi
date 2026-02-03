using System;

namespace Opc.Hda
{
    [Serializable]
    public class BrowseFilter : ICloneable
    {
        public int AttributeID
        {
            get => m_attributeID;
            set => m_attributeID = value;
        }

        public Operator Operator
        {
            get => m_operator;
            set => m_operator = value;
        }

        public object FilterValue
        {
            get => m_filterValue;
            set => m_filterValue = value;
        }

        public virtual object Clone()
        {
            BrowseFilter browseFilter = (BrowseFilter)MemberwiseClone();
            browseFilter.FilterValue = Convert.Clone(FilterValue);
            return browseFilter;
        }

        private int m_attributeID;

        private Operator m_operator = Operator.Equal;

        private object m_filterValue;
    }
}