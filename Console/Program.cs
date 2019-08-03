using System;
using System.Collections.Generic;
using CalendarLibrary;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var start = new TimeSpan(8, 0, 0);
            var end = new TimeSpan(17, 0, 0);
            var bk = new WorkDayInterval(new TimeSpan(12, 0, 0), 60);
            var monday = new WorkDay(DayOfWeek.Monday, start, end, new List<WorkDayInterval>() { bk });
            var tuesday = new WorkDay(DayOfWeek.Tuesday, start, end, new List<WorkDayInterval>() { bk });
            var wednesday = new WorkDay(DayOfWeek.Wednesday, start, end, new List<WorkDayInterval>() { bk });
            var thursday = new WorkDay(DayOfWeek.Thursday, start, end, new List<WorkDayInterval>() { bk });
            var friday = new WorkDay(DayOfWeek.Friday, start, end, new List<WorkDayInterval>() { bk });

            //Feriados
            var diaDaIndependencia = new Holiday(new DateTime(2019, 9, 7));
            var natal = new Holiday(new DateTime(2019, 12, 25));

            var workDays = new List<WorkDay>() { monday, tuesday, wednesday, thursday, friday };
            var holidays = new List<Holiday>() { diaDaIndependencia, natal };
            var calendarDefault = new Calendar(workDays, holidays);
            var Date = DateTime.Now;
            DayOfWeek d = DayOfWeek.Saturday;
            d += 1;
        }
    }
}
