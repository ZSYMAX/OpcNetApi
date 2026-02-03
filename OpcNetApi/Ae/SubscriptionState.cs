using System;

namespace Opc.Ae
{
    [Serializable]
    public class SubscriptionState : ICloneable
    {
        public string Name
        {
            get { return this.m_name; }
            set { this.m_name = value; }
        }

        public object ClientHandle
        {
            get { return this.m_clientHandle; }
            set { this.m_clientHandle = value; }
        }

        public bool Active
        {
            get { return this.m_active; }
            set { this.m_active = value; }
        }

        public int BufferTime
        {
            get { return this.m_bufferTime; }
            set { this.m_bufferTime = value; }
        }

        public int MaxSize
        {
            get { return this.m_maxSize; }
            set { this.m_maxSize = value; }
        }

        public int KeepAlive
        {
            get { return this.m_keepAlive; }
            set { this.m_keepAlive = value; }
        }

        public virtual object Clone()
        {
            return base.MemberwiseClone();
        }

        private string m_name;

        private object m_clientHandle;

        private bool m_active = true;

        private int m_bufferTime;

        private int m_maxSize;

        private int m_keepAlive;
    }
}