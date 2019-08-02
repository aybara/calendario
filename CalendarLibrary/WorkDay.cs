using System;
using System.Collections.Generic;
using System.Linq;

namespace CalendarLibrary
{
    public class WorkDay
    {
        public DayOfWeek DayOfWeek { get; }
        public TimeSpan Start { get; }
        public TimeSpan End { get; }
        public List<WorkDayInterval> Intervals { get; }
        public WorkDay(DayOfWeek dayOfWeek, TimeSpan start, TimeSpan end, List<WorkDayInterval> intervals)
        {
            DayOfWeek = dayOfWeek;
            Start = start;
            End = end;
            Intervals = intervals;
        }
        public WorkDay(DayOfWeek dayOfWeek, TimeSpan start, TimeSpan end) : this(dayOfWeek, start, end, new List<WorkDayInterval>()) { }
        public void SubscribeInterval(WorkDayInterval interval)
        {
            if (!Intervals.Contains(interval) &&
                !Intervals.Where(i => (interval.Start >= i.Start && interval.Start < i.End) || (interval.End > i.Start && interval.End <= i.End)).Any())
                Intervals.Add(interval);
        }
        public void UnsubscribeInterval(WorkDayInterval interval)
        {
            if (Intervals.Contains(interval))
                Intervals.Remove(interval);
        }
    }
}
