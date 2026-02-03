using System;

namespace Opc.Dx
{
    [Serializable]
    public class SourceServer : ItemIdentifier
    {
        public string Name
        {
            get => m_name;
            set => m_name = value;
        }

        public string Description
        {
            get => m_description;
            set => m_description = value;
        }

        public string ServerType
        {
            get => m_serverType;
            set => m_serverType = value;
        }

        public string ServerURL
        {
            get => m_serverURL;
            set => m_serverURL = value;
        }

        public bool DefaultConnected
        {
            get => m_defaultConnected;
            set => m_defaultConnected = value;
        }

        public bool DefaultConnectedSpecified
        {
            get => m_defaultConnectedSpecified;
            set => m_defaultConnectedSpecified = value;
        }

        public SourceServer()
        {
        }

        public SourceServer(ItemIdentifier item) : base(item)
        {
        }

        public SourceServer(SourceServer server) : base(server)
        {
            if (server != null)
            {
                m_name = server.m_name;
                m_description = server.m_description;
                m_serverType = server.m_serverType;
                m_serverURL = server.m_serverURL;
                m_defaultConnected = server.m_defaultConnected;
                m_defaultConnectedSpecified = server.m_defaultConnectedSpecified;
            }
        }

        private string m_name;

        private string m_description;

        private string m_serverType;

        private string m_serverURL;

        private bool m_defaultConnected;

        private bool m_defaultConnectedSpecified;
    }
}