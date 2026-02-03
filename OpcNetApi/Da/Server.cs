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
            this.m_filters = (int)info.GetValue("Filters", typeof(int));
            Subscription[] array = (Subscription[])info.GetValue("Subscription", typeof(Subscription[]));
            if (array != null)
            {
                foreach (Subscription value in array)
                {
                    this.m_subscriptions.Add(value);
                }
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Filters", this.m_filters);
            Subscription[] array = null;
            if (this.m_subscriptions.Count > 0)
            {
                array = new Subscription[this.m_subscriptions.Count];
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = this.m_subscriptions[i];
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

        public SubscriptionCollection Subscriptions
        {
            get { return this.m_subscriptions; }
        }

        public int Filters
        {
            get { return this.m_filters; }
        }

        public override void Connect(URL url, ConnectData connectData)
        {
            base.Connect(url, connectData);
            if (this.m_subscriptions == null)
            {
                return;
            }

            SubscriptionCollection subscriptionCollection = new SubscriptionCollection();
            foreach (object obj in this.m_subscriptions)
            {
                Subscription template = (Subscription)obj;
                try
                {
                    subscriptionCollection.Add(this.EstablishSubscription(template));
                }
                catch
                {
                }
            }

            this.m_subscriptions = subscriptionCollection;
        }

        public override void Disconnect()
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            if (this.m_subscriptions != null)
            {
                foreach (object obj in this.m_subscriptions)
                {
                    Subscription subscription = (Subscription)obj;
                    subscription.Dispose();
                }

                this.m_subscriptions = null;
            }

            base.Disconnect();
        }

        public int GetResultFilters()
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            this.m_filters = ((IServer)this.m_server).GetResultFilters();
            return this.m_filters;
        }

        public void SetResultFilters(int filters)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            ((IServer)this.m_server).SetResultFilters(filters);
            this.m_filters = filters;
        }

        public ServerStatus GetStatus()
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            ServerStatus status = ((IServer)this.m_server).GetStatus();
            if (status.StatusInfo == null)
            {
                status.StatusInfo = base.GetString("serverState." + status.ServerState.ToString());
            }

            return status;
        }

        public ItemValueResult[] Read(Item[] items)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)this.m_server).Read(items);
        }

        public IdentifiedResult[] Write(ItemValue[] items)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)this.m_server).Write(items);
        }

        public virtual ISubscription CreateSubscription(SubscriptionState state)
        {
            if (state == null)
            {
                throw new ArgumentNullException("state");
            }

            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            ISubscription subscription = ((IServer)this.m_server).CreateSubscription(state);
            subscription.SetResultFilters(this.m_filters);
            SubscriptionCollection subscriptionCollection = new SubscriptionCollection();
            if (this.m_subscriptions != null)
            {
                foreach (object obj in this.m_subscriptions)
                {
                    Subscription value = (Subscription)obj;
                    subscriptionCollection.Add(value);
                }
            }

            subscriptionCollection.Add(this.CreateSubscription(subscription));
            this.m_subscriptions = subscriptionCollection;
            return this.m_subscriptions[this.m_subscriptions.Count - 1];
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

            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            if (!typeof(Subscription).IsInstanceOfType(subscription))
            {
                throw new ArgumentException("Incorrect object type.", "subscription");
            }

            if (!this.Equals(((Subscription)subscription).Server))
            {
                throw new ArgumentException("Unknown subscription.", "subscription");
            }

            SubscriptionCollection subscriptionCollection = new SubscriptionCollection();
            foreach (object obj in this.m_subscriptions)
            {
                Subscription subscription2 = (Subscription)obj;
                if (!subscription.Equals(subscription2))
                {
                    subscriptionCollection.Add(subscription2);
                }
            }

            if (subscriptionCollection.Count == this.m_subscriptions.Count)
            {
                throw new ArgumentException("Subscription not found.", "subscription");
            }

            this.m_subscriptions = subscriptionCollection;
            ((IServer)this.m_server).CancelSubscription(((Subscription)subscription).m_subscription);
        }

        public BrowseElement[] Browse(ItemIdentifier itemID, BrowseFilters filters, out BrowsePosition position)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)this.m_server).Browse(itemID, filters, out position);
        }

        public BrowseElement[] BrowseNext(ref BrowsePosition position)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)this.m_server).BrowseNext(ref position);
        }

        public ItemPropertyCollection[] GetProperties(ItemIdentifier[] itemIDs, PropertyID[] propertyIDs, bool returnValues)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)this.m_server).GetProperties(itemIDs, propertyIDs, returnValues);
        }

        private Subscription EstablishSubscription(Subscription template)
        {
            Subscription subscription = new Subscription(this, ((IServer)this.m_server).CreateSubscription(template.State));
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