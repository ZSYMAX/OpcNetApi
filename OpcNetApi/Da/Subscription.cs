using System;
using System.Collections;
using System.Runtime.Serialization;

namespace Opc.Da
{
    [Serializable]
    public class Subscription : ISubscription, IDisposable, ISerializable, ICloneable
    {
        public Subscription(Server server, ISubscription subscription)
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
            this.GetResultFilters();
            this.GetState();
        }

        public void Dispose()
        {
            if (this.m_subscription != null)
            {
                this.m_subscription.Dispose();
                this.m_server = null;
                this.m_subscription = null;
                this.m_items = null;
            }
        }

        protected Subscription(SerializationInfo info, StreamingContext context)
        {
            this.m_state = (SubscriptionState)info.GetValue("State", typeof(SubscriptionState));
            this.m_filters = (int)info.GetValue("Filters", typeof(int));
            this.m_items = (Item[])info.GetValue("Items", typeof(Item[]));
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("State", this.m_state);
            info.AddValue("Filters", this.m_filters);
            info.AddValue("Items", this.m_items);
        }

        public virtual object Clone()
        {
            Subscription subscription = (Subscription)base.MemberwiseClone();
            subscription.m_server = null;
            subscription.m_subscription = null;
            subscription.m_state = (SubscriptionState)this.m_state.Clone();
            subscription.m_state.ServerHandle = null;
            subscription.m_state.Active = false;
            if (subscription.m_items != null)
            {
                ArrayList arrayList = new ArrayList();
                foreach (Item item in subscription.m_items)
                {
                    arrayList.Add(item.Clone());
                }

                subscription.m_items = (Item[])arrayList.ToArray(typeof(Item));
            }

            return subscription;
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

        public object ServerHandle
        {
            get { return this.m_state.ServerHandle; }
        }

        public bool Active
        {
            get { return this.m_state.Active; }
        }

        public bool Enabled
        {
            get { return this.m_enabled; }
        }

        public string Locale
        {
            get { return this.m_state.Locale; }
        }

        public int Filters
        {
            get { return this.m_filters; }
        }

        public SubscriptionState State
        {
            get { return (SubscriptionState)this.m_state.Clone(); }
        }

        public Item[] Items
        {
            get
            {
                if (this.m_items == null)
                {
                    return new Item[0];
                }

                Item[] array = new Item[this.m_items.Length];
                for (int i = 0; i < this.m_items.Length; i++)
                {
                    array[i] = (Item)this.m_items[i].Clone();
                }

                return array;
            }
        }

        public event DataChangedEventHandler DataChanged
        {
            add { this.m_subscription.DataChanged += value; }
            remove { this.m_subscription.DataChanged -= value; }
        }

        public int GetResultFilters()
        {
            this.m_filters = this.m_subscription.GetResultFilters();
            return this.m_filters;
        }

        public void SetResultFilters(int filters)
        {
            this.m_subscription.SetResultFilters(filters);
            this.m_filters = filters;
        }

        public SubscriptionState GetState()
        {
            this.m_state = this.m_subscription.GetState();
            return this.m_state;
        }

        public SubscriptionState ModifyState(int masks, SubscriptionState state)
        {
            this.m_state = this.m_subscription.ModifyState(masks, state);
            return this.m_state;
        }

        public virtual ItemResult[] AddItems(Item[] items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            if (items.Length == 0)
            {
                return new ItemResult[0];
            }

            ItemResult[] array = this.m_subscription.AddItems(items);
            if (array == null || array.Length == 0)
            {
                throw new InvalidResponseException();
            }

            ArrayList arrayList = new ArrayList();
            if (this.m_items != null)
            {
                arrayList.AddRange(this.m_items);
            }

            for (int i = 0; i < array.Length; i++)
            {
                if (!array[i].ResultID.Failed())
                {
                    arrayList.Add(new Item(array[i])
                    {
                        ItemName = items[i].ItemName,
                        ItemPath = items[i].ItemPath,
                        ClientHandle = items[i].ClientHandle
                    });
                }
            }

            this.m_items = (Item[])arrayList.ToArray(typeof(Item));
            this.GetState();
            return array;
        }

        public virtual ItemResult[] ModifyItems(int masks, Item[] items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            if (items.Length == 0)
            {
                return new ItemResult[0];
            }

            ItemResult[] array = this.m_subscription.ModifyItems(masks, items);
            if (array == null || array.Length == 0)
            {
                throw new InvalidResponseException();
            }

            for (int i = 0; i < array.Length; i++)
            {
                if (!array[i].ResultID.Failed())
                {
                    for (int j = 0; j < this.m_items.Length; j++)
                    {
                        if (this.m_items[j].ServerHandle.Equals(items[i].ServerHandle))
                        {
                            Item item = new Item(array[i]);
                            item.ItemName = this.m_items[j].ItemName;
                            item.ItemPath = this.m_items[j].ItemPath;
                            item.ClientHandle = this.m_items[j].ClientHandle;
                            this.m_items[j] = item;
                            break;
                        }
                    }
                }
            }

            this.GetState();
            return array;
        }

        public virtual IdentifiedResult[] RemoveItems(ItemIdentifier[] items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            if (items.Length == 0)
            {
                return new IdentifiedResult[0];
            }

            IdentifiedResult[] array = this.m_subscription.RemoveItems(items);
            if (array == null || array.Length == 0)
            {
                throw new InvalidResponseException();
            }

            ArrayList arrayList = new ArrayList();
            foreach (Item item in this.m_items)
            {
                bool flag = false;
                for (int j = 0; j < array.Length; j++)
                {
                    if (item.ServerHandle.Equals(items[j].ServerHandle))
                    {
                        flag = array[j].ResultID.Succeeded();
                        break;
                    }
                }

                if (!flag)
                {
                    arrayList.Add(item);
                }
            }

            this.m_items = (Item[])arrayList.ToArray(typeof(Item));
            this.GetState();
            return array;
        }

        public ItemValueResult[] Read(Item[] items)
        {
            return this.m_subscription.Read(items);
        }

        public IdentifiedResult[] Write(ItemValue[] items)
        {
            return this.m_subscription.Write(items);
        }

        public IdentifiedResult[] Read(Item[] items, object requestHandle, ReadCompleteEventHandler callback, out IRequest request)
        {
            return this.m_subscription.Read(items, requestHandle, callback, out request);
        }

        public IdentifiedResult[] Write(ItemValue[] items, object requestHandle, WriteCompleteEventHandler callback, out IRequest request)
        {
            return this.m_subscription.Write(items, requestHandle, callback, out request);
        }

        public void Cancel(IRequest request, CancelCompleteEventHandler callback)
        {
            this.m_subscription.Cancel(request, callback);
        }

        public void Refresh()
        {
            this.m_subscription.Refresh();
        }

        public void Refresh(object requestHandle, out IRequest request)
        {
            this.m_subscription.Refresh(requestHandle, out request);
        }

        public void SetEnabled(bool enabled)
        {
            this.m_subscription.SetEnabled(enabled);
            this.m_enabled = enabled;
        }

        public bool GetEnabled()
        {
            this.m_enabled = this.m_subscription.GetEnabled();
            return this.m_enabled;
        }

        internal Server m_server;

        internal ISubscription m_subscription;

        private SubscriptionState m_state = new SubscriptionState();

        private Item[] m_items;

        private bool m_enabled = true;

        private int m_filters = 9;

        private class Names
        {
            internal const string STATE = "State";

            internal const string FILTERS = "Filters";

            internal const string ITEMS = "Items";
        }
    }
}