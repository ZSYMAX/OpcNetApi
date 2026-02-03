using System;

namespace Opc.Da
{
    [Serializable]
    public class BrowseFilters : ICloneable
    {
        public int MaxElementsReturned
        {
            get { return this.m_maxElementsReturned; }
            set { this.m_maxElementsReturned = value; }
        }

        public browseFilter BrowseFilter
        {
            get { return this.m_browseFilter; }
            set { this.m_browseFilter = value; }
        }

        public string ElementNameFilter
        {
            get { return this.m_elementNameFilter; }
            set { this.m_elementNameFilter = value; }
        }

        public string VendorFilter
        {
            get { return this.m_vendorFilter; }
            set { this.m_vendorFilter = value; }
        }

        public bool ReturnAllProperties
        {
            get { return this.m_returnAllProperties; }
            set { this.m_returnAllProperties = value; }
        }

        public PropertyID[] PropertyIDs
        {
            get { return this.m_propertyIDs; }
            set { this.m_propertyIDs = value; }
        }

        public bool ReturnPropertyValues
        {
            get { return this.m_returnPropertyValues; }
            set { this.m_returnPropertyValues = value; }
        }

        public virtual object Clone()
        {
            BrowseFilters browseFilters = (BrowseFilters)base.MemberwiseClone();
            browseFilters.PropertyIDs = (PropertyID[])((this.PropertyIDs != null) ? this.PropertyIDs.Clone() : null);
            return browseFilters;
        }

        private int m_maxElementsReturned;

        private browseFilter m_browseFilter;

        private string m_elementNameFilter;

        private string m_vendorFilter;

        private bool m_returnAllProperties;

        private PropertyID[] m_propertyIDs;

        private bool m_returnPropertyValues;
    }
}