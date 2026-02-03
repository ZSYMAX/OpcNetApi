using System;

namespace Opc.Ae
{
    [Serializable]
    public class SubscriptionState : ICloneable
    {
        public string Name
        {
            get => m_name;
            set => m_name = value;
        }

        public object ClientHandle
        {
            get => m_clientHandle;
            set => m_clientHandle = value;
        }

        public bool Active
        {
            get => m_active;
            set => m_active = value;
        }

        public int BufferTime
        {
            get => m_bufferTime;
            set => m_bufferTime = value;
        }

        public int MaxSize
        {
            get => m_maxSize;
            set => m_maxSize = value;
        }

        public int KeepAlive
        {
            get => m_keepAlive;
            set => m_keepAlive = value;
        }

        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        private string m_name;

        private object m_clientHandle;

        private bool m_active = true;

        private int m_bufferTime;

        private int m_maxSize;

        private int m_keepAlive;
    }
}