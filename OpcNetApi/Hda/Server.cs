using System;
using System.Collections;
using System.Runtime.Serialization;

namespace Opc.Hda
{
    [Serializable]
    public class Server : Opc.Server, IServer, Opc.IServer, IDisposable
    {
        public Server(Factory factory, URL url) : base(factory, url)
        {
        }

        public AttributeCollection Attributes => m_attributes;

        public AggregateCollection Aggregates => m_aggregates;

        public ItemIdentifierCollection Items => new ItemIdentifierCollection(m_items.Values);

        public TrendCollection Trends => m_trends;

        public override void Connect(URL url, ConnectData connectData)
        {
            base.Connect(url, connectData);
            GetAttributes();
            GetAggregates();
            foreach (object obj in m_trends)
            {
                Trend trend = (Trend)obj;
                ArrayList arrayList = new ArrayList();
                foreach (object obj2 in trend.Items)
                {
                    Item itemID = (Item)obj2;
                    arrayList.Add(new ItemIdentifier(itemID));
                }

                IdentifiedResult[] array = CreateItems((ItemIdentifier[])arrayList.ToArray(typeof(ItemIdentifier)));
                if (array != null)
                {
                    for (int i = 0; i < array.Length; i++)
                    {
                        trend.Items[i].ServerHandle = null;
                        if (array[i].ResultID.Succeeded())
                        {
                            trend.Items[i].ServerHandle = array[i].ServerHandle;
                        }
                    }
                }
            }
        }

        public override void Disconnect()
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            if (m_items.Count > 0)
            {
                try
                {
                    ArrayList arrayList = new ArrayList(m_items.Count);
                    arrayList.AddRange(m_items);
                    ((IServer)m_server).ReleaseItems((ItemIdentifier[])arrayList.ToArray(typeof(ItemIdentifier)));
                }
                catch
                {
                }

                m_items.Clear();
            }

            foreach (object obj in m_trends)
            {
                Trend trend = (Trend)obj;
                foreach (object obj2 in trend.Items)
                {
                    Item item = (Item)obj2;
                    item.ServerHandle = null;
                }
            }

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

        public Attribute[] GetAttributes()
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            m_attributes.Clear();
            Attribute[] attributes = ((IServer)m_server).GetAttributes();
            if (attributes != null)
            {
                m_attributes.Init(attributes);
            }

            return attributes;
        }

        public Aggregate[] GetAggregates()
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            m_aggregates.Clear();
            Aggregate[] aggregates = ((IServer)m_server).GetAggregates();
            if (aggregates != null)
            {
                m_aggregates.Init(aggregates);
            }

