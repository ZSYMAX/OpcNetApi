using System;

namespace Opc.Dx
{
    [Serializable]
    public class DXConnection : ItemIdentifier
    {
        public string Name
        {
            get => m_name;
            set => m_name = value;
        }

        public BrowsePathCollection BrowsePaths => m_browsePaths;

        public string Description
        {
            get => m_description;
            set => m_description = value;
        }

        public string Keyword
        {
            get => m_keyword;
            set => m_keyword = value;
        }

        public bool DefaultSourceItemConnected
        {
            get => m_defaultSourceItemConnected;
            set => m_defaultSourceItemConnected = value;
        }

        public bool DefaultSourceItemConnectedSpecified
        {
            get => m_defaultSourceItemConnectedSpecified;
            set => m_defaultSourceItemConnectedSpecified = value;
        }

        public bool DefaultTargetItemConnected
        {
            get => m_defaultTargetItemConnected;
            set => m_defaultTargetItemConnected = value;
        }

        public bool DefaultTargetItemConnectedSpecified
        {
            get => m_defaultTargetItemConnectedSpecified;
            set => m_defaultTargetItemConnectedSpecified = value;
        }

        public bool DefaultOverridden
        {
            get => m_defaultOverridden;
            set => m_defaultOverridden = value;
        }

        public bool DefaultOverriddenSpecified
        {
            get => m_defaultOverriddenSpecified;
            set => m_defaultOverriddenSpecified = value;
        }

        public object DefaultOverrideValue
        {
            get => m_defaultOverrideValue;
            set => m_defaultOverrideValue = value;
        }

        public bool EnableSubstituteValue
        {
            get => m_enableSubstituteValue;
            set => m_enableSubstituteValue = value;
        }

        public bool EnableSubstituteValueSpecified
        {
            get => m_enableSubstituteValueSpecified;
            set => m_enableSubstituteValueSpecified = value;
        }

        public object SubstituteValue
        {
            get => m_substituteValue;
            set => m_substituteValue = value;
        }

        public string TargetItemName
        {
            get => m_targetItemName;
            set => m_targetItemName = value;
        }

        public string TargetItemPath
        {
            get => m_targetItemPath;
            set => m_targetItemPath = value;
        }

        public string SourceServerName
        {
            get => m_sourceServerName;
            set => m_sourceServerName = value;
        }

        public string SourceItemName
        {
            get => m_sourceItemName;
            set => m_sourceItemName = value;
        }

        public string SourceItemPath
        {
            get => m_sourceItemPath;
            set => m_sourceItemPath = value;
        }

        public int SourceItemQueueSize
        {
            get => m_sourceItemQueueSize;
            set => m_sourceItemQueueSize = value;
        }

        public bool SourceItemQueueSizeSpecified
        {
            get => m_sourceItemQueueSizeSpecified;
            set => m_sourceItemQueueSizeSpecified = value;
        }

        public int UpdateRate
        {
            get => m_updateRate;
            set => m_updateRate = value;
        }

        public bool UpdateRateSpecified
        {
            get => m_updateRateSpecified;
            set => m_updateRateSpecified = value;
        }

        public float Deadband
        {
            get => m_deadband;
            set => m_deadband = value;
        }

        public bool DeadbandSpecified
        {
            get => m_deadbandSpecified;
            set => m_deadbandSpecified = value;
        }

        public string VendorData
        {
            get => m_vendorData;
            set => m_vendorData = value;
        }

        public DXConnection()
        {
        }

        public DXConnection(ItemIdentifier item) : base(item)
        {
        }

        public DXConnection(DXConnection connection) : base(connection)
        {
            if (connection != null)
            {
                BrowsePaths.AddRange(connection.BrowsePaths);
                Name = connection.Name;
                Description = connection.Description;
                Keyword = connection.Keyword;
                DefaultSourceItemConnected = connection.DefaultSourceItemConnected;
                DefaultSourceItemConnectedSpecified = connection.DefaultSourceItemConnectedSpecified;
                DefaultTargetItemConnected = connection.DefaultTargetItemConnected;
                DefaultTargetItemConnectedSpecified = connection.DefaultTargetItemConnectedSpecified;
                DefaultOverridden = connection.DefaultOverridden;
                DefaultOverriddenSpecified = connection.DefaultOverriddenSpecified;
                DefaultOverrideValue = connection.DefaultOverrideValue;
                EnableSubstituteValue = connection.EnableSubstituteValue;
                EnableSubstituteValueSpecified = connection.EnableSubstituteValueSpecified;
                SubstituteValue = connection.SubstituteValue;
                TargetItemName = connection.TargetItemName;
                TargetItemPath = connection.TargetItemPath;
                SourceServerName = connection.SourceServerName;
                SourceItemName = connection.SourceItemName;
                SourceItemPath = connection.SourceItemPath;
                SourceItemQueueSize = connection.SourceItemQueueSize;
                SourceItemQueueSizeSpecified = connection.SourceItemQueueSizeSpecified;
                UpdateRate = connection.UpdateRate;
                UpdateRateSpecified = connection.UpdateRateSpecified;
                Deadband = connection.Deadband;
                DeadbandSpecified = connection.DeadbandSpecified;
                VendorData = connection.VendorData;
            }
        }

        public override object Clone()
        {
            return new DXConnection(this);
        }

        private string m_name;

        private BrowsePathCollection m_browsePaths = new BrowsePathCollection();

        private string m_description;

        private string m_keyword;

        private bool m_defaultSourceItemConnected;

        private bool m_defaultSourceItemConnectedSpecified;

        private bool m_defaultTargetItemConnected;

        private bool m_defaultTargetItemConnectedSpecified;

        private bool m_defaultOverridden;

        private bool m_defaultOverriddenSpecified;

        private object m_defaultOverrideValue;

        private bool m_enableSubstituteValue;

        private bool m_enableSubstituteValueSpecified;

        private object m_substituteValue;

        private string m_targetItemName;

        private string m_targetItemPath;

        private string m_sourceServerName;

        private string m_sourceItemName;

        private string m_sourceItemPath;

        private int m_sourceItemQueueSize = 1;

        private bool m_sourceItemQueueSizeSpecified;

        private int m_updateRate;

        private bool m_updateRateSpecified;

        private float m_deadband;

        private bool m_deadbandSpecified;

        private string m_vendorData;
    }
}