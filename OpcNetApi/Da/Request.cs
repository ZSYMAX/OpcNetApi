using System;

namespace Opc.Da
{
    [Serializable]
    public class Request : IRequest
    {
        public ISubscription Subscription
        {
            get { return this.m_subscription; }
        }

        public object Handle
        {
            get { return this.m_handle; }
        }

        public void Cancel(CancelCompleteEventHandler callback)
        {
            this.m_subscription.Cancel(this, callback);
        }

        public Request(ISubscription subscription, object handle)
        {
            this.m_subscription = subscription;
            this.m_handle = handle;
        }

        private ISubscription m_subscription;

        private object m_handle;
    }
}