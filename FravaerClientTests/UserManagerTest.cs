using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Managers;
using DateTimeExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceGateways.Entities;
using ServiceGateways.Interfaces;

namespace FravaerClientTests
{
    [TestClass]
    public class UserManagerTest
    {
        [TestMethod]
        public void AddAbsenceToUserTest()
        {
            var absenceDate = new DateTime(2016, 12, 15);
            var status = Statuses.A;
            User user = new User() { Id = 1 };
            var newAbsence = new Absence()
            {
                Date = absenceDate,
                Status = status,
                User = user
            };

            Assert.AreEqual(newAbsence.User, user);
            Assert.AreEqual(newAbsence.Date, absenceDate);
            Assert.AreEqual(newAbsence.Status, status);
        }

        [TestMethod]
        public void GetAbsenceForDateTest()
        {
            var testDate = new DateTime(2016, 12, 15);
            var expectedAbsence = new Absence() {Date = testDate, Id = 2};
            var absenceLst = new List<Absence>(){
                new Absence() {Date = new DateTime(2016, 12, 14), Id = 1},
                new Absence() { Date = new DateTime(2016, 12, 15), Id = 2},
                new Absence() { Date = new DateTime(2016, 12, 16), Id = 3}
            };
            var result = absenceLst.FirstOrDefault(x => x.Date == testDate);
            Assert.IsNotNull(result.Id);
            Assert.AreEqual(expectedAbsence.Id, result.Id);
            Assert.AreEqual(expectedAbsence.Date, result.Date);
        }

        [TestMethod]
        public void GetWeekCountFromMonth()
        {
            var testDate1 = new DateTime(2016, 12, 1);
            var initIndex1 = 3;
            var expectedResult1 = 5;
            var result1 = -1;
            var testDate2 = new DateTime(2016, 10, 1);
            var initIndex2 = 5;
            var expectedResult2 = 6;
            var result2 = -1;
            if (testDate1.LastDayOfTheMonth().Day > 30 && initIndex1 > 4 || testDate1.LastDayOfTheMonth().Day > 29 && initIndex1 > 5)
            {
                result1 = 6;
            }
            else
            {
                result1 = 5;
            }
            if (testDate2.LastDayOfTheMonth().Day > 30 && initIndex2 > 4 || testDate2.LastDayOfTheMonth().Day > 29 && initIndex2 > 5)
            {
                result2 = 6;
            }
            else
            {
                result2 = 5;
            }
            Assert.AreNotEqual(result1, -1);

            Assert.AreNotEqual(result2, -1);

            Assert.AreEqual(result1, expectedResult1);

            Assert.AreEqual(result2, expectedResult2);
        }
        [TestMethod]
        public void GetStartIndexTest()
        {

            DateTime DateToTest = new DateTime(2016, 12, 1);

            int WeekNumber = 1;
            int WeekDaysCount = 7;

            int initIndex = GetInitIndexTest(DateToTest);
            int result = (WeekNumber * WeekDaysCount) - initIndex;

            if (result < 0)
            {
                Assert.AreEqual(((WeekNumber * WeekDaysCount) + 1), 8);
            }
            else
            {
                Assert.AreEqual(((WeekNumber * WeekDaysCount) + 1 - initIndex), 5);
            }
        }

        [TestMethod]
        public int GetInitIndexTest(DateTime MonthToTest)
        {
            ArrayList _daysList = new ArrayList() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

            DateTime December = new DateTime(2016, 12, 1);
            DateTime January = new DateTime(2017, 1, 1);

            int TestDecember = _daysList.IndexOf(December.FirstDayOfTheMonth().DayOfWeek.ToString());
            int DecemberIndex = 3;

            int TestJanuary = _daysList.IndexOf(January.FirstDayOfTheMonth().DayOfWeek.ToString());
            int JanuaryIndex = 6;

            Assert.IsNotNull(TestDecember);
            Assert.AreEqual(TestDecember, DecemberIndex);

            Assert.IsNotNull(TestJanuary);
            Assert.AreEqual(TestJanuary, JanuaryIndex);

            return _daysList.IndexOf(MonthToTest.FirstDayOfTheMonth().DayOfWeek.ToString());
        }
    }
}
