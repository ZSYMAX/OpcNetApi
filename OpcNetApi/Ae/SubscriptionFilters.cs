using System;
using System.Runtime.Serialization;

namespace Opc.Ae
{
    [Serializable]
    public class SubscriptionFilters : ICloneable, ISerializable
    {
        public int EventTypes
        {
            get => m_eventTypes;
            set => m_eventTypes = value;
        }

        public int HighSeverity
        {
            get => m_highSeverity;
            set => m_highSeverity = value;
        }

        public int LowSeverity
        {
            get => m_lowSeverity;
            set => m_lowSeverity = value;
        }

        public CategoryCollection Categories => m_categories;

        public StringCollection Areas => m_areas;

        public StringCollection Sources => m_sources;

        public SubscriptionFilters()
        {
        }

        protected SubscriptionFilters(SerializationInfo info, StreamingContext context)
        {
            m_eventTypes = (int)info.GetValue("ET", typeof(int));
            m_categories = (CategoryCollection)info.GetValue("CT", typeof(CategoryCollection));
            m_highSeverity = (int)info.GetValue("HS", typeof(int));
            m_lowSeverity = (int)info.GetValue("LS", typeof(int));
            m_areas = (StringCollection)info.GetValue("AR", typeof(StringCollection));
            m_sources = (StringCollection)info.GetValue("SR", typeof(StringCollection));
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ET", m_eventTypes);
            info.AddValue("CT", m_categories);
            info.AddValue("HS", m_highSeverity);
            info.AddValue("LS", m_lowSeverity);
            info.AddValue("AR", m_areas);
            info.AddValue("SR", m_sources);
        }

        public virtual object Clone()
        {
            SubscriptionFilters subscriptionFilters = (SubscriptionFilters)MemberwiseClone();
            subscriptionFilters.m_categories = (CategoryCollection)m_categories.Clone();
            subscriptionFilters.m_areas = (StringCollection)m_areas.Clone();
            subscriptionFilters.m_sources = (StringCollection)m_sources.Clone();
            return subscriptionFilters;
        }

        private int m_eventTypes = 65535;

        private CategoryCollection m_categories = new CategoryCollection();

        private int m_highSeverity = 1000;

        private int m_lowSeverity = 1;

        private StringCollection m_areas = new StringCollection();

        private StringCollection m_sources = new StringCollection();

        [Serializable]
        public class CategoryCollection : WriteableCollection
        {
            public int this[int index] => (int)Array[index];

            public new int[] ToArray()
            {
                return (int[])Array.ToArray(typeof(int));
            }

            internal CategoryCollection() : base(null, typeof(int))
            {
            }

            protected CategoryCollection(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }

        [Serializable]
        public class StringCollection : WriteableCollection
        {
            public string this[int index] => (string)Array[index];

            public new string[] ToArray()
            {
                return (string[])Array.ToArray(typeof(string));
            }

            internal StringCollection() : base(null, typeof(string))
            {
            }

            protected StringCollection(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }

        private class Names
        {
            internal const string EVENT_TYPES = "ET";

            internal const string CATEGORIES = "CT";

            internal const string HIGH_SEVERITY = "HS";

            internal const string LOW_SEVERITY = "LS";

            internal const string AREAS = "AR";

            internal const string SOURCES = "SR";
        }
    }
}