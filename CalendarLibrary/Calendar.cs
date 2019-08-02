using System;
using System.Collections.Generic;
using System.Linq;

namespace CalendarLibrary
{
    public interface ICalendar
    {
        bool IsWorkDate(DateTime date);
        DateTime GetEndDateTime(DateTime start, TimeSpan interval);
        void SubscribeWorkDay(WorkDay workDay);
        void UnsubscribeWorkDay(WorkDay workDay);
        void SubscribeHoliday(Holiday holiday);
        void UnsubscribeHoliday(Holiday holiday);
    }
    public class Calendar : ICalendar
    {
        public List<WorkDay> WorkDays { get; }
        public List<Holiday> Holidays { get; }
        public Calendar(List<WorkDay> workDays, List<Holiday> holidays)
        {
            WorkDays = workDays.OrderBy(wd => wd.DayOfWeek).ToList();
            Holidays = holidays;
        }
        public Calendar()
        {
            WorkDays = new List<WorkDay>();
            Holidays = new List<Holiday>();
        }
        public bool IsWorkDate(DateTime date)
        {
            if(WorkDays.Where(wd => wd.DayOfWeek.Equals(date.DayOfWeek) && date.TimeOfDay >= wd.Start && date.TimeOfDay < wd.End).Any() &&
               !Holidays.Where(h => h.Start.Date == date.Date).Any())
            {
                return true;
            }
            return false;
        }
        public DateTime GetEndDateTime(DateTime start, TimeSpan interval)
        {
            throw new NotImplementedException();
        }
        public void SubscribeWorkDay(WorkDay workDay)
        {
            if(!WorkDays.Contains(workDay) && !WorkDays.Where(w => w.DayOfWeek == workDay.DayOfWeek).Any())
            {
                WorkDays.Add(workDay);
                //WorkDays.OrderBy(wd => wd.DayOfWeek).ToList();
            }
        }
        public void UnsubscribeWorkDay(WorkDay workDay)
        {
            if (WorkDays.Contains(workDay))
                WorkDays.Remove(workDay);
        }
        public void SubscribeHoliday(Holiday holiday)
        {
            if (!Holidays.Contains(holiday))
            {
                Holidays.Add(holiday);
            }
        }
        public void UnsubscribeHoliday(Holiday holiday)
        {
            if (Holidays.Contains(holiday))
                Holidays.Remove(holiday);
        }
    }
}
