using System;

namespace Opc.Ae
{
    [Serializable]
    public class ServerStatus : ICloneable
    {
        public string VendorInfo
        {
            get => m_vendorInfo;
            set => m_vendorInfo = value;
        }

        public string ProductVersion
        {
            get => m_productVersion;
            set => m_productVersion = value;
        }

        public ServerState ServerState
        {
            get => m_serverState;
            set => m_serverState = value;
        }

        public string StatusInfo
        {
            get => m_statusInfo;
            set => m_statusInfo = value;
        }

        public DateTime StartTime
        {
            get => m_startTime;
            set => m_startTime = value;
        }

        public DateTime CurrentTime
        {
            get => m_currentTime;
            set => m_currentTime = value;
        }

        public DateTime LastUpdateTime
        {
            get => m_lastUpdateTime;
            set => m_lastUpdateTime = value;
        }

        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        private string m_vendorInfo;

        private string m_productVersion;

        private ServerState m_serverState;

        private string m_statusInfo;

        private DateTime m_startTime = DateTime.MinValue;

        private DateTime m_currentTime = DateTime.MinValue;

        private DateTime m_lastUpdateTime = DateTime.MinValue;
    }
}