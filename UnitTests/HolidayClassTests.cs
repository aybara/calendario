using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalendarLibrary;

namespace UnitTests
{
    [TestClass]
    public class HolidayClassTests
    {
        [TestMethod]
        public void CreateHoliday_OnlyDate()
        {
            DateTime date = new DateTime(2019, 8, 1);
            Holiday holiday = new Holiday(date);
            Assert.AreEqual(date.Date, holiday.Start);
            Assert.AreEqual(date.AddDays(1).Date, holiday.End);
            Assert.AreEqual(new TimeSpan(24, 0, 0), holiday.Duration);
        }
        [TestMethod]
        public void CreateHoliday_Start_End()
        {
            DateTime dateStart = new DateTime(2019, 8, 1);
            DateTime dateEnd = new DateTime(2019, 8, 15);
            Holiday holiday = new Holiday(dateStart, dateEnd);
            Assert.AreEqual(dateStart.Date, holiday.Start);
            Assert.AreEqual(dateEnd.Date, holiday.End);
            Assert.AreEqual(dateEnd.Date - dateStart.Date, holiday.Duration);
        }
        [TestMethod]
        public void CreateHoliday_Date_DaysDuration()
        {
            DateTime date = new DateTime(2019, 8, 1);
            int day = 5;
            Holiday holiday = new Holiday(date, day);
            Assert.AreEqual(date, holiday.Start);
            Assert.AreEqual(date.AddDays(day), holiday.End);
            Assert.AreEqual(new TimeSpan(day * 24, 0, 0), holiday.Duration);
        }
    }
}
