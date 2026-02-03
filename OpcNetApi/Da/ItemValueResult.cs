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
                ResultID = item.ResultID;
                DiagnosticInfo = item.DiagnosticInfo;
            }
        }

        public ItemValueResult(string itemName, ResultID resultID) : base(itemName)
        {
            ResultID = resultID;
        }

        public ItemValueResult(string itemName, ResultID resultID, string diagnosticInfo) : base(itemName)
        {
            ResultID = resultID;
            DiagnosticInfo = diagnosticInfo;
        }

        public ItemValueResult(ItemIdentifier item, ResultID resultID) : base(item)
        {
            ResultID = resultID;
        }

        public ItemValueResult(ItemIdentifier item, ResultID resultID, string diagnosticInfo) : base(item)
        {
            ResultID = resultID;
            DiagnosticInfo = diagnosticInfo;
        }

        public ResultID ResultID
        {
            get => m_resultID;
            set => m_resultID = value;
        }

        public string DiagnosticInfo
        {
            get => m_diagnosticInfo;
            set => m_diagnosticInfo = value;
        }

        private ResultID m_resultID = ResultID.S_OK;

        private string m_diagnosticInfo;
    }
}