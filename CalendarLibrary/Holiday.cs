using System;

namespace CalendarLibrary
{
    public class Holiday
    {
        public DateTime Start { get; }
        public DateTime End { get; }
        public TimeSpan Duration
        {
            get { return End - Start; }
        }
        public Holiday(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }
        public Holiday(DateTime startDay, int daysDuration) : this(startDay.Date, startDay.Date.AddDays(daysDuration)) { }
        public Holiday(DateTime day) : this(day.Date, day.Date.AddDays(1)) { }
    }
}
