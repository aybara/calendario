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

            var workDays = new List<WorkDay>() { monday, tuesday, wednesday, thursday, friday };
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
            Assert.AreEqual(true, calendarDefault.IsWorkDate(new DateTime(2019, 9, 6, 8, 10, 0)));
            Assert.AreEqual(true, calendarDefault.IsWorkDate(new DateTime(2019, 8, 23, 10, 5, 6)));
            Assert.AreEqual(true, calendarDefault.IsWorkDate(new DateTime(2019, 10, 15, 16, 59, 59)));
            Assert.AreEqual(false, calendarDefault.IsWorkDate(new DateTime(2019, 9, 6, 7, 59, 59)));
            Assert.AreEqual(false, calendarDefault.IsWorkDate(new DateTime(2019, 8, 15, 22, 59, 59)));
        }
        [TestMethod]
        public void IsWorkDate_FimDeSemana_CalendarDefault()
        {
            Assert.AreEqual(false, calendarDefault.IsWorkDate(new DateTime(2019, 8, 10, 9, 31, 25)));
        }
        [TestMethod]
        public void IsWorkDate_Feriados_CalendarDefault()
        {
            Assert.AreEqual(false, calendarDefault.IsWorkDate(new DateTime(2019, 9, 7, 2, 50, 55)));
            Assert.AreEqual(false, calendarDefault.IsWorkDate(new DateTime(2019, 9, 7, 14, 3, 5)));
            Assert.AreEqual(false, calendarDefault.IsWorkDate(new DateTime(2019, 12, 25, 13, 26, 41)));
            Assert.AreEqual(false, calendarDefault.IsWorkDate(new DateTime(2019, 12, 25, 22, 17, 23)));
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
            Assert.AreEqual(new DateTime(2019, 8, 8, 10, 0, 0), calendarDefault.GetEndDateTime(new DateTime(2019, 8, 1, 10, 0, 0), new TimeSpan(56, 0, 0)));
        }
    }
}
