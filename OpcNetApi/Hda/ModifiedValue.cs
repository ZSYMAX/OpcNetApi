using System;

namespace Opc.Hda
{
    [Serializable]
    public class ModifiedValue : ItemValue
    {
        public DateTime ModificationTime
        {
            get { return this.m_modificationTime; }
            set { this.m_modificationTime = value; }
        }

        public EditType EditType
        {
            get { return this.m_editType; }
            set { this.m_editType = value; }
        }

        public string User
        {
            get { return this.m_user; }
            set { this.m_user = value; }
        }

        private DateTime m_modificationTime = DateTime.MinValue;

        private EditType m_editType = EditType.Insert;

        private string m_user;
    }
}