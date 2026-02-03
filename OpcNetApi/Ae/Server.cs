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
            m_subscriptions = new SubscriptionCollection();
            for (int i = 0; i < num; i++)
            {
                Subscription subscription = (Subscription)info.GetValue("SU" + i.ToString(), typeof(Subscription));
                m_subscriptions.Add(subscription);
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("CT", m_subscriptions.Count);
            for (int i = 0; i < m_subscriptions.Count; i++)
            {
                info.AddValue("SU" + i.ToString(), m_subscriptions[i]);
            }
        }

        public int AvailableFilters => m_filters;

        public SubscriptionCollection Subscriptions => m_subscriptions;

        public override void Connect(URL url, ConnectData connectData)
        {
            base.Connect(url, connectData);
            if (m_subscriptions.Count == 0)
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

            m_disposing = true;
            foreach (object obj in m_subscriptions)
            {
                Subscription subscription = (Subscription)obj;
                subscription.Dispose();
            }

            m_disposing = false;
            base.Disconnect();
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

        public ISubscription CreateSubscription(SubscriptionState state)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            ISubscription subscription = ((IServer)m_server).CreateSubscription(state);
            if (subscription != null)
            {
                Subscription subscription2 = new Subscription(this, subscription, state);
                m_subscriptions.Add(subscription2);
                return subscription2;
            }

            return null;
        }

        public int QueryAvailableFilters()
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            m_filters = ((IServer)m_server).QueryAvailableFilters();
            return m_filters;
        }

        public Category[] QueryEventCategories(int eventType)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).QueryEventCategories(eventType);
        }

        public string[] QueryConditionNames(int eventCategory)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).QueryConditionNames(eventCategory);
        }

        public string[] QuerySubConditionNames(string conditionName)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).QuerySubConditionNames(conditionName);
        }

        public string[] QueryConditionNames(string sourceName)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).QueryConditionNames(sourceName);
        }

        public Attribute[] QueryEventAttributes(int eventCategory)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).QueryEventAttributes(eventCategory);
        }

        public ItemUrl[] TranslateToItemIDs(string sourceName, int eventCategory, string conditionName, string subConditionName, int[] attributeIDs)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).TranslateToItemIDs(sourceName, eventCategory, conditionName, subConditionName, attributeIDs);
        }

        public Condition GetConditionState(string sourceName, string conditionName, int[] attributeIDs)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).GetConditionState(sourceName, conditionName, attributeIDs);
        }

        public ResultID[] EnableConditionByArea(string[] areas)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).EnableConditionByArea(areas);
        }

        public ResultID[] DisableConditionByArea(string[] areas)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).DisableConditionByArea(areas);
        }

        public ResultID[] EnableConditionBySource(string[] sources)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).EnableConditionBySource(sources);
        }

        public ResultID[] DisableConditionBySource(string[] sources)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).DisableConditionBySource(sources);
        }

        public EnabledStateResult[] GetEnableStateByArea(string[] areas)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).GetEnableStateByArea(areas);
        }

        public EnabledStateResult[] GetEnableStateBySource(string[] sources)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).GetEnableStateBySource(sources);
        }

        public ResultID[] AcknowledgeCondition(string acknowledgerID, string comment, EventAcknowledgement[] conditions)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).AcknowledgeCondition(acknowledgerID, comment, conditions);
        }

        public BrowseElement[] Browse(string areaID, BrowseType browseType, string browseFilter)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).Browse(areaID, browseType, browseFilter);
        }

        public BrowseElement[] Browse(string areaID, BrowseType browseType, string browseFilter, int maxElements, out IBrowsePosition position)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).Browse(areaID, browseType, browseFilter, maxElements, out position);
        }

        public BrowseElement[] BrowseNext(int maxElements, ref IBrowsePosition position)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).BrowseNext(maxElements, ref position);
        }

        internal void SubscriptionDisposed(Subscription subscription)
        {
            if (!m_disposing)
            {
                m_subscriptions.Remove(subscription);
            }
        }

        private Subscription EstablishSubscription(Subscription template)
        {
            ISubscription subscription = null;
            try
            {
                subscription = ((IServer)m_server).CreateSubscription(template.State);
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

        private SubscriptionCollection m_subscriptions = new SubscriptionCollection();

        private class Names
        {
            internal const string COUNT = "CT";

            internal const string SUBSCRIPTION = "SU";
        }

        public class SubscriptionCollection : ReadOnlyCollection
        {
            public Subscription this[int index] => (Subscription)Array.GetValue(index);

            public new Subscription[] ToArray()
            {
                return (Subscription[])Array;
            }

            internal void Add(Subscription subscription)
            {
                Subscription[] array = new Subscription[Count + 1];
                Array.CopyTo(array, 0);
                array[Count] = subscription;
                Array = array;
            }

            internal void Remove(Subscription subscription)
            {
                Subscription[] array = new Subscription[Count - 1];
                int num = 0;
                for (int i = 0; i < Array.Length; i++)
                {
                    Subscription subscription2 = (Subscription)Array.GetValue(i);
                    if (subscription != subscription2)
                    {
                        array[num++] = subscription2;
                    }
                }

                Array = array;
            }

            internal SubscriptionCollection() : base(new Subscription[0])
            {
            }
        }
    }
}