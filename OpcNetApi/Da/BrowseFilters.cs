using System;

namespace Opc.Da
{
    [Serializable]
    public class BrowseFilters : ICloneable
    {
        public int MaxElementsReturned
        {
            get => m_maxElementsReturned;
            set => m_maxElementsReturned = value;
        }

        public browseFilter BrowseFilter
        {
            get => m_browseFilter;
            set => m_browseFilter = value;
        }

        public string ElementNameFilter
        {
            get => m_elementNameFilter;
            set => m_elementNameFilter = value;
        }

        public string VendorFilter
        {
            get => m_vendorFilter;
            set => m_vendorFilter = value;
        }

        public bool ReturnAllProperties
        {
            get => m_returnAllProperties;
            set => m_returnAllProperties = value;
        }

        public PropertyID[] PropertyIDs
        {
            get => m_propertyIDs;
            set => m_propertyIDs = value;
        }

        public bool ReturnPropertyValues
        {
            get => m_returnPropertyValues;
            set => m_returnPropertyValues = value;
        }

        public virtual object Clone()
        {
            BrowseFilters browseFilters = (BrowseFilters)MemberwiseClone();
            browseFilters.PropertyIDs = (PropertyID[])((PropertyIDs != null) ? PropertyIDs.Clone() : null);
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