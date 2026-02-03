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

            m_server = server;
            m_subscription = subscription;
            GetResultFilters();
            GetState();
        }

        public void Dispose()
        {
            if (m_subscription != null)
            {
                m_subscription.Dispose();
                m_server = null;
                m_subscription = null;
                m_items = null;
            }
        }

        protected Subscription(SerializationInfo info, StreamingContext context)
        {
            m_state = (SubscriptionState)info.GetValue("State", typeof(SubscriptionState));
            m_filters = (int)info.GetValue("Filters", typeof(int));
            m_items = (Item[])info.GetValue("Items", typeof(Item[]));
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("State", m_state);
            info.AddValue("Filters", m_filters);
            info.AddValue("Items", m_items);
        }

        public virtual object Clone()
        {
            Subscription subscription = (Subscription)MemberwiseClone();
            subscription.m_server = null;
            subscription.m_subscription = null;
            subscription.m_state = (SubscriptionState)m_state.Clone();
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

        public Server Server => m_server;

        public string Name => m_state.Name;

        public object ClientHandle => m_state.ClientHandle;

        public object ServerHandle => m_state.ServerHandle;

        public bool Active => m_state.Active;

        public bool Enabled => m_enabled;

        public string Locale => m_state.Locale;

        public int Filters => m_filters;

        public SubscriptionState State => (SubscriptionState)m_state.Clone();

        public Item[] Items
        {
            get
            {
                if (m_items == null)
                {
                    return new Item[0];
                }

                Item[] array = new Item[m_items.Length];
                for (int i = 0; i < m_items.Length; i++)
                {
                    array[i] = (Item)m_items[i].Clone();
                }

                return array;
            }
        }

        public event DataChangedEventHandler DataChanged
        {
            add => m_subscription.DataChanged += value;
            remove => m_subscription.DataChanged -= value;
        }

        public int GetResultFilters()
        {
            m_filters = m_subscription.GetResultFilters();
            return m_filters;
        }

        public void SetResultFilters(int filters)
        {
            m_subscription.SetResultFilters(filters);
            m_filters = filters;
        }

        public SubscriptionState GetState()
        {
            m_state = m_subscription.GetState();
            return m_state;
        }

        public SubscriptionState ModifyState(int masks, SubscriptionState state)
        {
            m_state = m_subscription.ModifyState(masks, state);
            return m_state;
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

            ItemResult[] array = m_subscription.AddItems(items);
            if (array == null || array.Length == 0)
            {
                throw new InvalidResponseException();
            }

            ArrayList arrayList = new ArrayList();
            if (m_items != null)
            {
                arrayList.AddRange(m_items);
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

            m_items = (Item[])arrayList.ToArray(typeof(Item));
            GetState();
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

            ItemResult[] array = m_subscription.ModifyItems(masks, items);
            if (array == null || array.Length == 0)
            {
                throw new InvalidResponseException();
            }

            for (int i = 0; i < array.Length; i++)
            {
                if (!array[i].ResultID.Failed())
                {
                    for (int j = 0; j < m_items.Length; j++)
                    {
                        if (m_items[j].ServerHandle.Equals(items[i].ServerHandle))
                        {
                            Item item = new Item(array[i]);
                            item.ItemName = m_items[j].ItemName;
                            item.ItemPath = m_items[j].ItemPath;
                            item.ClientHandle = m_items[j].ClientHandle;
                            m_items[j] = item;
                            break;
                        }
                    }
                }
            }

            GetState();
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

            IdentifiedResult[] array = m_subscription.RemoveItems(items);
            if (array == null || array.Length == 0)
            {
                throw new InvalidResponseException();
            }

            ArrayList arrayList = new ArrayList();
            foreach (Item item in m_items)
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

            m_items = (Item[])arrayList.ToArray(typeof(Item));
            GetState();
            return array;
        }

        public ItemValueResult[] Read(Item[] items)
        {
            return m_subscription.Read(items);
        }

        public IdentifiedResult[] Write(ItemValue[] items)
        {
            return m_subscription.Write(items);
        }

        public IdentifiedResult[] Read(Item[] items, object requestHandle, ReadCompleteEventHandler callback, out IRequest request)
        {
            return m_subscription.Read(items, requestHandle, callback, out request);
        }

        public IdentifiedResult[] Write(ItemValue[] items, object requestHandle, WriteCompleteEventHandler callback, out IRequest request)
        {
            return m_subscription.Write(items, requestHandle, callback, out request);
        }

        public void Cancel(IRequest request, CancelCompleteEventHandler callback)
        {
            m_subscription.Cancel(request, callback);
        }

        public void Refresh()
        {
            m_subscription.Refresh();
        }

        public void Refresh(object requestHandle, out IRequest request)
        {
            m_subscription.Refresh(requestHandle, out request);
        }

        public void SetEnabled(bool enabled)
        {
            m_subscription.SetEnabled(enabled);
            m_enabled = enabled;
        }

        public bool GetEnabled()
        {
            m_enabled = m_subscription.GetEnabled();
            return m_enabled;
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