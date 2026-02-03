using System;
using System.Runtime.Serialization;

namespace Opc.Hda
{
    [Serializable]
    public class Trend : ISerializable, ICloneable
    {
        public Trend(Server server)
        {
            if (server == null)
            {
                throw new ArgumentNullException("server");
            }

            m_server = server;
            do
            {
                Name = string.Format("Trend{0,2:00}", ++m_count);
            } while (m_server.Trends[Name] != null);
        }

        public Server Server => m_server;

        public string Name
        {
            get => m_name;
            set => m_name = value;
        }

        public int AggregateID
        {
            get => m_aggregateID;
            set => m_aggregateID = value;
        }

        public Time StartTime
        {
            get => m_startTime;
            set => m_startTime = value;
        }

        public Time EndTime
        {
            get => m_endTime;
            set => m_endTime = value;
        }

        public int MaxValues
        {
            get => m_maxValues;
            set => m_maxValues = value;
        }

        public bool IncludeBounds
        {
            get => m_includeBounds;
            set => m_includeBounds = value;
        }

        public decimal ResampleInterval
        {
            get => m_resampleInterval;
            set => m_resampleInterval = value;
        }

        public ItemTimeCollection Timestamps
        {
            get => m_timestamps;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                m_timestamps = value;
            }
        }

        public decimal UpdateInterval
        {
            get => m_updateInterval;
            set => m_updateInterval = value;
        }

        public bool SubscriptionActive => m_subscription != null;

        public decimal PlaybackInterval
        {
            get => m_playbackInterval;
            set => m_playbackInterval = value;
        }

        public decimal PlaybackDuration
        {
            get => m_playbackDuration;
            set => m_playbackDuration = value;
        }

        public bool PlaybackActive => m_playback != null;

        public ItemCollection Items => m_items;

        public Item[] GetItems()
        {
            Item[] array = new Item[m_items.Count];
            for (int i = 0; i < m_items.Count; i++)
            {
                array[i] = m_items[i];
            }

            return array;
        }

        public Item AddItem(ItemIdentifier itemID)
        {
            if (itemID == null)
            {
                throw new ArgumentNullException("itemID");
            }

            if (itemID.ClientHandle == null)
            {
                itemID.ClientHandle = Guid.NewGuid().ToString();
            }

            IdentifiedResult[] array = m_server.CreateItems(new ItemIdentifier[]
            {
                itemID
            });
            if (array == null || array.Length != 1)
            {
                throw new InvalidResponseException();
            }

            if (array[0].ResultID.Failed())
            {
                throw new ResultIDException(array[0].ResultID, "Could not add item to trend.");
            }

            Item item = new Item(array[0]);
            m_items.Add(item);
            return item;
        }

        public void RemoveItem(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            for (int i = 0; i < m_items.Count; i++)
            {
                if (item.Equals(m_items[i]))
                {
                    m_server.ReleaseItems(new ItemIdentifier[]
                    {
                        item
                    });
                    m_items.RemoveAt(i);
                    return;
                }
            }

            throw new ArgumentOutOfRangeException("item", item.Key, "Item not found in collection.");
        }

        public void ClearItems()
        {
            m_server.ReleaseItems(GetItems());
            m_items.Clear();
        }

        public ItemValueCollection[] Read()
        {
            return Read(GetItems());
        }

        public ItemValueCollection[] Read(Item[] items)
        {
            if (AggregateID == 0)
            {
                return ReadRaw(items);
            }

            return ReadProcessed(items);
        }

        public IdentifiedResult[] Read(object requestHandle, ReadValuesEventHandler callback, out IRequest request)
        {
            return Read(GetItems(), requestHandle, callback, out request);
        }

        public IdentifiedResult[] Read(Item[] items, object requestHandle, ReadValuesEventHandler callback, out IRequest request)
        {
            if (AggregateID == 0)
            {
                return ReadRaw(items, requestHandle, callback, out request);
            }

            return ReadProcessed(items, requestHandle, callback, out request);
        }

        public ItemValueCollection[] ReadRaw()
        {
            return ReadRaw(GetItems());
        }

        public ItemValueCollection[] ReadRaw(Item[] items)
        {
            return m_server.ReadRaw(StartTime, EndTime, MaxValues, IncludeBounds, items);
        }

