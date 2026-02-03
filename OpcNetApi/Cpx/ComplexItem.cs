using System;
using System.Collections;
using System.IO;
using System.Xml;
using Opc.Da;

namespace Opc.Cpx
{
    public class ComplexItem : ItemIdentifier
    {
        public string Name
        {
            get { return this.m_name; }
        }

        public string TypeSystemID
        {
            get { return this.m_typeSystemID; }
        }

        public string DictionaryID
        {
            get { return this.m_dictionaryID; }
        }

        public string TypeID
        {
            get { return this.m_typeID; }
        }

        public ItemIdentifier DictionaryItemID
        {
            get { return this.m_dictionaryItemID; }
        }

        public ItemIdentifier TypeItemID
        {
            get { return this.m_typeItemID; }
        }

        public ItemIdentifier UnconvertedItemID
        {
            get { return this.m_unconvertedItemID; }
        }

        public ItemIdentifier UnfilteredItemID
        {
            get { return this.m_unfilteredItemID; }
        }

        public ItemIdentifier DataFilterItem
        {
            get { return this.m_filterItem; }
        }

        public string DataFilterValue
        {
            get { return this.m_filterValue; }
            set { this.m_filterValue = value; }
        }

        public ComplexItem()
        {
        }

        public ComplexItem(ItemIdentifier itemID)
        {
            base.ItemPath = itemID.ItemPath;
            base.ItemName = itemID.ItemName;
        }

        public override string ToString()
        {
            if (this.m_name != null || this.m_name.Length != 0)
            {
                return this.m_name;
            }

            return base.ItemName;
        }

        public ComplexItem GetRootItem()
        {
            if (this.m_unconvertedItemID != null)
            {
                return ComplexTypeCache.GetComplexItem(this.m_unconvertedItemID);
            }

            if (this.m_unfilteredItemID != null)
            {
                return ComplexTypeCache.GetComplexItem(this.m_unfilteredItemID);
            }

            return this;
        }

        public void Update(Da.Server server)
        {
            this.Clear();
            ItemPropertyCollection[] properties = server.GetProperties(new ItemIdentifier[]
            {
                this
            }, ComplexItem.CPX_PROPERTIES, true);
            if (properties == null || properties.Length != 1)
            {
                throw new ApplicationException("Unexpected results returned from server.");
            }

            if (!this.Init((ItemProperty[])properties[0].ToArray(typeof(ItemProperty))))
            {
                throw new ApplicationException("Not a valid complex item.");
            }

            this.GetDataFilterItem(server);
        }

        public ComplexItem[] GetTypeConversions(Da.Server server)
        {
            if (this.m_unconvertedItemID != null || this.m_unfilteredItemID != null)
            {
                return null;
            }

            BrowsePosition browsePosition = null;
            ComplexItem[] result;
            try
            {
                BrowseFilters browseFilters = new BrowseFilters();
                browseFilters.ElementNameFilter = "CPX";
                browseFilters.BrowseFilter = browseFilter.branch;
                browseFilters.ReturnAllProperties = false;
                browseFilters.PropertyIDs = null;
                browseFilters.ReturnPropertyValues = false;
                BrowseElement[] array = server.Browse(this, browseFilters, out browsePosition);
                if (array == null || array.Length == 0)
                {
                    result = null;
                }
                else
                {
                    if (browsePosition != null)
                    {
                        browsePosition.Dispose();
                        browsePosition = null;
                    }

                    ItemIdentifier itemID = new ItemIdentifier(array[0].ItemPath, array[0].ItemName);
                    browseFilters.ElementNameFilter = null;
                    browseFilters.BrowseFilter = browseFilter.item;
                    browseFilters.ReturnAllProperties = false;
                    browseFilters.PropertyIDs = ComplexItem.CPX_PROPERTIES;
                    browseFilters.ReturnPropertyValues = true;
                    array = server.Browse(itemID, browseFilters, out browsePosition);
                    if (array == null || array.Length == 0)
                    {
                        result = new ComplexItem[0];
                    }
                    else
                    {
                        ArrayList arrayList = new ArrayList(array.Length);
                        foreach (BrowseElement browseElement in array)
                        {
                            if (browseElement.Name != "DataFilters")
                            {
                                ComplexItem complexItem = new ComplexItem();
                                if (complexItem.Init(browseElement))
                                {
                                    complexItem.GetDataFilterItem(server);
                                    arrayList.Add(complexItem);
                                }
                            }
                        }

                        result = (ComplexItem[])arrayList.ToArray(typeof(ComplexItem));
                    }
                }
            }
            finally
            {
                if (browsePosition != null)
                {
                    browsePosition.Dispose();
                    browsePosition = null;
                }
            }

            return result;
        }