            return aggregates;
        }

        public IBrowser CreateBrowser(BrowseFilter[] filters, out ResultID[] results)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).CreateBrowser(filters, out results);
        }

        public IdentifiedResult[] CreateItems(ItemIdentifier[] items)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            IdentifiedResult[] array = ((IServer)m_server).CreateItems(items);
            if (array != null)
            {
                foreach (IdentifiedResult identifiedResult in array)
                {
                    if (identifiedResult.ResultID.Succeeded())
                    {
                        m_items.Add(identifiedResult.ServerHandle, new ItemIdentifier(identifiedResult));
                    }
                }
            }

            return array;
        }

        public IdentifiedResult[] ReleaseItems(ItemIdentifier[] items)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            IdentifiedResult[] array = ((IServer)m_server).ReleaseItems(items);
            if (array != null)
            {
                foreach (IdentifiedResult identifiedResult in array)
                {
                    if (identifiedResult.ResultID.Succeeded())
                    {
                        m_items.Remove(identifiedResult.ServerHandle);
                    }
                }
            }

            return array;
        }

        public IdentifiedResult[] ValidateItems(ItemIdentifier[] items)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).ValidateItems(items);
        }

        public ItemValueCollection[] ReadRaw(Time startTime, Time endTime, int maxValues, bool includeBounds, ItemIdentifier[] items)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).ReadRaw(startTime, endTime, maxValues, includeBounds, items);
        }

        public IdentifiedResult[] ReadRaw(Time startTime, Time endTime, int maxValues, bool includeBounds, ItemIdentifier[] items, object requestHandle, ReadValuesEventHandler callback, out IRequest request)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).ReadRaw(startTime, endTime, maxValues, includeBounds, items, requestHandle, callback, out request);
        }

        public IdentifiedResult[] AdviseRaw(Time startTime, decimal updateInterval, ItemIdentifier[] items, object requestHandle, DataUpdateEventHandler callback, out IRequest request)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).AdviseRaw(startTime, updateInterval, items, requestHandle, callback, out request);
        }

        public IdentifiedResult[] PlaybackRaw(Time startTime, Time endTime, int maxValues, decimal updateInterval, decimal playbackDuration, ItemIdentifier[] items, object requestHandle, DataUpdateEventHandler callback,
            out IRequest request)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).PlaybackRaw(startTime, endTime, maxValues, updateInterval, playbackDuration, items, requestHandle, callback, out request);
        }

        public ItemValueCollection[] ReadProcessed(Time startTime, Time endTime, decimal resampleInterval, Item[] items)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).ReadProcessed(startTime, endTime, resampleInterval, items);
        }

        public IdentifiedResult[] ReadProcessed(Time startTime, Time endTime, decimal resampleInterval, Item[] items, object requestHandle, ReadValuesEventHandler callback, out IRequest request)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).ReadProcessed(startTime, endTime, resampleInterval, items, requestHandle, callback, out request);
        }

        public IdentifiedResult[] AdviseProcessed(Time startTime, decimal resampleInterval, int numberOfIntervals, Item[] items, object requestHandle, DataUpdateEventHandler callback, out IRequest request)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).AdviseProcessed(startTime, resampleInterval, numberOfIntervals, items, requestHandle, callback, out request);
        }

        public IdentifiedResult[] PlaybackProcessed(Time startTime, Time endTime, decimal resampleInterval, int numberOfIntervals, decimal updateInterval, Item[] items, object requestHandle, DataUpdateEventHandler callback,
            out IRequest request)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).PlaybackProcessed(startTime, endTime, resampleInterval, numberOfIntervals, updateInterval, items, requestHandle, callback, out request);
        }

        public ItemValueCollection[] ReadAtTime(DateTime[] timestamps, ItemIdentifier[] items)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).ReadAtTime(timestamps, items);
        }

        public IdentifiedResult[] ReadAtTime(DateTime[] timestamps, ItemIdentifier[] items, object requestHandle, ReadValuesEventHandler callback, out IRequest request)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).ReadAtTime(timestamps, items, requestHandle, callback, out request);
        }

        public ModifiedValueCollection[] ReadModified(Time startTime, Time endTime, int maxValues, ItemIdentifier[] items)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).ReadModified(startTime, endTime, maxValues, items);
        }

        public IdentifiedResult[] ReadModified(Time startTime, Time endTime, int maxValues, ItemIdentifier[] items, object requestHandle, ReadValuesEventHandler callback, out IRequest request)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).ReadModified(startTime, endTime, maxValues, items, requestHandle, callback, out request);
        }

        public ItemAttributeCollection ReadAttributes(Time startTime, Time endTime, ItemIdentifier item, int[] attributeIDs)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).ReadAttributes(startTime, endTime, item, attributeIDs);
        }

        public ResultCollection ReadAttributes(Time startTime, Time endTime, ItemIdentifier item, int[] attributeIDs, object requestHandle, ReadAttributesEventHandler callback, out IRequest request)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).ReadAttributes(startTime, endTime, item, attributeIDs, requestHandle, callback, out request);
        }

        public AnnotationValueCollection[] ReadAnnotations(Time startTime, Time endTime, ItemIdentifier[] items)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).ReadAnnotations(startTime, endTime, items);
        }

        public IdentifiedResult[] ReadAnnotations(Time startTime, Time endTime, ItemIdentifier[] items, object requestHandle, ReadAnnotationsEventHandler callback, out IRequest request)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).ReadAnnotations(startTime, endTime, items, requestHandle, callback, out request);
        }

        public ResultCollection[] InsertAnnotations(AnnotationValueCollection[] items)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).InsertAnnotations(items);
        }

        public IdentifiedResult[] InsertAnnotations(AnnotationValueCollection[] items, object requestHandle, UpdateCompleteEventHandler callback, out IRequest request)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).InsertAnnotations(items, requestHandle, callback, out request);
        }

        public ResultCollection[] Insert(ItemValueCollection[] items, bool replace)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).Insert(items, replace);
        }

        public IdentifiedResult[] Insert(ItemValueCollection[] items, bool replace, object requestHandle, UpdateCompleteEventHandler callback, out IRequest request)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).Insert(items, replace, requestHandle, callback, out request);
        }

        public ResultCollection[] Replace(ItemValueCollection[] items)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).Replace(items);
        }

        public IdentifiedResult[] Replace(ItemValueCollection[] items, object requestHandle, UpdateCompleteEventHandler callback, out IRequest request)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).Replace(items, requestHandle, callback, out request);
        }

        public IdentifiedResult[] Delete(Time startTime, Time endTime, ItemIdentifier[] items)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).Delete(startTime, endTime, items);
        }

        public IdentifiedResult[] Delete(Time startTime, Time endTime, ItemIdentifier[] items, object requestHandle, UpdateCompleteEventHandler callback, out IRequest request)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).Delete(startTime, endTime, items, requestHandle, callback, out request);
        }

        public ResultCollection[] DeleteAtTime(ItemTimeCollection[] items)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).DeleteAtTime(items);
        }

        public IdentifiedResult[] DeleteAtTime(ItemTimeCollection[] items, object requestHandle, UpdateCompleteEventHandler callback, out IRequest request)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).DeleteAtTime(items, requestHandle, callback, out request);
        }

        public void CancelRequest(IRequest request)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            ((IServer)m_server).CancelRequest(request);
        }

        public void CancelRequest(IRequest request, CancelCompleteEventHandler callback)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            ((IServer)m_server).CancelRequest(request, callback);
        }

        protected Server(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Trend[] array = (Trend[])info.GetValue("Trends", typeof(Trend[]));
            if (array != null)
            {
                foreach (Trend trend in array)
                {
                    trend.SetServer(this);
                    m_trends.Add(trend);
                }
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            Trend[] array = null;
            if (m_trends.Count > 0)
            {
                array = new Trend[m_trends.Count];
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = m_trends[i];
                }
            }

            info.AddValue("Trends", array);
        }

        public override object Clone()
        {
            return (Server)base.Clone();
        }

        private Hashtable m_items = new Hashtable();

        private AttributeCollection m_attributes = new AttributeCollection();

        private AggregateCollection m_aggregates = new AggregateCollection();

        private TrendCollection m_trends = new TrendCollection();

        private class Names
        {
            internal const string TRENDS = "Trends";
        }
    }
}