        public IdentifiedResult[] ReadRaw(object requestHandle, ReadValuesEventHandler callback, out IRequest request)
        {
            return Read(GetItems(), requestHandle, callback, out request);
        }

        public IdentifiedResult[] ReadRaw(ItemIdentifier[] items, object requestHandle, ReadValuesEventHandler callback, out IRequest request)
        {
            return m_server.ReadRaw(StartTime, EndTime, MaxValues, IncludeBounds, items, requestHandle, callback, out request);
        }

        public ItemValueCollection[] ReadProcessed()
        {
            return ReadProcessed(GetItems());
        }

        public ItemValueCollection[] ReadProcessed(Item[] items)
        {
            Item[] items2 = ApplyDefaultAggregate(items);
            return m_server.ReadProcessed(StartTime, EndTime, ResampleInterval, items2);
        }

        public IdentifiedResult[] ReadProcessed(object requestHandle, ReadValuesEventHandler callback, out IRequest request)
        {
            return ReadProcessed(GetItems(), requestHandle, callback, out request);
        }

        public IdentifiedResult[] ReadProcessed(Item[] items, object requestHandle, ReadValuesEventHandler callback, out IRequest request)
        {
            Item[] items2 = ApplyDefaultAggregate(items);
            return m_server.ReadProcessed(StartTime, EndTime, ResampleInterval, items2, requestHandle, callback, out request);
        }

        public IdentifiedResult[] Subscribe(object subscriptionHandle, DataUpdateEventHandler callback)
        {
            IdentifiedResult[] result;
            if (AggregateID == 0)
            {
                result = m_server.AdviseRaw(StartTime, UpdateInterval, GetItems(), subscriptionHandle, callback, out m_subscription);
            }
            else
            {
                Item[] items = ApplyDefaultAggregate(GetItems());
                result = m_server.AdviseProcessed(StartTime, ResampleInterval, (int)UpdateInterval, items, subscriptionHandle, callback, out m_subscription);
            }

            return result;
        }

        public void SubscribeCancel()
        {
            if (m_subscription != null)
            {
                m_server.CancelRequest(m_subscription);
                m_subscription = null;
            }
        }

        public IdentifiedResult[] Playback(object playbackHandle, DataUpdateEventHandler callback)
        {
            IdentifiedResult[] result;
            if (AggregateID == 0)
            {
                result = m_server.PlaybackRaw(StartTime, EndTime, MaxValues, PlaybackInterval, PlaybackDuration, GetItems(), playbackHandle, callback, out m_playback);
            }
            else
            {
                Item[] items = ApplyDefaultAggregate(GetItems());
                result = m_server.PlaybackProcessed(StartTime, EndTime, ResampleInterval, (int)PlaybackDuration, PlaybackInterval, items, playbackHandle, callback, out m_playback);
            }

            return result;
        }

        public void PlaybackCancel()
        {
            if (m_playback != null)
            {
                m_server.CancelRequest(m_playback);
                m_playback = null;
            }
        }

        public ModifiedValueCollection[] ReadModified()
        {
            return ReadModified(GetItems());
        }

        public ModifiedValueCollection[] ReadModified(Item[] items)
        {
            return m_server.ReadModified(StartTime, EndTime, MaxValues, items);
        }

        public IdentifiedResult[] ReadModified(object requestHandle, ReadValuesEventHandler callback, out IRequest request)
        {
            return ReadModified(GetItems(), requestHandle, callback, out request);
        }

        public IdentifiedResult[] ReadModified(Item[] items, object requestHandle, ReadValuesEventHandler callback, out IRequest request)
        {
            return m_server.ReadModified(StartTime, EndTime, MaxValues, items, requestHandle, callback, out request);
        }

        public ItemValueCollection[] ReadAtTime()
        {
            return ReadAtTime(GetItems());
        }

        public ItemValueCollection[] ReadAtTime(Item[] items)
        {
            DateTime[] array = new DateTime[Timestamps.Count];
            for (int i = 0; i < Timestamps.Count; i++)
            {
                array[i] = Timestamps[i];
            }

            return m_server.ReadAtTime(array, items);
        }

        public IdentifiedResult[] ReadAtTime(object requestHandle, ReadValuesEventHandler callback, out IRequest request)
        {
            return ReadAtTime(GetItems(), requestHandle, callback, out request);
        }

