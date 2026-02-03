using System;

namespace Opc.Ae
{
    [Serializable]
    public class SubCondition : ICloneable
    {
        public string Name
        {
            get { return this.m_name; }
            set { this.m_name = value; }
        }

        public string Definition
        {
            get { return this.m_definition; }
            set { this.m_definition = value; }
        }

        public int Severity
        {
            get { return this.m_severity; }
            set { this.m_severity = value; }
        }

        public string Description
        {
            get { return this.m_description; }
            set { this.m_description = value; }
        }

        public override string ToString()
        {
            return this.Name;
        }

        public virtual object Clone()
        {
            return base.MemberwiseClone();
        }

        private string m_name;

        private string m_definition;

        private int m_severity = 1;

        private string m_description;
    }
}