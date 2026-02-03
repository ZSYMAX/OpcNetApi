using System;
using System.Collections;
using System.Runtime.Serialization;

namespace Opc.Ae
{
    [Serializable]
    public class Subscription : ISubscription, IDisposable, ISerializable, ICloneable
    {
        public Subscription(Server server, ISubscription subscription, SubscriptionState state)
        {
            if (server == null)
            {
                throw new ArgumentNullException("server");
            }

            if (subscription == null)
            {
                throw new ArgumentNullException("subscription");
            }

            m_server = server;
            m_subscription = subscription;
            m_state = (SubscriptionState)state.Clone();
            m_name = state.Name;
        }

        ~Subscription()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                if (disposing && m_subscription != null)
                {
                    m_server.SubscriptionDisposed(this);
                    m_subscription.Dispose();
                }

                m_disposed = true;
            }
        }

        protected Subscription(SerializationInfo info, StreamingContext context)
        {
            m_state = (SubscriptionState)info.GetValue("ST", typeof(SubscriptionState));
            m_filters = (SubscriptionFilters)info.GetValue("FT", typeof(SubscriptionFilters));
            m_attributes = (AttributeDictionary)info.GetValue("AT", typeof(AttributeDictionary));
            m_name = m_state.Name;
            m_categories = new CategoryCollection(m_filters.Categories.ToArray());
            m_areas = new StringCollection(m_filters.Areas.ToArray());
            m_sources = new StringCollection(m_filters.Sources.ToArray());
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ST", m_state);
            info.AddValue("FT", m_filters);
            info.AddValue("AT", m_attributes);
        }

        public virtual object Clone()
        {
            return (Subscription)MemberwiseClone();
        }

        public Server Server => m_server;

        public string Name => m_state.Name;

        public object ClientHandle => m_state.ClientHandle;

        public bool Active => m_state.Active;

        public int BufferTime => m_state.BufferTime;

        public int MaxSize => m_state.MaxSize;

        public int KeepAlive => m_state.KeepAlive;

        public int EventTypes => m_filters.EventTypes;

        public int HighSeverity => m_filters.HighSeverity;

        public int LowSeverity => m_filters.LowSeverity;

        public CategoryCollection Categories => m_categories;

        public StringCollection Areas => m_areas;

        public StringCollection Sources => m_sources;

        public AttributeDictionary Attributes => m_attributes;

        public Opc.Ae.AttributeDictionary GetAttributes()
        {
            Opc.Ae.AttributeDictionary attributeDictionary = new Opc.Ae.AttributeDictionary();
            IDictionaryEnumerator enumerator = m_attributes.GetEnumerator();
            while (enumerator.MoveNext())
            {
                int key = (int)enumerator.Key;
                AttributeCollection attributeCollection = (AttributeCollection)enumerator.Value;
                attributeDictionary.Add(key, attributeCollection.ToArray());
            }

            return attributeDictionary;
        }

        public event EventChangedEventHandler EventChanged
        {
            add => m_subscription.EventChanged += value;
            remove => m_subscription.EventChanged -= value;
        }

        public SubscriptionState GetState()
        {
            if (m_subscription == null)
            {
                throw new NotConnectedException();
            }

            m_state = m_subscription.GetState();
            m_state.Name = m_name;
            return (SubscriptionState)m_state.Clone();
        }

        public SubscriptionState ModifyState(int masks, SubscriptionState state)
        {
            if (m_subscription == null)
            {
                throw new NotConnectedException();
            }

            m_state = m_subscription.ModifyState(masks, state);
            if ((masks & 1) != 0)
            {
                m_state.Name = (m_name = state.Name);
            }
            else
            {
                m_state.Name = m_name;
            }

            return (SubscriptionState)m_state.Clone();
        }

        public SubscriptionFilters GetFilters()
        {
            if (m_subscription == null)
            {
                throw new NotConnectedException();
            }

            m_filters = m_subscription.GetFilters();
            m_categories = new CategoryCollection(m_filters.Categories.ToArray());
            m_areas = new StringCollection(m_filters.Areas.ToArray());
            m_sources = new StringCollection(m_filters.Sources.ToArray());
            return (SubscriptionFilters)m_filters.Clone();
        }

        public void SetFilters(SubscriptionFilters filters)
        {
            if (m_subscription == null)
            {
                throw new NotConnectedException();
            }

            m_subscription.SetFilters(filters);
            GetFilters();
        }

        public int[] GetReturnedAttributes(int eventCategory)
        {
            if (m_subscription == null)
            {
                throw new NotConnectedException();
            }

            int[] returnedAttributes = m_subscription.GetReturnedAttributes(eventCategory);
            m_attributes.Update(eventCategory, (int[])Convert.Clone(returnedAttributes));
            return returnedAttributes;
        }

        public void SelectReturnedAttributes(int eventCategory, int[] attributeIDs)
        {
            if (m_subscription == null)
            {
                throw new NotConnectedException();
            }

            m_subscription.SelectReturnedAttributes(eventCategory, attributeIDs);
            m_attributes.Update(eventCategory, (int[])Convert.Clone(attributeIDs));
        }

        public void Refresh()
        {
            if (m_subscription == null)
            {
                throw new NotConnectedException();
            }

            m_subscription.Refresh();
        }

        public void CancelRefresh()
        {
            if (m_subscription == null)
            {
                throw new NotConnectedException();
            }

            m_subscription.CancelRefresh();
        }

        internal SubscriptionState State => m_state;

        internal SubscriptionFilters Filters => m_filters;

        private bool m_disposed;

        private Server m_server;

        private ISubscription m_subscription;

        private SubscriptionState m_state = new SubscriptionState();

        private string m_name;

        private SubscriptionFilters m_filters = new SubscriptionFilters();

        private CategoryCollection m_categories = new CategoryCollection();

        private StringCollection m_areas = new StringCollection();

        private StringCollection m_sources = new StringCollection();

        private AttributeDictionary m_attributes = new AttributeDictionary();

        private class Names
        {
            internal const string STATE = "ST";

            internal const string FILTERS = "FT";

            internal const string ATTRIBUTES = "AT";
        }

        public class CategoryCollection : ReadOnlyCollection
        {
            public int this[int index] => (int)Array.GetValue(index);

            public new int[] ToArray()
            {
                return (int[])Convert.Clone(Array);
            }

            internal CategoryCollection() : base(new int[0])
            {
            }

            internal CategoryCollection(int[] categoryIDs) : base(categoryIDs)
            {
            }
        }

        public class StringCollection : ReadOnlyCollection
        {
            public string this[int index] => (string)Array.GetValue(index);

            public new string[] ToArray()
            {
                return (string[])Convert.Clone(Array);
            }

            internal StringCollection() : base(new string[0])
            {
            }

            internal StringCollection(string[] strings) : base(strings)
            {
            }
        }

        [Serializable]
        public class AttributeDictionary : ReadOnlyDictionary
        {
            public AttributeCollection this[int categoryID] => (AttributeCollection)base[categoryID];

            internal AttributeDictionary() : base(null)
            {
            }

            internal AttributeDictionary(Hashtable dictionary) : base(dictionary)
            {
            }

            internal void Update(int categoryID, int[] attributeIDs)
            {
                Dictionary[categoryID] = new AttributeCollection(attributeIDs);
            }

            protected AttributeDictionary(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }

        [Serializable]
        public class AttributeCollection : ReadOnlyCollection
        {
            public int this[int index] => (int)Array.GetValue(index);

            public new int[] ToArray()
            {
                return (int[])Convert.Clone(Array);
            }

            internal AttributeCollection() : base(new int[0])
            {
            }

            internal AttributeCollection(int[] attributeIDs) : base(attributeIDs)
            {
            }

            protected AttributeCollection(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
    }
}