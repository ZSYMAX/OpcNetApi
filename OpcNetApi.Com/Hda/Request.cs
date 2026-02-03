using System;
using System.Collections;
using Opc;
using Opc.Hda;

namespace OpcCom.Hda
{
    internal class Request : IRequest, IActualTime
    {
        public int RequestID => m_requestID;

        public int CancelID => m_cancelID;

        public event CancelCompleteEventHandler CancelComplete
        {
            add
            {
                lock (this)
                {
                    m_cancelComplete = (CancelCompleteEventHandler)Delegate.Combine(m_cancelComplete, value);
                }
            }
            remove
            {
                lock (this)
                {
                    m_cancelComplete = (CancelCompleteEventHandler)Delegate.Remove(m_cancelComplete, value);
                }
            }
        }

        public Request(object requestHandle, Delegate callback, int requestID)
        {
            m_requestHandle = requestHandle;
            m_callback = callback;
            m_requestID = requestID;
        }

        public bool Update(int cancelID, ItemIdentifier[] results)
        {
            bool result;
            lock (this)
            {
                m_cancelID = cancelID;
                m_items = new Hashtable();
                foreach (ItemIdentifier itemIdentifier in results)
                {
                    if (!typeof(IResult).IsInstanceOfType(itemIdentifier) || ((IResult)itemIdentifier).ResultID.Succeeded())
                    {
                        m_items[itemIdentifier.ServerHandle] = new ItemIdentifier(itemIdentifier);
                    }
                }

                if (m_items.Count == 0)
                {
                    result = true;
                }
                else
                {
                    bool flag = false;
                    if (m_results != null)
                    {
                        foreach (object results2 in m_results)
                        {
                            flag = InvokeCallback(results2);
                        }
                    }

                    result = flag;
                }
            }

            return result;
        }

        public bool InvokeCallback(object results)
        {
            bool result;
            lock (this)
            {
                if (m_items == null)
                {
                    if (m_results == null)
                    {
                        m_results = new ArrayList();
                    }

                    m_results.Add(results);
                    result = false;
                }
                else if (typeof(DataUpdateEventHandler).IsInstanceOfType(m_callback))
                {
                    result = InvokeCallback((DataUpdateEventHandler)m_callback, results);
                }
                else if (typeof(ReadValuesEventHandler).IsInstanceOfType(m_callback))
                {
                    result = InvokeCallback((ReadValuesEventHandler)m_callback, results);
                }
                else if (typeof(ReadAttributesEventHandler).IsInstanceOfType(m_callback))
                {
                    result = InvokeCallback((ReadAttributesEventHandler)m_callback, results);
                }
                else if (typeof(ReadAnnotationsEventHandler).IsInstanceOfType(m_callback))
                {
                    result = InvokeCallback((ReadAnnotationsEventHandler)m_callback, results);
                }
                else if (typeof(UpdateCompleteEventHandler).IsInstanceOfType(m_callback))
                {
                    result = InvokeCallback((UpdateCompleteEventHandler)m_callback, results);
                }
                else
                {
                    result = true;
                }
            }

            return result;
        }

        public void OnCancelComplete()
        {
            lock (this)
            {
                if (m_cancelComplete != null)
                {
                    m_cancelComplete(this);
                }
            }
        }

        public object Handle => m_requestHandle;

        public DateTime StartTime
        {
            get => m_startTime;
            set => m_startTime = value;
        }

        public DateTime EndTime
        {
            get => m_endTime;
            set => m_endTime = value;
        }

        private bool InvokeCallback(DataUpdateEventHandler callback, object results)
        {
            if (!typeof(ItemValueCollection[]).IsInstanceOfType(results))
            {
                return false;
            }

            ItemValueCollection[] results2 = (ItemValueCollection[])results;
            UpdateResults(results2);
            try
            {
                callback(this, results2);
            }
            catch
            {
            }

            return false;
        }

        private bool InvokeCallback(ReadValuesEventHandler callback, object results)
        {
            if (!typeof(ItemValueCollection[]).IsInstanceOfType(results))
            {
                return false;
            }

            ItemValueCollection[] array = (ItemValueCollection[])results;
            UpdateResults(array);
            try
            {
                callback(this, array);
            }
            catch
            {
            }

            foreach (ItemValueCollection itemValueCollection in array)
            {
                if (itemValueCollection.ResultID == ResultID.Hda.S_MOREDATA)
                {
                    return false;
                }
            }

            return true;
        }

        private bool InvokeCallback(ReadAttributesEventHandler callback, object results)
        {
            if (!typeof(ItemAttributeCollection).IsInstanceOfType(results))
            {
                return false;
            }

            ItemAttributeCollection itemAttributeCollection = (ItemAttributeCollection)results;
            UpdateResults(new ItemAttributeCollection[]
            {
                itemAttributeCollection
            });
            try
            {
                callback(this, itemAttributeCollection);
            }
            catch
            {
            }

            return true;
        }

        private bool InvokeCallback(ReadAnnotationsEventHandler callback, object results)
        {
            if (!typeof(AnnotationValueCollection[]).IsInstanceOfType(results))
            {
                return false;
            }

            AnnotationValueCollection[] results2 = (AnnotationValueCollection[])results;
            UpdateResults(results2);
            try
            {
                callback(this, results2);
            }
            catch
            {
            }

            return true;
        }

        private bool InvokeCallback(UpdateCompleteEventHandler callback, object results)
        {
            if (!typeof(ResultCollection[]).IsInstanceOfType(results))
            {
                return false;
            }

            ResultCollection[] results2 = (ResultCollection[])results;
            UpdateResults(results2);
            try
            {
                callback(this, results2);
            }
            catch
            {
            }

            return true;
        }

        private void UpdateResults(ItemIdentifier[] results)
        {
            foreach (ItemIdentifier itemIdentifier in results)
            {
                if (typeof(IActualTime).IsInstanceOfType(itemIdentifier))
                {
                    ((IActualTime)itemIdentifier).StartTime = StartTime;
                    ((IActualTime)itemIdentifier).EndTime = EndTime;
                }

                ItemIdentifier itemIdentifier2 = (ItemIdentifier)m_items[itemIdentifier.ServerHandle];
                if (itemIdentifier2 != null)
                {
                    itemIdentifier.ItemName = itemIdentifier2.ItemName;
                    itemIdentifier.ItemPath = itemIdentifier2.ItemPath;
                    itemIdentifier.ServerHandle = itemIdentifier2.ServerHandle;
                    itemIdentifier.ClientHandle = itemIdentifier2.ClientHandle;
                }
            }
        }

        private event CancelCompleteEventHandler m_cancelComplete;

        private object m_requestHandle;

        private Delegate m_callback;

        private int m_requestID;

        private int m_cancelID;

        private DateTime m_startTime = DateTime.MinValue;

        private DateTime m_endTime = DateTime.MinValue;

        private Hashtable m_items;

        private ArrayList m_results;
    }
}