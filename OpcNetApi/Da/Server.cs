using System;
using System.Runtime.Serialization;

namespace Opc.Da
{
    [Serializable]
    public class Server : Opc.Server, IServer, Opc.IServer, IDisposable
    {
        public Server(Factory factory, URL url) : base(factory, url)
        {
        }

        protected Server(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            m_filters = (int)info.GetValue("Filters", typeof(int));
            Subscription[] array = (Subscription[])info.GetValue("Subscription", typeof(Subscription[]));
            if (array != null)
            {
                foreach (Subscription value in array)
                {
                    m_subscriptions.Add(value);
                }
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Filters", m_filters);
            Subscription[] array = null;
            if (m_subscriptions.Count > 0)
            {
                array = new Subscription[m_subscriptions.Count];
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = m_subscriptions[i];
                }
            }

            info.AddValue("Subscription", array);
        }

        public override object Clone()
        {
            Server server = (Server)base.Clone();
            if (server.m_subscriptions != null)
            {
                SubscriptionCollection subscriptionCollection = new SubscriptionCollection();
                foreach (object obj in server.m_subscriptions)
                {
                    Subscription subscription = (Subscription)obj;
                    subscriptionCollection.Add(subscription.Clone());
                }

                server.m_subscriptions = subscriptionCollection;
            }

            return server;
        }

        public SubscriptionCollection Subscriptions => m_subscriptions;

        public int Filters => m_filters;

        public override void Connect(URL url, ConnectData connectData)
        {
            base.Connect(url, connectData);
            if (m_subscriptions == null)
            {
                return;
            }

            SubscriptionCollection subscriptionCollection = new SubscriptionCollection();
            foreach (object obj in m_subscriptions)
            {
                Subscription template = (Subscription)obj;
                try
                {
                    subscriptionCollection.Add(EstablishSubscription(template));
                }
                catch
                {
                }
            }

            m_subscriptions = subscriptionCollection;
        }

        public override void Disconnect()
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            if (m_subscriptions != null)
            {
                foreach (object obj in m_subscriptions)
                {
                    Subscription subscription = (Subscription)obj;
                    subscription.Dispose();
                }

                m_subscriptions = null;
            }

            base.Disconnect();
        }

        public int GetResultFilters()
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            m_filters = ((IServer)m_server).GetResultFilters();
            return m_filters;
        }

        public void SetResultFilters(int filters)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            ((IServer)m_server).SetResultFilters(filters);
            m_filters = filters;
        }

        public ServerStatus GetStatus()
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            ServerStatus status = ((IServer)m_server).GetStatus();
            if (status.StatusInfo == null)
            {
                status.StatusInfo = GetString("serverState." + status.ServerState.ToString());
            }

            return status;
        }

        public ItemValueResult[] Read(Item[] items)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).Read(items);
        }

        public IdentifiedResult[] Write(ItemValue[] items)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).Write(items);
        }

        public virtual ISubscription CreateSubscription(SubscriptionState state)
        {
            if (state == null)
            {
                throw new ArgumentNullException("state");
            }

            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            ISubscription subscription = ((IServer)m_server).CreateSubscription(state);
            subscription.SetResultFilters(m_filters);
            SubscriptionCollection subscriptionCollection = new SubscriptionCollection();
            if (m_subscriptions != null)
            {
                foreach (object obj in m_subscriptions)
                {
                    Subscription value = (Subscription)obj;
                    subscriptionCollection.Add(value);
                }
            }

            subscriptionCollection.Add(CreateSubscription(subscription));
            m_subscriptions = subscriptionCollection;
            return m_subscriptions[m_subscriptions.Count - 1];
        }

        protected virtual Subscription CreateSubscription(ISubscription subscription)
        {
            return new Subscription(this, subscription);
        }

        public virtual void CancelSubscription(ISubscription subscription)
        {
            if (subscription == null)
            {
                throw new ArgumentNullException("subscription");
            }

            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            if (!typeof(Subscription).IsInstanceOfType(subscription))
            {
                throw new ArgumentException("Incorrect object type.", "subscription");
            }

            if (!Equals(((Subscription)subscription).Server))
            {
                throw new ArgumentException("Unknown subscription.", "subscription");
            }

            SubscriptionCollection subscriptionCollection = new SubscriptionCollection();
            foreach (object obj in m_subscriptions)
            {
                Subscription subscription2 = (Subscription)obj;
                if (!subscription.Equals(subscription2))
                {
                    subscriptionCollection.Add(subscription2);
                }
            }

            if (subscriptionCollection.Count == m_subscriptions.Count)
            {
                throw new ArgumentException("Subscription not found.", "subscription");
            }

            m_subscriptions = subscriptionCollection;
            ((IServer)m_server).CancelSubscription(((Subscription)subscription).m_subscription);
        }

        public BrowseElement[] Browse(ItemIdentifier itemID, BrowseFilters filters, out BrowsePosition position)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).Browse(itemID, filters, out position);
        }

        public BrowseElement[] BrowseNext(ref BrowsePosition position)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).BrowseNext(ref position);
        }

        public ItemPropertyCollection[] GetProperties(ItemIdentifier[] itemIDs, PropertyID[] propertyIDs, bool returnValues)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).GetProperties(itemIDs, propertyIDs, returnValues);
        }

        private Subscription EstablishSubscription(Subscription template)
        {
            Subscription subscription = new Subscription(this, ((IServer)m_server).CreateSubscription(template.State));
            subscription.SetResultFilters(template.Filters);
            try
            {
                subscription.AddItems(template.Items);
            }
            catch
            {
                subscription.Dispose();
                subscription = null;
            }

            return subscription;
        }

        private SubscriptionCollection m_subscriptions = new SubscriptionCollection();

        private int m_filters = 9;

        private class Names
        {
            internal const string FILTERS = "Filters";

            internal const string SUBSCRIPTIONS = "Subscription";
        }
    }
}