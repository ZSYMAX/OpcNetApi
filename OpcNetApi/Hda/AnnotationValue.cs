using System;

namespace Opc.Hda
{
    [Serializable]
    public class AnnotationValue : ICloneable
    {
        public DateTime Timestamp
        {
            get { return this.m_timestamp; }
            set { this.m_timestamp = value; }
        }

        public string Annotation
        {
            get { return this.m_annotation; }
            set { this.m_annotation = value; }
        }

        public DateTime CreationTime
        {
            get { return this.m_creationTime; }
            set { this.m_creationTime = value; }
        }

        public string User
        {
            get { return this.m_user; }
            set { this.m_user = value; }
        }

        public virtual object Clone()
        {
            return base.MemberwiseClone();
        }

        private DateTime m_timestamp = DateTime.MinValue;

        private string m_annotation;

        private DateTime m_creationTime = DateTime.MinValue;

        private string m_user;
    }
}