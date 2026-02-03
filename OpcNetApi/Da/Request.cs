using System;

namespace Opc.Da
{
    [Serializable]
    public class Request : IRequest
    {
        public ISubscription Subscription => m_subscription;

        public object Handle => m_handle;

        public void Cancel(CancelCompleteEventHandler callback)
        {
            m_subscription.Cancel(this, callback);
        }

        public Request(ISubscription subscription, object handle)
        {
            m_subscription = subscription;
            m_handle = handle;
        }

        private ISubscription m_subscription;

        private object m_handle;
    }
}