using System;

namespace Opc.Ae
{
    [Serializable]
    public class EventAcknowledgement : ICloneable
    {
        public string SourceName
        {
            get => m_sourceName;
            set => m_sourceName = value;
        }

        public string ConditionName
        {
            get => m_conditionName;
            set => m_conditionName = value;
        }

        public DateTime ActiveTime
        {
            get => m_activeTime;
            set => m_activeTime = value;
        }

        public int Cookie
        {
            get => m_cookie;
            set => m_cookie = value;
        }

        public EventAcknowledgement()
        {
        }

        public EventAcknowledgement(EventNotification notification)
        {
            m_sourceName = notification.SourceID;
            m_conditionName = notification.ConditionName;
            m_activeTime = notification.ActiveTime;
            m_cookie = notification.Cookie;
        }

        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        private string m_sourceName;

        private string m_conditionName;

        private DateTime m_activeTime = DateTime.MinValue;

        private int m_cookie;
    }
}