using System;
using System.Runtime.Serialization;

namespace Opc.Ae
{
    [Serializable]
    public class SubscriptionFilters : ICloneable, ISerializable
    {
        public int EventTypes
        {
            get { return this.m_eventTypes; }
            set { this.m_eventTypes = value; }
        }

        public int HighSeverity
        {
            get { return this.m_highSeverity; }
            set { this.m_highSeverity = value; }
        }

        public int LowSeverity
        {
            get { return this.m_lowSeverity; }
            set { this.m_lowSeverity = value; }
        }

        public SubscriptionFilters.CategoryCollection Categories
        {
            get { return this.m_categories; }
        }

        public SubscriptionFilters.StringCollection Areas
        {
            get { return this.m_areas; }
        }

        public SubscriptionFilters.StringCollection Sources
        {
            get { return this.m_sources; }
        }

        public SubscriptionFilters()
        {
        }

        protected SubscriptionFilters(SerializationInfo info, StreamingContext context)
        {
            this.m_eventTypes = (int)info.GetValue("ET", typeof(int));
            this.m_categories = (SubscriptionFilters.CategoryCollection)info.GetValue("CT", typeof(SubscriptionFilters.CategoryCollection));
            this.m_highSeverity = (int)info.GetValue("HS", typeof(int));
            this.m_lowSeverity = (int)info.GetValue("LS", typeof(int));
            this.m_areas = (SubscriptionFilters.StringCollection)info.GetValue("AR", typeof(SubscriptionFilters.StringCollection));
            this.m_sources = (SubscriptionFilters.StringCollection)info.GetValue("SR", typeof(SubscriptionFilters.StringCollection));
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ET", this.m_eventTypes);
            info.AddValue("CT", this.m_categories);
            info.AddValue("HS", this.m_highSeverity);
            info.AddValue("LS", this.m_lowSeverity);
            info.AddValue("AR", this.m_areas);
            info.AddValue("SR", this.m_sources);
        }

        public virtual object Clone()
        {
            SubscriptionFilters subscriptionFilters = (SubscriptionFilters)base.MemberwiseClone();
            subscriptionFilters.m_categories = (SubscriptionFilters.CategoryCollection)this.m_categories.Clone();
            subscriptionFilters.m_areas = (SubscriptionFilters.StringCollection)this.m_areas.Clone();
            subscriptionFilters.m_sources = (SubscriptionFilters.StringCollection)this.m_sources.Clone();
            return subscriptionFilters;
        }

        private int m_eventTypes = 65535;

        private SubscriptionFilters.CategoryCollection m_categories = new SubscriptionFilters.CategoryCollection();

        private int m_highSeverity = 1000;

        private int m_lowSeverity = 1;

        private SubscriptionFilters.StringCollection m_areas = new SubscriptionFilters.StringCollection();

        private SubscriptionFilters.StringCollection m_sources = new SubscriptionFilters.StringCollection();

        [Serializable]
        public class CategoryCollection : WriteableCollection
        {
            public int this[int index]
            {
                get { return (int)this.Array[index]; }
            }

            public new int[] ToArray()
            {
                return (int[])this.Array.ToArray(typeof(int));
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
            public string this[int index]
            {
                get { return (string)this.Array[index]; }
            }

            public new string[] ToArray()
            {
                return (string[])this.Array.ToArray(typeof(string));
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