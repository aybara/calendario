using System;
using System.Collections.Generic;
using System.Linq;

namespace CalendarLibrary
{
    public class Calendar
    {
        public List<WorkDay> WorkDays { get; }
        public List<Holiday> Holidays { get; }
        public Calendar(List<WorkDay> workDays, List<Holiday> holidays)
        {
            WorkDays = workDays;
            Holidays = holidays;
        }

        public bool IsWorkDate(DateTime date)
        {
            bool isWorkDate = false;
            if(WorkDays.Where(wd => wd.DayOfWeek.Equals(date.DayOfWeek) && date.TimeOfDay >= wd.Start && date.TimeOfDay < wd.End).Any() &&
               !Holidays.Where(h => h.Start.Date == date.Date).Any())
            {
                isWorkDate = true;
            }
            return isWorkDate;
        }
        public DateTime GetEndDateTime(DateTime start, TimeSpan interval)
        {
            throw new NotImplementedException();
        }
    }
}
