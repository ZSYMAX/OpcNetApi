using System;

namespace Opc.Da
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

        public object ServerHandle
        {
            get => m_serverHandle;
            set => m_serverHandle = value;
        }

        public string Locale
        {
            get => m_locale;
            set => m_locale = value;
        }

        public bool Active
        {
            get => m_active;
            set => m_active = value;
        }

        public int UpdateRate
        {
            get => m_updateRate;
            set => m_updateRate = value;
        }

        public int KeepAlive
        {
            get => m_keepAlive;
            set => m_keepAlive = value;
        }

        public float Deadband
        {
            get => m_deadband;
            set => m_deadband = value;
        }

        public virtual object Clone()
        {
            return MemberwiseClone();
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