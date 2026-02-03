using System;

namespace Opc
{
    [Serializable]
    public struct Specification
    {
        public string ID
        {
            get { return this.m_id; }
            set { this.m_id = value; }
        }

        public string Description
        {
            get { return this.m_description; }
            set { this.m_description = value; }
        }

        public static bool operator ==(Specification a, Specification b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Specification a, Specification b)
        {
            return !a.Equals(b);
        }

        public Specification(string id, string description)
        {
            this.m_id = id;
            this.m_description = description;
        }

        public override bool Equals(object target)
        {
            return target != null && target.GetType() == typeof(Specification) && this.ID == ((Specification)target).ID;
        }

        public override string ToString()
        {
            return this.Description;
        }

        public override int GetHashCode()
        {
            if (this.ID == null)
            {
                return base.GetHashCode();
            }

            return this.ID.GetHashCode();
        }

        private string m_id;

        private string m_description;

        public static readonly Specification COM_AE_10 = new Specification("58E13251-AC87-11d1-84D5-00608CB8A7E9", "Alarms and Event 1.XX");

        public static readonly Specification COM_BATCH_10 = new Specification("A8080DA0-E23E-11D2-AFA7-00C04F539421", "Batch 1.00");

        public static readonly Specification COM_BATCH_20 = new Specification("843DE67B-B0C9-11d4-A0B7-000102A980B1", "Batch 2.00");

        public static readonly Specification COM_DA_10 = new Specification("63D5F430-CFE4-11d1-B2C8-0060083BA1FB", "Data Access 1.0a");

        public static readonly Specification COM_DA_20 = new Specification("63D5F432-CFE4-11d1-B2C8-0060083BA1FB", "Data Access 2.XX");

        public static readonly Specification COM_DA_30 = new Specification("CC603642-66D7-48f1-B69A-B625E73652D7", "Data Access 3.00");

        public static readonly Specification COM_DX_10 = new Specification("A0C85BB8-4161-4fd6-8655-BB584601C9E0", "Data eXchange 1.00");

        public static readonly Specification COM_HDA_10 = new Specification("7DE5B060-E089-11d2-A5E6-000086339399", "Historical Data Access 1.XX");

        public static readonly Specification XML_DA_10 = new Specification("3098EDA4-A006-48b2-A27F-247453959408", "XML Data Access 1.00");

        public static readonly Specification UA10 = new Specification("EC10AFD8-9BC0-4828-B47E-B3D907F929B1", "Unified Architecture 1.00");
    }
}