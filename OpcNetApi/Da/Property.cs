using System;

namespace Opc.Da
{
    public class Property
    {
        public static readonly PropertyID DATATYPE = new PropertyID("dataType", 1, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID VALUE = new PropertyID("value", 2, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID QUALITY = new PropertyID("quality", 3, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID TIMESTAMP = new PropertyID("timestamp", 4, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID ACCESSRIGHTS = new PropertyID("accessRights", 5, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID SCANRATE = new PropertyID("scanRate", 6, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID EUTYPE = new PropertyID("euType", 7, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID EUINFO = new PropertyID("euInfo", 8, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID ENGINEERINGUINTS = new PropertyID("engineeringUnits", 100, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID DESCRIPTION = new PropertyID("description", 101, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID HIGHEU = new PropertyID("highEU", 102, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID LOWEU = new PropertyID("lowEU", 103, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID HIGHIR = new PropertyID("highIR", 104, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID LOWIR = new PropertyID("lowIR", 105, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID CLOSELABEL = new PropertyID("closeLabel", 106, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID OPENLABEL = new PropertyID("openLabel", 107, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID TIMEZONE = new PropertyID("timeZone", 108, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID CONDITION_STATUS = new PropertyID("conditionStatus", 300, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID ALARM_QUICK_HELP = new PropertyID("alarmQuickHelp", 301, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID ALARM_AREA_LIST = new PropertyID("alarmAreaList", 302, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID PRIMARY_ALARM_AREA = new PropertyID("primaryAlarmArea", 303, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID CONDITION_LOGIC = new PropertyID("conditionLogic", 304, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID LIMIT_EXCEEDED = new PropertyID("limitExceeded", 305, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID DEADBAND = new PropertyID("deadband", 306, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID HIHI_LIMIT = new PropertyID("hihiLimit", 307, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID HI_LIMIT = new PropertyID("hiLimit", 308, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID LO_LIMIT = new PropertyID("loLimit", 309, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID LOLO_LIMIT = new PropertyID("loloLimit", 310, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID RATE_CHANGE_LIMIT = new PropertyID("rangeOfChangeLimit", 311, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID DEVIATION_LIMIT = new PropertyID("deviationLimit", 312, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID SOUNDFILE = new PropertyID("soundFile", 313, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID TYPE_SYSTEM_ID = new PropertyID("typeSystemID", 600, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID DICTIONARY_ID = new PropertyID("dictionaryID", 601, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID TYPE_ID = new PropertyID("typeID", 602, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID DICTIONARY = new PropertyID("dictionary", 603, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID TYPE_DESCRIPTION = new PropertyID("typeDescription", 604, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID CONSISTENCY_WINDOW = new PropertyID("consistencyWindow", 605, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID WRITE_BEHAVIOR = new PropertyID("writeBehavior", 606, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID UNCONVERTED_ITEM_ID = new PropertyID("unconvertedItemID", 607, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID UNFILTERED_ITEM_ID = new PropertyID("unfilteredItemID", 608, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID DATA_FILTER_VALUE = new PropertyID("dataFilterValue", 609, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID MINIMUM_VALUE = new PropertyID("minimumValue", 109, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID MAXIMUM_VALUE = new PropertyID("maximumValue", 110, "http://opcfoundation.org/DataAccess/");

        public static readonly PropertyID VALUE_PRECISION = new PropertyID("valuePrecision", 111, "http://opcfoundation.org/DataAccess/");
    }
}