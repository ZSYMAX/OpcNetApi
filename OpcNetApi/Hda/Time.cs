using System;
using System.Text;

namespace Opc.Hda
{
    [Serializable]
    public class Time
    {
        public Time()
        {
        }

        public Time(DateTime time)
        {
            AbsoluteTime = time;
        }

        public Time(string time)
        {
            Time time2 = Parse(time);
            m_absoluteTime = DateTime.MinValue;
            m_baseTime = time2.m_baseTime;
            m_offsets = time2.m_offsets;
        }

        public bool IsRelative
        {
            get => m_absoluteTime == DateTime.MinValue;
            set => m_absoluteTime = DateTime.MinValue;
        }

        public DateTime AbsoluteTime
        {
            get => m_absoluteTime;
            set => m_absoluteTime = value;
        }

        public RelativeTime BaseTime
        {
            get => m_baseTime;
            set => m_baseTime = value;
        }

        public TimeOffsetCollection Offsets => m_offsets;

        public DateTime ResolveTime()
        {
            if (!IsRelative)
            {
                return m_absoluteTime;
            }

            DateTime result = DateTime.UtcNow;
            int year = result.Year;
            int month = result.Month;
            int day = result.Day;
            int hour = result.Hour;
            int minute = result.Minute;
            int second = result.Second;
            int millisecond = result.Millisecond;
            switch (BaseTime)
            {
                case RelativeTime.Second:
                    millisecond = 0;
                    break;
                case RelativeTime.Minute:
                    second = 0;
                    millisecond = 0;
                    break;
                case RelativeTime.Hour:
                    minute = 0;
                    second = 0;
                    millisecond = 0;
                    break;
                case RelativeTime.Day:
                case RelativeTime.Week:
                    hour = 0;
                    minute = 0;
                    second = 0;
                    millisecond = 0;
                    break;
                case RelativeTime.Month:
                    day = 0;
                    hour = 0;
                    minute = 0;
                    second = 0;
                    millisecond = 0;
                    break;
                case RelativeTime.Year:
                    month = 0;
                    day = 0;
                    hour = 0;
                    minute = 0;
                    second = 0;
                    millisecond = 0;
                    break;
            }

            result = new DateTime(year, month, day, hour, minute, second, millisecond);
            if (BaseTime == RelativeTime.Week && result.DayOfWeek != DayOfWeek.Sunday)
            {
                result = result.AddDays((double)(-(double)result.DayOfWeek));
            }

            foreach (object obj in Offsets)
            {
                TimeOffset timeOffset = (TimeOffset)obj;
                switch (timeOffset.Type)
                {
                    case RelativeTime.Second:
                        result = result.AddSeconds((double)timeOffset.Value);
                        break;
                    case RelativeTime.Minute:
                        result = result.AddMinutes((double)timeOffset.Value);
                        break;
                    case RelativeTime.Hour:
                        result = result.AddHours((double)timeOffset.Value);
                        break;
                    case RelativeTime.Day:
                        result = result.AddDays((double)timeOffset.Value);
                        break;
                    case RelativeTime.Week:
                        result = result.AddDays((double)(timeOffset.Value * 7));
                        break;
                    case RelativeTime.Month:
                        result = result.AddMonths(timeOffset.Value);
                        break;
                    case RelativeTime.Year:
                        result = result.AddYears(timeOffset.Value);
                        break;
                }
            }

            return result;
        }

        public override string ToString()
        {
            if (!IsRelative)
            {
                return Convert.ToString(m_absoluteTime);
            }

            StringBuilder stringBuilder = new StringBuilder(256);
            stringBuilder.Append(BaseTypeToString(BaseTime));
            stringBuilder.Append(Offsets.ToString());
            return stringBuilder.ToString();
        }

        public static Time Parse(string buffer)
        {
            buffer = buffer.Trim();
            Time time = new Time();
            bool flag = false;
            foreach (object obj in Enum.GetValues(typeof(RelativeTime)))
            {
                RelativeTime baseTime = (RelativeTime)obj;
                string text = BaseTypeToString(baseTime);
                if (buffer.StartsWith(text))
                {
                    buffer = buffer.Substring(text.Length).Trim();
                    time.BaseTime = baseTime;
                    flag = true;
                    break;
                }
            }

            if (!flag)
            {
                time.AbsoluteTime = System.Convert.ToDateTime(buffer).ToUniversalTime();
                return time;
            }

            if (buffer.Length > 0)
            {
                time.Offsets.Parse(buffer);
            }

            return time;
        }

        private static string BaseTypeToString(RelativeTime baseTime)
        {
            switch (baseTime)
            {
                case RelativeTime.Now:
                    return "NOW";
                case RelativeTime.Second:
                    return "SECOND";
                case RelativeTime.Minute:
                    return "MINUTE";
                case RelativeTime.Hour:
                    return "HOUR";
                case RelativeTime.Day:
                    return "DAY";
                case RelativeTime.Week:
                    return "WEEK";
                case RelativeTime.Month:
                    return "MONTH";
                case RelativeTime.Year:
                    return "YEAR";
                default:
                    throw new ArgumentOutOfRangeException("baseTime", baseTime.ToString(), "Invalid value for relative base time.");
            }
        }

        private DateTime m_absoluteTime = DateTime.MinValue;

        private RelativeTime m_baseTime;

        private TimeOffsetCollection m_offsets = new TimeOffsetCollection();
    }
}