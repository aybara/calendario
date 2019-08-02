using System;

namespace CalendarLibrary
{
    public class WorkDayBreak
    {
        public TimeSpan Start { get; }
        public TimeSpan End { get; }
        public TimeSpan Duration
        {
            get { return End - Start; }
        }
        public WorkDayBreak(TimeSpan start, TimeSpan end)
        {
            Start = start;
            End = end;
        }
        public WorkDayBreak(TimeSpan start, int minutesDuration) : this(start, start + new TimeSpan(0, minutesDuration, 0)) { }

    }
}
