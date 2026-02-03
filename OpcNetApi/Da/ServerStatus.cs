using System;

namespace Opc.Da
{
    [Serializable]
    public class ServerStatus : ICloneable
    {
        public string VendorInfo
        {
            get { return this.m_vendorInfo; }
            set { this.m_vendorInfo = value; }
        }

        public string ProductVersion
        {
            get { return this.m_productVersion; }
            set { this.m_productVersion = value; }
        }

        public serverState ServerState
        {
            get { return this.m_serverState; }
            set { this.m_serverState = value; }
        }

        public string StatusInfo
        {
            get { return this.m_statusInfo; }
            set { this.m_statusInfo = value; }
        }

        public DateTime StartTime
        {
            get { return this.m_startTime; }
            set { this.m_startTime = value; }
        }

        public DateTime CurrentTime
        {
            get { return this.m_currentTime; }
            set { this.m_currentTime = value; }
        }

        public DateTime LastUpdateTime
        {
            get { return this.m_lastUpdateTime; }
            set { this.m_lastUpdateTime = value; }
        }

        public virtual object Clone()
        {
            return base.MemberwiseClone();
        }

        private string m_vendorInfo;

        private string m_productVersion;

        private serverState m_serverState;

        private string m_statusInfo;

        private DateTime m_startTime = DateTime.MinValue;

        private DateTime m_currentTime = DateTime.MinValue;

        private DateTime m_lastUpdateTime = DateTime.MinValue;
    }
}