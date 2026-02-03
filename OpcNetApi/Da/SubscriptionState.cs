using System;

namespace Opc.Da
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

        public object ServerHandle
        {
            get { return this.m_serverHandle; }
            set { this.m_serverHandle = value; }
        }

        public string Locale
        {
            get { return this.m_locale; }
            set { this.m_locale = value; }
        }

        public bool Active
        {
            get { return this.m_active; }
            set { this.m_active = value; }
        }

        public int UpdateRate
        {
            get { return this.m_updateRate; }
            set { this.m_updateRate = value; }
        }

        public int KeepAlive
        {
            get { return this.m_keepAlive; }
            set { this.m_keepAlive = value; }
        }

        public float Deadband
        {
            get { return this.m_deadband; }
            set { this.m_deadband = value; }
        }

        public virtual object Clone()
        {
            return base.MemberwiseClone();
        }

        private string m_name;

        private object m_clientHandle;

        private object m_serverHandle;

        private string m_locale;

        private bool m_active = true;

        private int m_updateRate;

        private int m_keepAlive;

        private float m_deadband;
    }
}