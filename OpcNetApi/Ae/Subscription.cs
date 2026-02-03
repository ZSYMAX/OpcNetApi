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

            this.m_server = server;
            this.m_subscription = subscription;
            this.m_state = (SubscriptionState)state.Clone();
            this.m_name = state.Name;
        }

        ~Subscription()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.m_disposed)
            {
                if (disposing && this.m_subscription != null)
                {
                    this.m_server.SubscriptionDisposed(this);
                    this.m_subscription.Dispose();
                }

                this.m_disposed = true;
            }
        }

        protected Subscription(SerializationInfo info, StreamingContext context)
        {
            this.m_state = (SubscriptionState)info.GetValue("ST", typeof(SubscriptionState));
            this.m_filters = (SubscriptionFilters)info.GetValue("FT", typeof(SubscriptionFilters));
            this.m_attributes = (Subscription.AttributeDictionary)info.GetValue("AT", typeof(Subscription.AttributeDictionary));
            this.m_name = this.m_state.Name;
            this.m_categories = new Subscription.CategoryCollection(this.m_filters.Categories.ToArray());
            this.m_areas = new Subscription.StringCollection(this.m_filters.Areas.ToArray());
            this.m_sources = new Subscription.StringCollection(this.m_filters.Sources.ToArray());
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ST", this.m_state);
            info.AddValue("FT", this.m_filters);
            info.AddValue("AT", this.m_attributes);
        }

        public virtual object Clone()
        {
            return (Subscription)base.MemberwiseClone();
        }

        public Server Server
        {
            get { return this.m_server; }
        }

        public string Name
        {
            get { return this.m_state.Name; }
        }

        public object ClientHandle
        {
            get { return this.m_state.ClientHandle; }
        }

        public bool Active
        {
            get { return this.m_state.Active; }
        }

        public int BufferTime
        {
            get { return this.m_state.BufferTime; }
        }

        public int MaxSize
        {
            get { return this.m_state.MaxSize; }
        }

        public int KeepAlive
        {
            get { return this.m_state.KeepAlive; }
        }

        public int EventTypes
        {
            get { return this.m_filters.EventTypes; }
        }

        public int HighSeverity
        {
            get { return this.m_filters.HighSeverity; }
        }

        public int LowSeverity
        {
            get { return this.m_filters.LowSeverity; }
        }

        public Subscription.CategoryCollection Categories
        {
            get { return this.m_categories; }
        }

        public Subscription.StringCollection Areas
        {
            get { return this.m_areas; }
        }

        public Subscription.StringCollection Sources
        {
            get { return this.m_sources; }
        }

        public Subscription.AttributeDictionary Attributes
        {
            get { return this.m_attributes; }
        }

        public Opc.Ae.AttributeDictionary GetAttributes()
        {
            Opc.Ae.AttributeDictionary attributeDictionary = new Opc.Ae.AttributeDictionary();
            IDictionaryEnumerator enumerator = this.m_attributes.GetEnumerator();
            while (enumerator.MoveNext())
            {
                int key = (int)enumerator.Key;
                Subscription.AttributeCollection attributeCollection = (Subscription.AttributeCollection)enumerator.Value;
                attributeDictionary.Add(key, attributeCollection.ToArray());
            }

            return attributeDictionary;
        }

        public event EventChangedEventHandler EventChanged
        {
            add { this.m_subscription.EventChanged += value; }
            remove { this.m_subscription.EventChanged -= value; }
        }

        public SubscriptionState GetState()
        {
            if (this.m_subscription == null)
            {
                throw new NotConnectedException();
            }

            this.m_state = this.m_subscription.GetState();
            this.m_state.Name = this.m_name;
            return (SubscriptionState)this.m_state.Clone();
        }

        public SubscriptionState ModifyState(int masks, SubscriptionState state)
        {
            if (this.m_subscription == null)
            {
                throw new NotConnectedException();
            }

            this.m_state = this.m_subscription.ModifyState(masks, state);
            if ((masks & 1) != 0)
            {
                this.m_state.Name = (this.m_name = state.Name);
            }
            else
            {
                this.m_state.Name = this.m_name;
            }

            return (SubscriptionState)this.m_state.Clone();
        }

        public SubscriptionFilters GetFilters()
        {
            if (this.m_subscription == null)
            {
                throw new NotConnectedException();
            }

            this.m_filters = this.m_subscription.GetFilters();
            this.m_categories = new Subscription.CategoryCollection(this.m_filters.Categories.ToArray());
            this.m_areas = new Subscription.StringCollection(this.m_filters.Areas.ToArray());
            this.m_sources = new Subscription.StringCollection(this.m_filters.Sources.ToArray());
            return (SubscriptionFilters)this.m_filters.Clone();
        }

        public void SetFilters(SubscriptionFilters filters)
        {
            if (this.m_subscription == null)
            {
                throw new NotConnectedException();
            }

            this.m_subscription.SetFilters(filters);
            this.GetFilters();
        }

        public int[] GetReturnedAttributes(int eventCategory)
        {
            if (this.m_subscription == null)
            {
                throw new NotConnectedException();
            }

            int[] returnedAttributes = this.m_subscription.GetReturnedAttributes(eventCategory);
            this.m_attributes.Update(eventCategory, (int[])Convert.Clone(returnedAttributes));
            return returnedAttributes;
        }

        public void SelectReturnedAttributes(int eventCategory, int[] attributeIDs)
        {
            if (this.m_subscription == null)
            {
                throw new NotConnectedException();
            }

            this.m_subscription.SelectReturnedAttributes(eventCategory, attributeIDs);
            this.m_attributes.Update(eventCategory, (int[])Convert.Clone(attributeIDs));
        }

        public void Refresh()
        {
            if (this.m_subscription == null)
            {
                throw new NotConnectedException();
            }

            this.m_subscription.Refresh();
        }

        public void CancelRefresh()
        {
            if (this.m_subscription == null)
            {
                throw new NotConnectedException();
            }

            this.m_subscription.CancelRefresh();
        }

        internal SubscriptionState State
        {
            get { return this.m_state; }
        }

        internal SubscriptionFilters Filters
        {
            get { return this.m_filters; }
        }

        private bool m_disposed;

        private Server m_server;

        private ISubscription m_subscription;

        private SubscriptionState m_state = new SubscriptionState();

        private string m_name;

        private SubscriptionFilters m_filters = new SubscriptionFilters();

        private Subscription.CategoryCollection m_categories = new Subscription.CategoryCollection();

        private Subscription.StringCollection m_areas = new Subscription.StringCollection();

        private Subscription.StringCollection m_sources = new Subscription.StringCollection();

        private Subscription.AttributeDictionary m_attributes = new Subscription.AttributeDictionary();

        private class Names
        {
            internal const string STATE = "ST";

            internal const string FILTERS = "FT";

            internal const string ATTRIBUTES = "AT";
        }

        public class CategoryCollection : ReadOnlyCollection
        {
            public int this[int index]
            {
                get { return (int)this.Array.GetValue(index); }
            }

            public new int[] ToArray()
            {
                return (int[])Convert.Clone(this.Array);
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
            public string this[int index]
            {
                get { return (string)this.Array.GetValue(index); }
            }

            public new string[] ToArray()
            {
                return (string[])Convert.Clone(this.Array);
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
            public Subscription.AttributeCollection this[int categoryID]
            {
                get { return (Subscription.AttributeCollection)base[categoryID]; }
            }

            internal AttributeDictionary() : base(null)
            {
            }

            internal AttributeDictionary(Hashtable dictionary) : base(dictionary)
            {
            }

            internal void Update(int categoryID, int[] attributeIDs)
            {
                this.Dictionary[categoryID] = new Subscription.AttributeCollection(attributeIDs);
            }

            protected AttributeDictionary(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }

        [Serializable]
        public class AttributeCollection : ReadOnlyCollection
        {
            public int this[int index]
            {
                get { return (int)this.Array.GetValue(index); }
            }

            public new int[] ToArray()
            {
                return (int[])Convert.Clone(this.Array);
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