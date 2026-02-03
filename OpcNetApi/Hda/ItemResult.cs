using System;

namespace Opc.Hda
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

        public ItemResult(Item item) : base(item)
        {
        }

        public ItemResult(ItemResult item) : base(item)
        {
            if (item != null)
            {
                ResultID = item.ResultID;
                DiagnosticInfo = item.DiagnosticInfo;
            }
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