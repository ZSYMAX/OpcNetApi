using System;
using System.Collections;
using System.Reflection;

namespace Opc.Da
{
    [Serializable]
    public class PropertyDescription
    {
        public PropertyID ID
        {
            get => m_id;
            set => m_id = value;
        }

        public System.Type Type
        {
            get => m_type;
            set => m_type = value;
        }

        public string Name
        {
            get => m_name;
            set => m_name = value;
        }

        public PropertyDescription(PropertyID id, System.Type type, string name)
        {
            ID = id;
            Type = type;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        public static PropertyDescription Find(PropertyID id)
        {
            FieldInfo[] fields = typeof(PropertyDescription).GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (FieldInfo fieldInfo in fields)
            {
                PropertyDescription propertyDescription = (PropertyDescription)fieldInfo.GetValue(typeof(PropertyDescription));
                if (propertyDescription.ID == id)
                {
                    return propertyDescription;
                }
            }

            return null;
        }

        public static PropertyDescription[] Enumerate()
        {
            ArrayList arrayList = new ArrayList();
            FieldInfo[] fields = typeof(PropertyDescription).GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (FieldInfo fieldInfo in fields)
            {
                arrayList.Add(fieldInfo.GetValue(typeof(PropertyDescription)));
            }

            return (PropertyDescription[])arrayList.ToArray(typeof(PropertyDescription));
        }

        private PropertyID m_id;

        private System.Type m_type;

        private string m_name;

        public static readonly PropertyDescription DATATYPE = new PropertyDescription(Property.DATATYPE, typeof(Type), "Item Canonical DataType");

        public static readonly PropertyDescription VALUE = new PropertyDescription(Property.VALUE, typeof(object), "Item Value");

        public static readonly PropertyDescription QUALITY = new PropertyDescription(Property.QUALITY, typeof(Quality), "Item Quality");

        public static readonly PropertyDescription TIMESTAMP = new PropertyDescription(Property.TIMESTAMP, typeof(DateTime), "Item Timestamp");

        public static readonly PropertyDescription ACCESSRIGHTS = new PropertyDescription(Property.ACCESSRIGHTS, typeof(accessRights), "Item Access Rights");

        public static readonly PropertyDescription SCANRATE = new PropertyDescription(Property.SCANRATE, typeof(float), "Server Scan Rate");

        public static readonly PropertyDescription EUTYPE = new PropertyDescription(Property.EUTYPE, typeof(euType), "Item EU Type");

        public static readonly PropertyDescription EUINFO = new PropertyDescription(Property.EUINFO, typeof(string[]), "Item EU Info");

        public static readonly PropertyDescription ENGINEERINGUINTS = new PropertyDescription(Property.ENGINEERINGUINTS, typeof(string), "EU Units");

        public static readonly PropertyDescription DESCRIPTION = new PropertyDescription(Property.DESCRIPTION, typeof(string), "Item Description");

        public static readonly PropertyDescription HIGHEU = new PropertyDescription(Property.HIGHEU, typeof(double), "High EU");

        public static readonly PropertyDescription LOWEU = new PropertyDescription(Property.LOWEU, typeof(double), "Low EU");

        public static readonly PropertyDescription HIGHIR = new PropertyDescription(Property.HIGHIR, typeof(double), "High Instrument Range");

        public static readonly PropertyDescription LOWIR = new PropertyDescription(Property.LOWIR, typeof(double), "Low Instrument Range");

        public static readonly PropertyDescription CLOSELABEL = new PropertyDescription(Property.CLOSELABEL, typeof(string), "Contact Close Label");

        public static readonly PropertyDescription OPENLABEL = new PropertyDescription(Property.OPENLABEL, typeof(string), "Contact Open Label");

        public static readonly PropertyDescription TIMEZONE = new PropertyDescription(Property.TIMEZONE, typeof(int), "Timezone");

        public static readonly PropertyDescription CONDITION_STATUS = new PropertyDescription(Property.CONDITION_STATUS, typeof(string), "Condition Status");

        public static readonly PropertyDescription ALARM_QUICK_HELP = new PropertyDescription(Property.ALARM_QUICK_HELP, typeof(string), "Alarm Quick Help");

        public static readonly PropertyDescription ALARM_AREA_LIST = new PropertyDescription(Property.ALARM_AREA_LIST, typeof(string), "Alarm Area List");

        public static readonly PropertyDescription PRIMARY_ALARM_AREA = new PropertyDescription(Property.PRIMARY_ALARM_AREA, typeof(string), "Primary Alarm Area");

        public static readonly PropertyDescription CONDITION_LOGIC = new PropertyDescription(Property.CONDITION_LOGIC, typeof(string), "Condition Logic");

        public static readonly PropertyDescription LIMIT_EXCEEDED = new PropertyDescription(Property.LIMIT_EXCEEDED, typeof(string), "Limit Exceeded");

        public static readonly PropertyDescription DEADBAND = new PropertyDescription(Property.DEADBAND, typeof(double), "Deadband");

        public static readonly PropertyDescription HIHI_LIMIT = new PropertyDescription(Property.HIHI_LIMIT, typeof(double), "HiHi Limit");

        public static readonly PropertyDescription HI_LIMIT = new PropertyDescription(Property.HI_LIMIT, typeof(double), "Hi Limit");

        public static readonly PropertyDescription LO_LIMIT = new PropertyDescription(Property.LO_LIMIT, typeof(double), "Lo Limit");

        public static readonly PropertyDescription LOLO_LIMIT = new PropertyDescription(Property.LOLO_LIMIT, typeof(double), "LoLo Limit");

        public static readonly PropertyDescription RATE_CHANGE_LIMIT = new PropertyDescription(Property.RATE_CHANGE_LIMIT, typeof(double), "Rate of Change Limit");

        public static readonly PropertyDescription DEVIATION_LIMIT = new PropertyDescription(Property.DEVIATION_LIMIT, typeof(double), "Deviation Limit");

        public static readonly PropertyDescription SOUNDFILE = new PropertyDescription(Property.SOUNDFILE, typeof(string), "Sound File");

        public static readonly PropertyDescription TYPE_SYSTEM_ID = new PropertyDescription(Property.TYPE_SYSTEM_ID, typeof(string), "Type System ID");

        public static readonly PropertyDescription DICTIONARY_ID = new PropertyDescription(Property.DICTIONARY_ID, typeof(string), "Dictionary ID");

        public static readonly PropertyDescription TYPE_ID = new PropertyDescription(Property.TYPE_ID, typeof(string), "Type ID");

        public static readonly PropertyDescription DICTIONARY = new PropertyDescription(Property.DICTIONARY, typeof(object), "Dictionary");

        public static readonly PropertyDescription TYPE_DESCRIPTION = new PropertyDescription(Property.TYPE_DESCRIPTION, typeof(string), "Type Description");

        public static readonly PropertyDescription CONSISTENCY_WINDOW = new PropertyDescription(Property.CONSISTENCY_WINDOW, typeof(string), "Consistency Window");

        public static readonly PropertyDescription WRITE_BEHAVIOR = new PropertyDescription(Property.WRITE_BEHAVIOR, typeof(string), "Write Behavior");

        public static readonly PropertyDescription UNCONVERTED_ITEM_ID = new PropertyDescription(Property.UNCONVERTED_ITEM_ID, typeof(string), "Unconverted Item ID");

        public static readonly PropertyDescription UNFILTERED_ITEM_ID = new PropertyDescription(Property.UNFILTERED_ITEM_ID, typeof(string), "Unfiltered Item ID");

        public static readonly PropertyDescription DATA_FILTER_VALUE = new PropertyDescription(Property.DATA_FILTER_VALUE, typeof(string), "Data Filter Value");

        public static readonly PropertyDescription MINIMUM_VALUE = new PropertyDescription(Property.MINIMUM_VALUE, typeof(object), "Minimum Value");

        public static readonly PropertyDescription MAXIMUM_VALUE = new PropertyDescription(Property.MAXIMUM_VALUE, typeof(object), "Maximum Value");

        public static readonly PropertyDescription VALUE_PRECISION = new PropertyDescription(Property.VALUE_PRECISION, typeof(object), "Value Precision");
    }
}