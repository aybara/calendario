using System;
using System.Collections.Generic;
using System.Linq;
using CalendarLibrary.Extensions;

namespace CalendarLibrary
{
    public interface ICalendar
    {
        bool IsWorkDay(DateTime date);
        DateTime GetEndDateTime(DateTime start, TimeSpan interval);
        WorkDay NextWorkDay(WorkDay workDay);
        WorkDay NextWorkDay(DayOfWeek dayOfWeek);
        WorkDay GetWorkDay_OrNext(DayOfWeek dayOfWeek);
        WorkDay GetWorkDay_OrNext(DateTime dateTime);
        TimeSpan AdditionalFromWorkDayIntervals(WorkDay workDay, TimeSpan start, TimeSpan end);
        void SubscribeWorkDay(WorkDay workDay);
        void UnsubscribeWorkDay(WorkDay workDay);
        void SubscribeHoliday(Holiday holiday);
        void UnsubscribeHoliday(Holiday holiday);
    }
    public class Calendar : ICalendar
    {
        private List<WorkDay> _workDays;
        public List<WorkDay> WorkDays
        {
            get { return _workDays; }
        }
        private List<Holiday> _holidays;
        public List<Holiday> Holidays
        {
            get { return _holidays; }
        }
        public Calendar(List<WorkDay> workDays, List<Holiday> holidays)
        {
            _workDays = workDays.OrderBy(wd => wd.DayOfWeek).ToList();
            _holidays = holidays;
        }
        public Calendar()
        {
            _workDays = new List<WorkDay>();
            _holidays = new List<Holiday>();
        }
        public bool IsWorkDay(DateTime date)
        {
            if (_workDays.Where(wd => wd.DayOfWeek.Equals(date.DayOfWeek) && date.TimeOfDay >= wd.Start && date.TimeOfDay < wd.End).Any() && !IsHoliday(date))
                return true;
            return false;
        }
        public bool IsHoliday(DateTime date)
        {
            if (_holidays.Where(h => h.Start.Date == date.Date).Any())
                return true;
            return false;
        }
        public DateTime GetEndDateTime(DateTime start, TimeSpan span)
        {
            DateTime end = start;
            while (!IsWorkDay(end))
            {
                end = GoNextWorkDateStart(end);
            }
            WorkDay workDay = GetWorkDay_OrNext(end);
            if (end.TimeOfDay < workDay.Start)
                end += workDay.Start - end.TimeOfDay;
            else if (end.TimeOfDay > workDay.End)
            {
                do
                {
                    end = GoNextWorkDateStart(end);
                } while (!IsWorkDay(end));
            }

            TimeSpan additionalFromWorkDayIntervals = AdditionalFromWorkDayIntervals(workDay, end.TimeOfDay, end.TimeOfDay + span);
            span += additionalFromWorkDayIntervals;
            while (end.TimeOfDay + span > workDay.End)
            {
                span -= workDay.End - end.TimeOfDay;
                end = GoNextWorkDateStart(end);
                workDay = GetWorkDay_OrNext(end);
                additionalFromWorkDayIntervals = AdditionalFromWorkDayIntervals(workDay, end.TimeOfDay, end.TimeOfDay + span);
                span += additionalFromWorkDayIntervals;
            }

            return end + span;
        }
        private DateTime GoNextWorkDateStart(DateTime dateTime)
        {
            WorkDay nextWorkDay = NextWorkDay(dateTime.DayOfWeek);
            dateTime = dateTime.Date + nextWorkDay.Start;
            int days = nextWorkDay.DayOfWeek - dateTime.DayOfWeek;
            if (nextWorkDay.DayOfWeek < dateTime.DayOfWeek)
                days += 7;
            dateTime = dateTime.AddDays(days);
            return dateTime;
        }
        public WorkDay NextWorkDay(WorkDay workDay)
        {
            return NextWorkDay(workDay.DayOfWeek + 1);
        }
        public WorkDay NextWorkDay(DayOfWeek dayOfWeek)
        {
            if (dayOfWeek != DayOfWeek.Saturday)
                return GetWorkDay_OrNext(dayOfWeek + 1);
            else
                return GetWorkDay_OrNext(DayOfWeek.Sunday);
        }
        public WorkDay GetWorkDay_OrNext(DayOfWeek dayOfWeek)
        {
            WorkDay workDay = _workDays.Where(wd => wd.DayOfWeek == dayOfWeek).FirstOrDefault();
            int days = 0;
            while (workDay == null && days < 7)
            {
                days++;
                if (dayOfWeek != DayOfWeek.Saturday)
                    dayOfWeek += 1;
                else
                    dayOfWeek = DayOfWeek.Sunday;
                workDay = _workDays.Where(wd => wd.DayOfWeek == dayOfWeek).FirstOrDefault();
            }

            return workDay;
        }
        public WorkDay GetWorkDay_OrNext(DateTime dateTime)
        {
            return GetWorkDay_OrNext(dateTime.DayOfWeek);
        }
        public TimeSpan AdditionalFromWorkDayIntervals(WorkDay workDay, TimeSpan start, TimeSpan end)
        {
            TimeSpan additional = TimeSpan.Zero;
            if (start < end)
            {
                TimeSpan difference;
                foreach (WorkDayInterval interval in workDay.Intervals)
                {
                    difference = TimeSpan.Zero;
                    if (interval.Start >= start && interval.Start < end)
                        difference = interval.End - start;
                    else if (interval.End > start && interval.End <= end)
                        difference = interval.End - end;
                    additional += TimeSpanExtensions.Min(interval.Duration, difference);
                }
            }
            return additional;
        }
        public void SubscribeWorkDay(WorkDay workDay)
        {
            if(!_workDays.Contains(workDay) && !_workDays.Where(w => w.DayOfWeek == workDay.DayOfWeek).Any())
            {
                _workDays.Add(workDay);
                _workDays = _workDays.OrderBy(wd => wd.DayOfWeek).ToList();
            }
        }
        public void UnsubscribeWorkDay(WorkDay workDay)
        {
            if (_workDays.Contains(workDay))
                _workDays.Remove(workDay);
        }
        public void SubscribeHoliday(Holiday holiday)
        {
            if (!_holidays.Contains(holiday))
            {
                _holidays.Add(holiday);
            }
        }
        public void UnsubscribeHoliday(Holiday holiday)
        {
            if (_holidays.Contains(holiday))
                _holidays.Remove(holiday);
        }
    }
}
