using System;
using System.Collections;
using Opc;
using Opc.Hda;

namespace OpcCom.Hda
{
    internal class Request : IRequest, IActualTime
    {
        public int RequestID
        {
            get { return this.m_requestID; }
        }

        public int CancelID
        {
            get { return this.m_cancelID; }
        }

        public event CancelCompleteEventHandler CancelComplete
        {
            add
            {
                lock (this)
                {
                    this.m_cancelComplete = (CancelCompleteEventHandler)Delegate.Combine(this.m_cancelComplete, value);
                }
            }
            remove
            {
                lock (this)
                {
                    this.m_cancelComplete = (CancelCompleteEventHandler)Delegate.Remove(this.m_cancelComplete, value);
                }
            }
        }

        public Request(object requestHandle, Delegate callback, int requestID)
        {
            this.m_requestHandle = requestHandle;
            this.m_callback = callback;
            this.m_requestID = requestID;
        }

        public bool Update(int cancelID, ItemIdentifier[] results)
        {
            bool result;
            lock (this)
            {
                this.m_cancelID = cancelID;
                this.m_items = new Hashtable();
                foreach (ItemIdentifier itemIdentifier in results)
                {
                    if (!typeof(IResult).IsInstanceOfType(itemIdentifier) || ((IResult)itemIdentifier).ResultID.Succeeded())
                    {
                        this.m_items[itemIdentifier.ServerHandle] = new ItemIdentifier(itemIdentifier);
                    }
                }

                if (this.m_items.Count == 0)
                {
                    result = true;
                }
                else
                {
                    bool flag = false;
                    if (this.m_results != null)
                    {
                        foreach (object results2 in this.m_results)
                        {
                            flag = this.InvokeCallback(results2);
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
                if (this.m_items == null)
                {
                    if (this.m_results == null)
                    {
                        this.m_results = new ArrayList();
                    }

                    this.m_results.Add(results);
                    result = false;
                }
                else if (typeof(DataUpdateEventHandler).IsInstanceOfType(this.m_callback))
                {
                    result = this.InvokeCallback((DataUpdateEventHandler)this.m_callback, results);
                }
                else if (typeof(ReadValuesEventHandler).IsInstanceOfType(this.m_callback))
                {
                    result = this.InvokeCallback((ReadValuesEventHandler)this.m_callback, results);
                }
                else if (typeof(ReadAttributesEventHandler).IsInstanceOfType(this.m_callback))
                {
                    result = this.InvokeCallback((ReadAttributesEventHandler)this.m_callback, results);
                }
                else if (typeof(ReadAnnotationsEventHandler).IsInstanceOfType(this.m_callback))
                {
                    result = this.InvokeCallback((ReadAnnotationsEventHandler)this.m_callback, results);
                }
                else if (typeof(UpdateCompleteEventHandler).IsInstanceOfType(this.m_callback))
                {
                    result = this.InvokeCallback((UpdateCompleteEventHandler)this.m_callback, results);
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
                if (this.m_cancelComplete != null)
                {
                    this.m_cancelComplete(this);
                }
            }
        }

        public object Handle
        {
            get { return this.m_requestHandle; }
        }

        public DateTime StartTime
        {
            get { return this.m_startTime; }
            set { this.m_startTime = value; }
        }

        public DateTime EndTime
        {
            get { return this.m_endTime; }
            set { this.m_endTime = value; }
        }

        private bool InvokeCallback(DataUpdateEventHandler callback, object results)
        {
            if (!typeof(ItemValueCollection[]).IsInstanceOfType(results))
            {
                return false;
            }

            ItemValueCollection[] results2 = (ItemValueCollection[])results;
            this.UpdateResults(results2);
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
            this.UpdateResults(array);
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
            this.UpdateResults(new ItemAttributeCollection[]
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
            this.UpdateResults(results2);
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
            this.UpdateResults(results2);
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
                    ((IActualTime)itemIdentifier).StartTime = this.StartTime;
                    ((IActualTime)itemIdentifier).EndTime = this.EndTime;
                }

                ItemIdentifier itemIdentifier2 = (ItemIdentifier)this.m_items[itemIdentifier.ServerHandle];
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