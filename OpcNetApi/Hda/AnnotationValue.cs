using System;

namespace Opc.Hda
{
    [Serializable]
    public class AnnotationValue : ICloneable
    {
        public DateTime Timestamp
        {
            get => m_timestamp;
            set => m_timestamp = value;
        }

        public string Annotation
        {
            get => m_annotation;
            set => m_annotation = value;
        }

        public DateTime CreationTime
        {
            get => m_creationTime;
            set => m_creationTime = value;
        }

        public string User
        {
            get => m_user;
            set => m_user = value;
        }

        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        private DateTime m_timestamp = DateTime.MinValue;

        private string m_annotation;

        private DateTime m_creationTime = DateTime.MinValue;

        private string m_user;
    }
}