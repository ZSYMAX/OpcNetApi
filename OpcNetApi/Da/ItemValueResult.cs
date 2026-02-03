using System;

namespace Opc.Da
{
    [Serializable]
    public class ItemValueResult : ItemValue, IResult
    {
        public ItemValueResult()
        {
        }

        public ItemValueResult(ItemIdentifier item) : base(item)
        {
        }

        public ItemValueResult(ItemValue item) : base(item)
        {
        }

        public ItemValueResult(ItemValueResult item) : base(item)
        {
            if (item != null)
            {
                this.ResultID = item.ResultID;
                this.DiagnosticInfo = item.DiagnosticInfo;
            }
        }

        public ItemValueResult(string itemName, ResultID resultID) : base(itemName)
        {
            this.ResultID = resultID;
        }

        public ItemValueResult(string itemName, ResultID resultID, string diagnosticInfo) : base(itemName)
        {
            this.ResultID = resultID;
            this.DiagnosticInfo = diagnosticInfo;
        }

        public ItemValueResult(ItemIdentifier item, ResultID resultID) : base(item)
        {
            this.ResultID = resultID;
        }

        public ItemValueResult(ItemIdentifier item, ResultID resultID, string diagnosticInfo) : base(item)
        {
            this.ResultID = resultID;
            this.DiagnosticInfo = diagnosticInfo;
        }

        public ResultID ResultID
        {
            get { return this.m_resultID; }
            set { this.m_resultID = value; }
        }

        public string DiagnosticInfo
        {
            get { return this.m_diagnosticInfo; }
            set { this.m_diagnosticInfo = value; }
        }

        private ResultID m_resultID = ResultID.S_OK;

        private string m_diagnosticInfo;
    }
}