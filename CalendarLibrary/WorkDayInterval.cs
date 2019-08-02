using System;

namespace CalendarLibrary
{
    public class WorkDayInterval
    {
        public TimeSpan Start { get; }
        public TimeSpan End { get; }
        public TimeSpan Duration
        {
            get { return End - Start; }
        }
        public WorkDayInterval(TimeSpan start, TimeSpan end)
        {
            Start = start;
            End = end;
        }
        public WorkDayInterval(TimeSpan start, int minutesDuration) : this(start, start + new TimeSpan(0, minutesDuration, 0)) { }
    }
}
