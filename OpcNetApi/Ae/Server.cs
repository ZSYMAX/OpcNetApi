using System;
using System.Collections;
using System.Runtime.Serialization;

namespace Opc.Ae
{
    [Serializable]
    public class Server : Opc.Server, IServer, Opc.IServer, IDisposable, ISerializable
    {
        public Server(Factory factory, URL url) : base(factory, url)
        {
        }

        protected Server(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            int num = (int)info.GetValue("CT", typeof(int));
            this.m_subscriptions = new Server.SubscriptionCollection();
            for (int i = 0; i < num; i++)
            {
                Subscription subscription = (Subscription)info.GetValue("SU" + i.ToString(), typeof(Subscription));
                this.m_subscriptions.Add(subscription);
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("CT", this.m_subscriptions.Count);
            for (int i = 0; i < this.m_subscriptions.Count; i++)
            {
                info.AddValue("SU" + i.ToString(), this.m_subscriptions[i]);
            }
        }

        public int AvailableFilters
        {
            get { return this.m_filters; }
        }

        public Server.SubscriptionCollection Subscriptions
        {
            get { return this.m_subscriptions; }
        }

        public override void Connect(URL url, ConnectData connectData)
        {
            base.Connect(url, connectData);
            if (this.m_subscriptions.Count == 0)
            {
                return;
            }

            Server.SubscriptionCollection subscriptionCollection = new Server.SubscriptionCollection();
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

            this.m_disposing = true;
            foreach (object obj in this.m_subscriptions)
            {
                Subscription subscription = (Subscription)obj;
                subscription.Dispose();
            }

            this.m_disposing = false;
            base.Disconnect();
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

        public ISubscription CreateSubscription(SubscriptionState state)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            ISubscription subscription = ((IServer)this.m_server).CreateSubscription(state);
            if (subscription != null)
            {
                Subscription subscription2 = new Subscription(this, subscription, state);
                this.m_subscriptions.Add(subscription2);
                return subscription2;
            }

            return null;
        }

        public int QueryAvailableFilters()
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            this.m_filters = ((IServer)this.m_server).QueryAvailableFilters();
            return this.m_filters;
        }

        public Category[] QueryEventCategories(int eventType)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)this.m_server).QueryEventCategories(eventType);
        }

        public string[] QueryConditionNames(int eventCategory)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)this.m_server).QueryConditionNames(eventCategory);
        }

        public string[] QuerySubConditionNames(string conditionName)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)this.m_server).QuerySubConditionNames(conditionName);
        }

        public string[] QueryConditionNames(string sourceName)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)this.m_server).QueryConditionNames(sourceName);
        }

        public Attribute[] QueryEventAttributes(int eventCategory)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)this.m_server).QueryEventAttributes(eventCategory);
        }

        public ItemUrl[] TranslateToItemIDs(string sourceName, int eventCategory, string conditionName, string subConditionName, int[] attributeIDs)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)this.m_server).TranslateToItemIDs(sourceName, eventCategory, conditionName, subConditionName, attributeIDs);
        }

        public Condition GetConditionState(string sourceName, string conditionName, int[] attributeIDs)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)this.m_server).GetConditionState(sourceName, conditionName, attributeIDs);
        }

        public ResultID[] EnableConditionByArea(string[] areas)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)this.m_server).EnableConditionByArea(areas);
        }

        public ResultID[] DisableConditionByArea(string[] areas)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)this.m_server).DisableConditionByArea(areas);
        }

        public ResultID[] EnableConditionBySource(string[] sources)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)this.m_server).EnableConditionBySource(sources);
        }

        public ResultID[] DisableConditionBySource(string[] sources)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)this.m_server).DisableConditionBySource(sources);
        }

        public EnabledStateResult[] GetEnableStateByArea(string[] areas)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)this.m_server).GetEnableStateByArea(areas);
        }

        public EnabledStateResult[] GetEnableStateBySource(string[] sources)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)this.m_server).GetEnableStateBySource(sources);
        }

        public ResultID[] AcknowledgeCondition(string acknowledgerID, string comment, EventAcknowledgement[] conditions)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)this.m_server).AcknowledgeCondition(acknowledgerID, comment, conditions);
        }

        public BrowseElement[] Browse(string areaID, BrowseType browseType, string browseFilter)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)this.m_server).Browse(areaID, browseType, browseFilter);
        }

        public BrowseElement[] Browse(string areaID, BrowseType browseType, string browseFilter, int maxElements, out IBrowsePosition position)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)this.m_server).Browse(areaID, browseType, browseFilter, maxElements, out position);
        }

        public BrowseElement[] BrowseNext(int maxElements, ref IBrowsePosition position)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)this.m_server).BrowseNext(maxElements, ref position);
        }

        internal void SubscriptionDisposed(Subscription subscription)
        {
            if (!this.m_disposing)
            {
                this.m_subscriptions.Remove(subscription);
            }
        }

        private Subscription EstablishSubscription(Subscription template)
        {
            ISubscription subscription = null;
            try
            {
                subscription = ((IServer)this.m_server).CreateSubscription(template.State);
                if (subscription == null)
                {
                    return null;
                }

                Subscription subscription2 = new Subscription(this, subscription, template.State);
                subscription2.SetFilters(template.Filters);
                IDictionaryEnumerator enumerator = template.Attributes.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    subscription2.SelectReturnedAttributes((int)enumerator.Key, ((Subscription.AttributeCollection)enumerator.Value).ToArray());
                }

                return subscription2;
            }
            catch
            {
                if (subscription != null)
                {
                    subscription.Dispose();
                    subscription = null;
                }
            }

            return null;
        }

        private int m_filters;

        private bool m_disposing;

        private Server.SubscriptionCollection m_subscriptions = new Server.SubscriptionCollection();

        private class Names
        {
            internal const string COUNT = "CT";

            internal const string SUBSCRIPTION = "SU";
        }

        public class SubscriptionCollection : ReadOnlyCollection
        {
            public Subscription this[int index]
            {
                get { return (Subscription)this.Array.GetValue(index); }
            }

            public new Subscription[] ToArray()
            {
                return (Subscription[])this.Array;
            }

            internal void Add(Subscription subscription)
            {
                Subscription[] array = new Subscription[this.Count + 1];
                this.Array.CopyTo(array, 0);
                array[this.Count] = subscription;
                this.Array = array;
            }

            internal void Remove(Subscription subscription)
            {
                Subscription[] array = new Subscription[this.Count - 1];
                int num = 0;
                for (int i = 0; i < this.Array.Length; i++)
                {
                    Subscription subscription2 = (Subscription)this.Array.GetValue(i);
                    if (subscription != subscription2)
                    {
                        array[num++] = subscription2;
                    }
                }

                this.Array = array;
            }

            internal SubscriptionCollection() : base(new Subscription[0])
            {
            }
        }
    }
}