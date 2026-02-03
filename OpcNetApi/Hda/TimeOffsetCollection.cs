using System;
using System.Collections;
using System.Text;

namespace Opc.Hda
{
    [Serializable]
    public class TimeOffsetCollection : ArrayList
    {
        public TimeOffset this[int index]
        {
            get => this[index];
            set => this[index] = value;
        }

        public int Add(int value, RelativeTime type)
        {
            return base.Add(new TimeOffset
            {
                Value = value,
                Type = type
            });
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder(256);
            foreach (object obj in ((IEnumerable)this))
            {
                TimeOffset timeOffset = (TimeOffset)obj;
                if (timeOffset.Value >= 0)
                {
                    stringBuilder.Append("+");
                }

                stringBuilder.AppendFormat("{0}", timeOffset.Value);
                stringBuilder.Append(TimeOffset.OffsetTypeToString(timeOffset.Type));
            }

            return stringBuilder.ToString();
        }

        public void Parse(string buffer)
        {
            Clear();
            bool positive = true;
            int num = 0;
            string text = "";
            int num2 = 0;
            for (int i = 0; i < buffer.Length; i++)
            {
                if (buffer[i] == '+' || buffer[i] == '-')
                {
                    if (num2 == 3)
                    {
                        Add(CreateOffset(positive, num, text));
                        num = 0;
                        text = "";
                        num2 = 0;
                    }

                    if (num2 != 0)
                    {
                        throw new FormatException("Unexpected token encountered while parsing relative time string.");
                    }

                    positive = (buffer[i] == '+');
                    num2 = 1;
                }
                else if (char.IsDigit(buffer, i))
                {
                    if (num2 == 3)
                    {
                        Add(CreateOffset(positive, num, text));
                        num = 0;
                        text = "";
                        num2 = 0;
                    }

                    if (num2 != 0 && num2 != 1 && num2 != 2)
                    {
                        throw new FormatException("Unexpected token encountered while parsing relative time string.");
                    }

                    num *= 10;
                    num += System.Convert.ToInt32((int)(buffer[i] - '0'));
                    num2 = 2;
                }
                else if (!char.IsWhiteSpace(buffer, i))
                {
                    if (num2 != 2 && num2 != 3)
                    {
                        throw new FormatException("Unexpected token encountered while parsing relative time string.");
                    }

                    text += buffer[i];
                    num2 = 3;
                }
            }

            if (num2 == 3)
            {
                Add(CreateOffset(positive, num, text));
                num2 = 0;
            }

            if (num2 != 0)
            {
                throw new FormatException("Unexpected end of string encountered while parsing relative time string.");
            }
        }

        public void CopyTo(TimeOffset[] array, int index)
        {
            CopyTo(array, index);
        }

        public void Insert(int index, TimeOffset value)
        {
            Insert(index, value);
        }

        public void Remove(TimeOffset value)
        {
            Remove(value);
        }

        public bool Contains(TimeOffset value)
        {
            return Contains(value);
        }

        public int IndexOf(TimeOffset value)
        {
            return IndexOf(value);
        }

        public int Add(TimeOffset value)
        {
            return Add(value);
        }

        private static TimeOffset CreateOffset(bool positive, int magnitude, string units)
        {
            foreach (object obj in Enum.GetValues(typeof(RelativeTime)))
            {
                RelativeTime relativeTime = (RelativeTime)obj;
                if (relativeTime != RelativeTime.Now && units == TimeOffset.OffsetTypeToString(relativeTime))
                {
                    return new TimeOffset
                    {
                        Value = (positive ? magnitude : (-magnitude)),
                        Type = relativeTime
                    };
                }
            }

            throw new ArgumentOutOfRangeException("units", units, "String is not a valid offset time type.");
        }
    }
}