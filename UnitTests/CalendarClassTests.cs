using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalendarLibrary;

namespace UnitTests
{
    [TestClass]
    public class CalendarClassTests
    {
        static Calendar calendarDefault;

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            #region Inicia Calendário Padrão
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

            var workDays = new List<WorkDay>() { wednesday, friday, monday, thursday, tuesday };
            var holidays = new List<Holiday>() { diaDaIndependencia, natal };
            calendarDefault = new Calendar(workDays, holidays);
            #endregion
        }
        [ClassCleanup]
        public static void Cleanup()
        {

        }
        [TestMethod]
        public void IsWorkDate_DiaDaSemana_CalendarDefault()
        {
            Assert.AreEqual(true, calendarDefault.IsWorkDay(new DateTime(2019, 9, 6, 8, 10, 0)));
            Assert.AreEqual(true, calendarDefault.IsWorkDay(new DateTime(2019, 8, 23, 10, 5, 6)));
            Assert.AreEqual(true, calendarDefault.IsWorkDay(new DateTime(2019, 10, 15, 16, 59, 59)));
            Assert.AreEqual(false, calendarDefault.IsWorkDay(new DateTime(2019, 9, 6, 7, 59, 59)));
            Assert.AreEqual(false, calendarDefault.IsWorkDay(new DateTime(2019, 8, 15, 22, 59, 59)));
        }
        [TestMethod]
        public void IsWorkDate_FimDeSemana_CalendarDefault()
        {
            Assert.AreEqual(false, calendarDefault.IsWorkDay(new DateTime(2019, 8, 10, 9, 31, 25)));
        }
        [TestMethod]
        public void IsWorkDate_Feriados_CalendarDefault()
        {
            Assert.AreEqual(false, calendarDefault.IsWorkDay(new DateTime(2019, 9, 7, 2, 50, 55)));
            Assert.AreEqual(false, calendarDefault.IsWorkDay(new DateTime(2019, 9, 7, 14, 3, 5)));
            Assert.AreEqual(false, calendarDefault.IsWorkDay(new DateTime(2019, 12, 25, 13, 26, 41)));
            Assert.AreEqual(false, calendarDefault.IsWorkDay(new DateTime(2019, 12, 25, 22, 17, 23)));
        }
        [TestMethod]
        public void GetEndDateTime_CalendarDefault()
        {
            Assert.AreEqual(new DateTime(2019, 8, 1, 10, 0, 0), calendarDefault.GetEndDateTime(new DateTime(2019, 8, 1, 8, 0, 0), new TimeSpan(2, 0, 0)));
            Assert.AreEqual(new DateTime(2019, 8, 1, 9, 0, 0), calendarDefault.GetEndDateTime(new DateTime(2019, 8, 1, 8, 0, 0), new TimeSpan(1, 0, 0)));
            Assert.AreEqual(new DateTime(2019, 8, 1, 17, 0, 0), calendarDefault.GetEndDateTime(new DateTime(2019, 8, 1, 8, 0, 0), new TimeSpan(8, 0, 0)));
            Assert.AreEqual(new DateTime(2019, 8, 1, 13, 30, 0), calendarDefault.GetEndDateTime(new DateTime(2019, 8, 1, 8, 0, 0), new TimeSpan(4, 30, 0)));
            Assert.AreEqual(new DateTime(2019, 8, 2, 9, 0, 0), calendarDefault.GetEndDateTime(new DateTime(2019, 8, 1, 8, 0, 0), new TimeSpan(9, 0, 0)));
            Assert.AreEqual(new DateTime(2019, 8, 2, 14, 0, 0), calendarDefault.GetEndDateTime(new DateTime(2019, 8, 1, 8, 0, 0), new TimeSpan(13, 0, 0)));
            Assert.AreEqual(new DateTime(2019, 8, 2, 17, 0, 0), calendarDefault.GetEndDateTime(new DateTime(2019, 8, 1, 8, 0, 0), new TimeSpan(16, 0, 0)));
            Assert.AreEqual(new DateTime(2019, 8, 5, 9, 0, 0), calendarDefault.GetEndDateTime(new DateTime(2019, 8, 1, 8, 0, 0), new TimeSpan(17, 0, 0)));
            Assert.AreEqual(new DateTime(2019, 8, 6, 9, 0, 0), calendarDefault.GetEndDateTime(new DateTime(2019, 8, 1, 8, 0, 0), new TimeSpan(25, 0, 0)));
            Assert.AreEqual(new DateTime(2019, 8, 1, 12, 0, 0), calendarDefault.GetEndDateTime(new DateTime(2019, 8, 1, 10, 0, 0), new TimeSpan(2, 0, 0)));
            Assert.AreEqual(new DateTime(2019, 8, 1, 14, 0, 0), calendarDefault.GetEndDateTime(new DateTime(2019, 8, 1, 10, 0, 0), new TimeSpan(3, 0, 0)));
            Assert.AreEqual(new DateTime(2019, 8, 2, 11, 0, 0), calendarDefault.GetEndDateTime(new DateTime(2019, 8, 1, 11, 0, 0), new TimeSpan(8, 0, 0)));
            Assert.AreEqual(new DateTime(2019, 8, 5, 11, 0, 0), calendarDefault.GetEndDateTime(new DateTime(2019, 8, 1, 11, 0, 0), new TimeSpan(16, 0, 0)));
            Assert.AreEqual(new DateTime(2019, 8, 12, 10, 0, 0), calendarDefault.GetEndDateTime(new DateTime(2019, 8, 1, 10, 0, 0), new TimeSpan(56, 0, 0)));
        }
        [TestMethod]
        public void GetWorkDay_CalendarDefault()
        {
            Assert.AreEqual(DayOfWeek.Thursday, calendarDefault.GetWorkDay_OrNext(new DateTime(2019, 8, 1)).DayOfWeek);
            Assert.AreEqual(DayOfWeek.Friday, calendarDefault.GetWorkDay_OrNext(new DateTime(2019, 8, 2)).DayOfWeek);
            Assert.AreEqual(DayOfWeek.Monday, calendarDefault.GetWorkDay_OrNext(new DateTime(2019, 8, 3)).DayOfWeek);
            Assert.AreEqual(DayOfWeek.Monday, calendarDefault.GetWorkDay_OrNext(new DateTime(2019, 8, 4)).DayOfWeek);
            Assert.AreEqual(DayOfWeek.Monday, calendarDefault.GetWorkDay_OrNext(new DateTime(2019, 8, 5)).DayOfWeek);
        }
        [TestMethod]
        public void NextWorkDay_CalendarDefault()
        {
            Assert.AreEqual(DayOfWeek.Friday, calendarDefault.NextWorkDay(new DateTime(2019, 8, 1).DayOfWeek).DayOfWeek);
            Assert.AreEqual(DayOfWeek.Monday, calendarDefault.NextWorkDay(new DateTime(2019, 8, 2).DayOfWeek).DayOfWeek);
            Assert.AreEqual(DayOfWeek.Monday, calendarDefault.NextWorkDay(new DateTime(2019, 8, 3).DayOfWeek).DayOfWeek);
            Assert.AreEqual(DayOfWeek.Monday, calendarDefault.NextWorkDay(new DateTime(2019, 8, 4).DayOfWeek).DayOfWeek);
            Assert.AreEqual(DayOfWeek.Tuesday, calendarDefault.NextWorkDay(new DateTime(2019, 8, 5).DayOfWeek).DayOfWeek);
        }
        [TestMethod]
        public void GetWorkDay_CalendarCustom()
        {
            var start = new TimeSpan(8, 0, 0);
            var end = new TimeSpan(17, 0, 0);
            var bk = new WorkDayInterval(new TimeSpan(12, 0, 0), 60);
            var monday = new WorkDay(DayOfWeek.Monday, start, end, new List<WorkDayInterval>() { bk });
            var tuesday = new WorkDay(DayOfWeek.Tuesday, start, end, new List<WorkDayInterval>() { bk });
            var thursday = new WorkDay(DayOfWeek.Thursday, start, end, new List<WorkDayInterval>() { bk });
            var sunday = new WorkDay(DayOfWeek.Sunday, start, end, new List<WorkDayInterval>() { bk });

            var workDays = new List<WorkDay>() { monday, thursday, tuesday, sunday };
            var customCalendar = new Calendar(workDays, null);

            Assert.AreEqual(DayOfWeek.Thursday, customCalendar.GetWorkDay_OrNext(new DateTime(2019, 8, 1)).DayOfWeek);
            Assert.AreEqual(DayOfWeek.Sunday, customCalendar.GetWorkDay_OrNext(new DateTime(2019, 8, 2)).DayOfWeek);
            Assert.AreEqual(DayOfWeek.Sunday, customCalendar.GetWorkDay_OrNext(new DateTime(2019, 8, 3)).DayOfWeek);
            Assert.AreEqual(DayOfWeek.Sunday, customCalendar.GetWorkDay_OrNext(new DateTime(2019, 8, 4)).DayOfWeek);
            Assert.AreEqual(DayOfWeek.Monday, customCalendar.GetWorkDay_OrNext(new DateTime(2019, 8, 5)).DayOfWeek);
            Assert.AreEqual(DayOfWeek.Tuesday, customCalendar.GetWorkDay_OrNext(new DateTime(2019, 8, 6)).DayOfWeek);
            Assert.AreEqual(DayOfWeek.Thursday, customCalendar.GetWorkDay_OrNext(new DateTime(2019, 8, 7)).DayOfWeek);
        }
        [TestMethod]
        public void GetWorkDay_CalendarCustom2()
        {
            var start = new TimeSpan(8, 0, 0);
            var end = new TimeSpan(17, 0, 0);
            var bk = new WorkDayInterval(new TimeSpan(12, 0, 0), 60);
            var monday = new WorkDay(DayOfWeek.Monday, start, end, new List<WorkDayInterval>() { bk });

            var workDays = new List<WorkDay>() { monday };
            var customCalendar = new Calendar(workDays, null);

            Assert.AreEqual(DayOfWeek.Monday, customCalendar.GetWorkDay_OrNext(new DateTime(2019, 8, 1)).DayOfWeek);
            Assert.AreEqual(DayOfWeek.Monday, customCalendar.GetWorkDay_OrNext(new DateTime(2019, 8, 2)).DayOfWeek);
            Assert.AreEqual(DayOfWeek.Monday, customCalendar.GetWorkDay_OrNext(new DateTime(2019, 8, 3)).DayOfWeek);
            Assert.AreEqual(DayOfWeek.Monday, customCalendar.GetWorkDay_OrNext(new DateTime(2019, 8, 4)).DayOfWeek);
            Assert.AreEqual(DayOfWeek.Monday, customCalendar.GetWorkDay_OrNext(new DateTime(2019, 8, 5)).DayOfWeek);
            Assert.AreEqual(DayOfWeek.Monday, customCalendar.GetWorkDay_OrNext(new DateTime(2019, 8, 6)).DayOfWeek);
            Assert.AreEqual(DayOfWeek.Monday, customCalendar.GetWorkDay_OrNext(new DateTime(2019, 8, 7)).DayOfWeek);
            Assert.AreEqual(DayOfWeek.Monday, customCalendar.GetWorkDay_OrNext(new DateTime(2019, 8, 8)).DayOfWeek);
        }
        [TestMethod]
        public void GetWorkDay_CalendarCustom3()
        {
            var start = new TimeSpan(8, 0, 0);
            var end = new TimeSpan(17, 0, 0);
            var bk = new WorkDayInterval(new TimeSpan(12, 0, 0), 60);

            var workDays = new List<WorkDay>();
            var customCalendar = new Calendar(workDays, null);

            Assert.AreEqual(null, customCalendar.GetWorkDay_OrNext(new DateTime(2019, 8, 1))?.DayOfWeek);
            Assert.AreEqual(null, customCalendar.GetWorkDay_OrNext(new DateTime(2019, 8, 2))?.DayOfWeek);
            Assert.AreEqual(null, customCalendar.GetWorkDay_OrNext(new DateTime(2019, 8, 3))?.DayOfWeek);
            Assert.AreEqual(null, customCalendar.GetWorkDay_OrNext(new DateTime(2019, 8, 4))?.DayOfWeek);
            Assert.AreEqual(null, customCalendar.GetWorkDay_OrNext(new DateTime(2019, 8, 5))?.DayOfWeek);
            Assert.AreEqual(null, customCalendar.GetWorkDay_OrNext(new DateTime(2019, 8, 6))?.DayOfWeek);
            Assert.AreEqual(null, customCalendar.GetWorkDay_OrNext(new DateTime(2019, 8, 7))?.DayOfWeek);
            Assert.AreEqual(null, customCalendar.GetWorkDay_OrNext(new DateTime(2019, 8, 8))?.DayOfWeek);
        }
        public void NextWorkDay_CalendarCustom()
        {
            var start = new TimeSpan(8, 0, 0);
            var end = new TimeSpan(17, 0, 0);
            var bk = new WorkDayInterval(new TimeSpan(12, 0, 0), 60);
            var monday = new WorkDay(DayOfWeek.Monday, start, end, new List<WorkDayInterval>() { bk });
            var tuesday = new WorkDay(DayOfWeek.Tuesday, start, end, new List<WorkDayInterval>() { bk });
            var thursday = new WorkDay(DayOfWeek.Thursday, start, end, new List<WorkDayInterval>() { bk });
            var sunday = new WorkDay(DayOfWeek.Sunday, start, end, new List<WorkDayInterval>() { bk });

            var workDays = new List<WorkDay>() { monday, thursday, tuesday, sunday };
            var customCalendar = new Calendar(workDays, null);

            Assert.AreEqual(DayOfWeek.Thursday, customCalendar.GetWorkDay_OrNext(new DateTime(2019, 8, 1)).DayOfWeek);
            Assert.AreEqual(DayOfWeek.Sunday, customCalendar.GetWorkDay_OrNext(new DateTime(2019, 8, 2)).DayOfWeek);
            Assert.AreEqual(DayOfWeek.Sunday, customCalendar.GetWorkDay_OrNext(new DateTime(2019, 8, 3)).DayOfWeek);
            Assert.AreEqual(DayOfWeek.Sunday, customCalendar.GetWorkDay_OrNext(new DateTime(2019, 8, 4)).DayOfWeek);
            Assert.AreEqual(DayOfWeek.Monday, customCalendar.GetWorkDay_OrNext(new DateTime(2019, 8, 5)).DayOfWeek);
            Assert.AreEqual(DayOfWeek.Tuesday, customCalendar.GetWorkDay_OrNext(new DateTime(2019, 8, 6)).DayOfWeek);
            Assert.AreEqual(DayOfWeek.Thursday, customCalendar.GetWorkDay_OrNext(new DateTime(2019, 8, 7)).DayOfWeek);
        }
    }
}
