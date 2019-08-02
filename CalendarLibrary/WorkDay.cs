using System;
using System.Collections.Generic;

namespace CalendarLibrary
{
    public class WorkDay
    {
        public DayOfWeek DayOfWeek { get; }
        public TimeSpan Start { get; }
        public TimeSpan End { get; }
        public List<WorkDayBreak> Breaks { get; }
        public WorkDay(DayOfWeek dayOfWeek, TimeSpan start, TimeSpan end, List<WorkDayBreak> breaks)
        {
            DayOfWeek = dayOfWeek;
            Start = start;
            End = end;
            Breaks = breaks;
        }
    }
}