        public ComplexItem[] GetDataFilters(Da.Server server)
        {
            if (this.m_unfilteredItemID != null)
            {
                return null;
            }

            if (this.m_filterItem == null)
            {
                return null;
            }

            BrowsePosition browsePosition = null;
            ComplexItem[] result;
            try
            {
                BrowseFilters browseFilters = new BrowseFilters();
                browseFilters.ElementNameFilter = null;
                browseFilters.BrowseFilter = browseFilter.item;
                browseFilters.ReturnAllProperties = false;
                browseFilters.PropertyIDs = ComplexItem.CPX_PROPERTIES;
                browseFilters.ReturnPropertyValues = true;
                BrowseElement[] array = server.Browse(this.m_filterItem, browseFilters, out browsePosition);
                if (array == null || array.Length == 0)
                {
                    result = new ComplexItem[0];
                }
                else
                {
                    ArrayList arrayList = new ArrayList(array.Length);
                    foreach (BrowseElement element in array)
                    {
                        ComplexItem complexItem = new ComplexItem();
                        if (complexItem.Init(element))
                        {
                            arrayList.Add(complexItem);
                        }
                    }

                    result = (ComplexItem[])arrayList.ToArray(typeof(ComplexItem));
                }
            }
            finally
            {
                if (browsePosition != null)
                {
                    browsePosition.Dispose();
                    browsePosition = null;
                }
            }

            return result;
        }

        public ComplexItem CreateDataFilter(Da.Server server, string filterName, string filterValue)
        {
            if (this.m_unfilteredItemID != null)
            {
                return null;
            }

            if (this.m_filterItem == null)
            {
                return null;
            }

            BrowsePosition browsePosition = null;
            ComplexItem result;
            try
            {
                ItemValue itemValue = new ItemValue(this.m_filterItem);
                StringWriter stringWriter = new StringWriter();
                XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
                xmlTextWriter.WriteStartElement("DataFilters");
                xmlTextWriter.WriteAttributeString("Name", filterName);
                xmlTextWriter.WriteString(filterValue);
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.Close();
                itemValue.Value = stringWriter.ToString();
                itemValue.Quality = Quality.Bad;
                itemValue.QualitySpecified = false;
                itemValue.Timestamp = DateTime.MinValue;
                itemValue.TimestampSpecified = false;
                IdentifiedResult[] array = server.Write(new ItemValue[]
                {
                    itemValue
                });
                if (array == null || array.Length == 0)
                {
                    throw new ApplicationException("Unexpected result from server.");
                }

                if (array[0].ResultID.Failed())
                {
                    throw new ApplicationException("Could not create new data filter.");
                }

                BrowseFilters browseFilters = new BrowseFilters();
                browseFilters.ElementNameFilter = filterName;
                browseFilters.BrowseFilter = browseFilter.item;
                browseFilters.ReturnAllProperties = false;
                browseFilters.PropertyIDs = ComplexItem.CPX_PROPERTIES;
                browseFilters.ReturnPropertyValues = true;
                BrowseElement[] array2 = server.Browse(this.m_filterItem, browseFilters, out browsePosition);
                if (array2 == null || array2.Length == 0)
                {
                    throw new ApplicationException("Could not browse to new data filter.");
                }

                ComplexItem complexItem = new ComplexItem();
                if (!complexItem.Init(array2[0]))
                {
                    throw new ApplicationException("Could not initialize to new data filter.");
                }

                result = complexItem;
            }
            finally
            {
                if (browsePosition != null)
                {
                    browsePosition.Dispose();
                    browsePosition = null;
                }
            }

            return result;
        }

        public void UpdateDataFilter(Da.Server server, string filterValue)
        {
            if (this.m_unfilteredItemID == null)
            {
                throw new ApplicationException("Cannot update the data filter for this item.");
            }

            IdentifiedResult[] array = server.Write(new ItemValue[]
            {
                new ItemValue(this)
                {
                    Value = filterValue,
                    Quality = Quality.Bad,
                    QualitySpecified = false,
                    Timestamp = DateTime.MinValue,
                    TimestampSpecified = false
                }
            });
            if (array == null || array.Length == 0)
            {
                throw new ApplicationException("Unexpected result from server.");
            }

            if (array[0].ResultID.Failed())
            {
                throw new ApplicationException("Could not update data filter.");
            }

            this.m_filterValue = filterValue;
        }

        public string GetTypeDictionary(Da.Server server)
        {
            ItemPropertyCollection[] properties = server.GetProperties(new ItemIdentifier[]
            {
                this.m_dictionaryItemID
            }, new PropertyID[]
            {
                Property.DICTIONARY
            }, true);
            if (properties == null || properties.Length == 0 || properties[0].Count == 0)
            {
                return null;
            }

            ItemProperty itemProperty = properties[0][0];
            if (!itemProperty.ResultID.Succeeded())
            {
                return null;
            }

            return (string)itemProperty.Value;
        }

        public string GetTypeDescription(Da.Server server)
        {
            ItemPropertyCollection[] properties = server.GetProperties(new ItemIdentifier[]
            {
                this.m_typeItemID
            }, new PropertyID[]
            {
                Property.TYPE_DESCRIPTION
            }, true);
            if (properties == null || properties.Length == 0 || properties[0].Count == 0)
            {
                return null;
            }

            ItemProperty itemProperty = properties[0][0];
            if (!itemProperty.ResultID.Succeeded())
            {
                return null;
            }

            return (string)itemProperty.Value;
        }

