using System;
using Opc.Da;

namespace Opc.Ae
{
    [Serializable]
    public class Condition : ICloneable
    {
        public int State
        {
            get { return this.m_state; }
            set { this.m_state = value; }
        }

        public SubCondition ActiveSubCondition
        {
            get { return this.m_activeSubcondition; }
            set { this.m_activeSubcondition = value; }
        }

        public Quality Quality
        {
            get { return this.m_quality; }
            set { this.m_quality = value; }
        }

        public DateTime LastAckTime
        {
            get { return this.m_lastAckTime; }
            set { this.m_lastAckTime = value; }
        }

        public DateTime SubCondLastActive
        {
            get { return this.m_subCondLastActive; }
            set { this.m_subCondLastActive = value; }
        }

        public DateTime CondLastActive
        {
            get { return this.m_condLastActive; }
            set { this.m_condLastActive = value; }
        }

        public DateTime CondLastInactive
        {
            get { return this.m_condLastInactive; }
            set { this.m_condLastInactive = value; }
        }

        public string AcknowledgerID
        {
            get { return this.m_acknowledgerID; }
            set { this.m_acknowledgerID = value; }
        }

        public string Comment
        {
            get { return this.m_comment; }
            set { this.m_comment = value; }
        }

        public Condition.SubConditionCollection SubConditions
        {
            get { return this.m_subconditions; }
        }

        public Condition.AttributeValueCollection Attributes
        {
            get { return this.m_attributes; }
        }

        public virtual object Clone()
        {
            Condition condition = (Condition)base.MemberwiseClone();
            condition.m_activeSubcondition = (SubCondition)this.m_activeSubcondition.Clone();
            condition.m_subconditions = (Condition.SubConditionCollection)this.m_subconditions.Clone();
            condition.m_attributes = (Condition.AttributeValueCollection)this.m_attributes.Clone();
            return condition;
        }

        private int m_state;

        private SubCondition m_activeSubcondition = new SubCondition();

        private Quality m_quality = Quality.Bad;

        private DateTime m_lastAckTime = DateTime.MinValue;

        private DateTime m_subCondLastActive = DateTime.MinValue;

        private DateTime m_condLastActive = DateTime.MinValue;

        private DateTime m_condLastInactive = DateTime.MinValue;

        private string m_acknowledgerID;

        private string m_comment;

        private Condition.SubConditionCollection m_subconditions = new Condition.SubConditionCollection();

        private Condition.AttributeValueCollection m_attributes = new Condition.AttributeValueCollection();

        public class AttributeValueCollection : WriteableCollection
        {
            public AttributeValue this[int index]
            {
                get { return (AttributeValue)this.Array[index]; }
            }

            public new AttributeValue[] ToArray()
            {
                return (AttributeValue[])this.Array.ToArray();
            }

            internal AttributeValueCollection() : base(null, typeof(AttributeValue))
            {
            }
        }

        public class SubConditionCollection : WriteableCollection
        {
            public SubCondition this[int index]
            {
                get { return (SubCondition)this.Array[index]; }
            }

            public new SubCondition[] ToArray()
            {
                return (SubCondition[])this.Array.ToArray();
            }

            internal SubConditionCollection() : base(null, typeof(SubCondition))
            {
            }
        }
    }
}