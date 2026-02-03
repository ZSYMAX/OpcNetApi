using System;

namespace Opc.Ae
{
    [Serializable]
    public class Category : ICloneable
    {
        public int ID
        {
            get => m_id;
            set => m_id = value;
        }

        public string Name
        {
            get => m_name;
            set => m_name = value;
        }

        public override string ToString()
        {
            return Name;
        }

        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        private int m_id;

        private string m_name;
    }
}