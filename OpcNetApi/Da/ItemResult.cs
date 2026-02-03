using System;

namespace Opc.Da
{
    [Serializable]
    public class ItemResult : Item, IResult
    {
        public ItemResult()
        {
        }

        public ItemResult(ItemIdentifier item) : base(item)
        {
        }

        public ItemResult(ItemIdentifier item, ResultID resultID) : base(item)
        {
            this.ResultID = this.ResultID;
        }

        public ItemResult(Item item) : base(item)
        {
        }

        public ItemResult(Item item, ResultID resultID) : base(item)
        {
            this.ResultID = resultID;
        }

        public ItemResult(ItemResult item) : base(item)
        {
            if (item != null)
            {
                this.ResultID = item.ResultID;
                this.DiagnosticInfo = item.DiagnosticInfo;
            }
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