        public IdentifiedResult[] ReadAtTime(Item[] items, object requestHandle, ReadValuesEventHandler callback, out IRequest request)
        {
            DateTime[] array = new DateTime[Timestamps.Count];
            for (int i = 0; i < Timestamps.Count; i++)
            {
                array[i] = Timestamps[i];
            }

            return m_server.ReadAtTime(array, items, requestHandle, callback, out request);
        }

        public ItemAttributeCollection ReadAttributes(ItemIdentifier item, int[] attributeIDs)
        {
            return m_server.ReadAttributes(StartTime, EndTime, item, attributeIDs);
        }

        public ResultCollection ReadAttributes(ItemIdentifier item, int[] attributeIDs, object requestHandle, ReadAttributesEventHandler callback, out IRequest request)
        {
            return m_server.ReadAttributes(StartTime, EndTime, item, attributeIDs, requestHandle, callback, out request);
        }

        public AnnotationValueCollection[] ReadAnnotations()
        {
            return ReadAnnotations(GetItems());
        }

        public AnnotationValueCollection[] ReadAnnotations(Item[] items)
        {
            return m_server.ReadAnnotations(StartTime, EndTime, items);
        }

        public IdentifiedResult[] ReadAnnotations(object requestHandle, ReadAnnotationsEventHandler callback, out IRequest request)
        {
            return ReadAnnotations(GetItems(), requestHandle, callback, out request);
        }

        public IdentifiedResult[] ReadAnnotations(Item[] items, object requestHandle, ReadAnnotationsEventHandler callback, out IRequest request)
        {
            return m_server.ReadAnnotations(StartTime, EndTime, items, requestHandle, callback, out request);
        }

        public IdentifiedResult[] Delete()
        {
            return Delete(GetItems());
        }

        public IdentifiedResult[] Delete(Item[] items)
        {
            return m_server.Delete(StartTime, EndTime, items);
        }

        public IdentifiedResult[] Delete(object requestHandle, UpdateCompleteEventHandler callback, out IRequest request)
        {
            return Delete(GetItems(), requestHandle, callback, out request);
        }

        public IdentifiedResult[] Delete(ItemIdentifier[] items, object requestHandle, UpdateCompleteEventHandler callback, out IRequest request)
        {
            return m_server.Delete(StartTime, EndTime, items, requestHandle, callback, out request);
        }

        public ResultCollection[] DeleteAtTime()
        {
            return DeleteAtTime(GetItems());
        }

        public ResultCollection[] DeleteAtTime(Item[] items)
        {
            ItemTimeCollection[] array = new ItemTimeCollection[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                array[i] = (ItemTimeCollection)Timestamps.Clone();
                array[i].ItemName = items[i].ItemName;
                array[i].ItemPath = items[i].ItemPath;
                array[i].ClientHandle = items[i].ClientHandle;
                array[i].ServerHandle = items[i].ServerHandle;
            }

            return m_server.DeleteAtTime(array);
        }

        public IdentifiedResult[] DeleteAtTime(object requestHandle, UpdateCompleteEventHandler callback, out IRequest request)
        {
            return DeleteAtTime(GetItems(), requestHandle, callback, out request);
        }

        public IdentifiedResult[] DeleteAtTime(Item[] items, object requestHandle, UpdateCompleteEventHandler callback, out IRequest request)
        {
            ItemTimeCollection[] array = new ItemTimeCollection[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                array[i] = (ItemTimeCollection)Timestamps.Clone();
                array[i].ItemName = items[i].ItemName;
                array[i].ItemPath = items[i].ItemPath;
                array[i].ClientHandle = items[i].ClientHandle;
                array[i].ServerHandle = items[i].ServerHandle;
            }

            return m_server.DeleteAtTime(array, requestHandle, callback, out request);
        }