        public void GetDataFilterItem(Da.Server server)
        {
            this.m_filterItem = null;
            if (this.m_unfilteredItemID != null)
            {
                return;
            }

            BrowsePosition browsePosition = null;
            try
            {
                ItemIdentifier itemID = new ItemIdentifier(this);
                BrowseFilters browseFilters = new BrowseFilters();
                browseFilters.ElementNameFilter = "DataFilters";
                browseFilters.BrowseFilter = browseFilter.all;
                browseFilters.ReturnAllProperties = false;
                browseFilters.PropertyIDs = null;
                browseFilters.ReturnPropertyValues = false;
                BrowseElement[] array;
                if (this.m_unconvertedItemID == null)
                {
                    browseFilters.ElementNameFilter = "CPX";
                    array = server.Browse(itemID, browseFilters, out browsePosition);
                    if (array == null || array.Length == 0)
                    {
                        return;
                    }

                    if (browsePosition != null)
                    {
                        browsePosition.Dispose();
                        browsePosition = null;
                    }

                    itemID = new ItemIdentifier(array[0].ItemPath, array[0].ItemName);
                    browseFilters.ElementNameFilter = "DataFilters";
                }

                array = server.Browse(itemID, browseFilters, out browsePosition);
                if (array != null && array.Length != 0)
                {
                    this.m_filterItem = new ItemIdentifier(array[0].ItemPath, array[0].ItemName);
                }
            }
            finally
            {
                if (browsePosition != null)
                {
                    browsePosition.Dispose();
                    browsePosition = null;
                }
            }
        }

        private void Clear()
        {
            this.m_typeSystemID = null;
            this.m_dictionaryID = null;
            this.m_typeID = null;
            this.m_dictionaryItemID = null;
            this.m_typeItemID = null;
            this.m_unconvertedItemID = null;
            this.m_unfilteredItemID = null;
            this.m_filterItem = null;
            this.m_filterValue = null;
        }

        private bool Init(BrowseElement element)
        {
            base.ItemPath = element.ItemPath;
            base.ItemName = element.ItemName;
            this.m_name = element.Name;
            return this.Init(element.Properties);
        }

        private bool Init(ItemProperty[] properties)
        {
            this.Clear();
            if (properties == null || properties.Length < 3)
            {
                return false;
            }

            foreach (ItemProperty itemProperty in properties)
            {
                if (itemProperty.ResultID.Succeeded())
                {
                    if (itemProperty.ID == Property.TYPE_SYSTEM_ID)
                    {
                        this.m_typeSystemID = (string)itemProperty.Value;
                    }
                    else if (itemProperty.ID == Property.DICTIONARY_ID)
                    {
                        this.m_dictionaryID = (string)itemProperty.Value;
                        this.m_dictionaryItemID = new ItemIdentifier(itemProperty.ItemPath, itemProperty.ItemName);
                    }
                    else if (itemProperty.ID == Property.TYPE_ID)
                    {
                        this.m_typeID = (string)itemProperty.Value;
                        this.m_typeItemID = new ItemIdentifier(itemProperty.ItemPath, itemProperty.ItemName);
                    }
                    else if (itemProperty.ID == Property.UNCONVERTED_ITEM_ID)
                    {
                        this.m_unconvertedItemID = new ItemIdentifier(base.ItemPath, (string)itemProperty.Value);
                    }
                    else if (itemProperty.ID == Property.UNFILTERED_ITEM_ID)
                    {
                        this.m_unfilteredItemID = new ItemIdentifier(base.ItemPath, (string)itemProperty.Value);
                    }
                    else if (itemProperty.ID == Property.DATA_FILTER_VALUE)
                    {
                        this.m_filterValue = (string)itemProperty.Value;
                    }
                }
            }

            return this.m_typeSystemID != null && this.m_dictionaryID != null && this.m_typeID != null;
        }

        public const string CPX_BRANCH = "CPX";

        public const string CPX_DATA_FILTERS = "DataFilters";

        public static readonly PropertyID[] CPX_PROPERTIES = new PropertyID[]
        {
            Property.TYPE_SYSTEM_ID,
            Property.DICTIONARY_ID,
            Property.TYPE_ID,
            Property.UNCONVERTED_ITEM_ID,
            Property.UNFILTERED_ITEM_ID,
            Property.DATA_FILTER_VALUE
        };

        private string m_name;

        private string m_typeSystemID;

        private string m_dictionaryID;

        private string m_typeID;

        private ItemIdentifier m_dictionaryItemID;

        private ItemIdentifier m_typeItemID;

        private ItemIdentifier m_unconvertedItemID;

        private ItemIdentifier m_unfilteredItemID;

        private ItemIdentifier m_filterItem;

        private string m_filterValue;
    }
}