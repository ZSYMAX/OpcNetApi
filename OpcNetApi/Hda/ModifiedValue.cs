using System;

namespace Opc.Hda
{
    [Serializable]
    public class ModifiedValue : ItemValue
    {
        public DateTime ModificationTime
        {
            get => m_modificationTime;
            set => m_modificationTime = value;
        }

        public EditType EditType
        {
            get => m_editType;
            set => m_editType = value;
        }

        public string User
        {
            get => m_user;
            set => m_user = value;
        }

        private DateTime m_modificationTime = DateTime.MinValue;

        private EditType m_editType = EditType.Insert;

        private string m_user;
    }
}