        protected Trend(SerializationInfo info, StreamingContext context)
        {
            m_name = (string)info.GetValue("Name", typeof(string));
            m_aggregateID = (int)info.GetValue("AggregateID", typeof(int));
            m_startTime = (Time)info.GetValue("StartTime", typeof(Time));
            m_endTime = (Time)info.GetValue("EndTime", typeof(Time));
            m_maxValues = (int)info.GetValue("MaxValues", typeof(int));
            m_includeBounds = (bool)info.GetValue("IncludeBounds", typeof(bool));
            m_resampleInterval = (decimal)info.GetValue("ResampleInterval", typeof(decimal));
            m_updateInterval = (decimal)info.GetValue("UpdateInterval", typeof(decimal));
            m_playbackInterval = (decimal)info.GetValue("PlaybackInterval", typeof(decimal));
            m_playbackDuration = (decimal)info.GetValue("PlaybackDuration", typeof(decimal));
            DateTime[] array = (DateTime[])info.GetValue("Timestamps", typeof(DateTime[]));
            if (array != null)
            {
                foreach (DateTime value in array)
                {
                    m_timestamps.Add(value);
                }
            }

            Item[] array3 = (Item[])info.GetValue("Items", typeof(Item[]));
            if (array3 != null)
            {
                foreach (Item value2 in array3)
                {
                    m_items.Add(value2);
                }
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", m_name);
            info.AddValue("AggregateID", m_aggregateID);
            info.AddValue("StartTime", m_startTime);
            info.AddValue("EndTime", m_endTime);
            info.AddValue("MaxValues", m_maxValues);
            info.AddValue("IncludeBounds", m_includeBounds);
            info.AddValue("ResampleInterval", m_resampleInterval);
            info.AddValue("UpdateInterval", m_updateInterval);
            info.AddValue("PlaybackInterval", m_playbackInterval);
            info.AddValue("PlaybackDuration", m_playbackDuration);
            DateTime[] array = null;
            if (m_timestamps.Count > 0)
            {
                array = new DateTime[m_timestamps.Count];
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = m_timestamps[i];
                }
            }

            info.AddValue("Timestamps", array);
            Item[] array2 = null;
            if (m_items.Count > 0)
            {
                array2 = new Item[m_items.Count];
                for (int j = 0; j < array2.Length; j++)
                {
                    array2[j] = m_items[j];
                }
            }

            info.AddValue("Items", array2);
        }

        internal void SetServer(Server server)
        {
            m_server = server;
        }

        public virtual object Clone()
        {
            Trend trend = (Trend)MemberwiseClone();
            trend.m_items = new ItemCollection();
            foreach (object obj in m_items)
            {
                Item item = (Item)obj;
                trend.m_items.Add(item.Clone());
            }

            trend.m_timestamps = new ItemTimeCollection();
            foreach (object obj2 in m_timestamps)
            {
                DateTime value = (DateTime)obj2;
                trend.m_timestamps.Add(value);
            }

            trend.m_subscription = null;
            trend.m_playback = null;
            return trend;
        }

        private Item[] ApplyDefaultAggregate(Item[] items)
        {
            int num = AggregateID;
            if (num == 0)
            {
                num = 1;
            }

            Item[] array = new Item[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                array[i] = new Item(items[i]);
                if (array[i].AggregateID == 0)
                {
                    array[i].AggregateID = num;
                }
            }

            return array;
        }

        private static int m_count;

        private Server m_server;

        private string m_name;

        private int m_aggregateID;

        private Time m_startTime;

        private Time m_endTime;

        private int m_maxValues;

        private bool m_includeBounds;

        private decimal m_resampleInterval;

        private ItemTimeCollection m_timestamps = new ItemTimeCollection();

        private ItemCollection m_items = new ItemCollection();

        private decimal m_updateInterval;

        private decimal m_playbackInterval;

        private decimal m_playbackDuration;

        private IRequest m_subscription;

        private IRequest m_playback;

        private class Names
        {
            internal const string NAME = "Name";

            internal const string AGGREGATE_ID = "AggregateID";

            internal const string START_TIME = "StartTime";

            internal const string END_TIME = "EndTime";

            internal const string MAX_VALUES = "MaxValues";

            internal const string INCLUDE_BOUNDS = "IncludeBounds";

            internal const string RESAMPLE_INTERVAL = "ResampleInterval";

            internal const string UPDATE_INTERVAL = "UpdateInterval";

            internal const string PLAYBACK_INTERVAL = "PlaybackInterval";

            internal const string PLAYBACK_DURATION = "PlaybackDuration";

            internal const string TIMESTAMPS = "Timestamps";

            internal const string ITEMS = "Items";
        }
    }
}