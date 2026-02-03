using System;

namespace Opc.Ae
{
    [Serializable]
    public class Category : ICloneable
    {
        public int ID
        {
            get { return this.m_id; }
            set { this.m_id = value; }
        }

        public string Name
        {
            get { return this.m_name; }
            set { this.m_name = value; }
        }

        public override string ToString()
        {
            return this.Name;
        }

        public virtual object Clone()
        {
            return base.MemberwiseClone();
        }

        private int m_id;

        private string m_name;
    }
}