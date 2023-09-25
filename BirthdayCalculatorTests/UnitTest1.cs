
using BirthdayCalculator.Controllers;
using BirthdayCalculator.Models;
using Microsoft.AspNetCore.Mvc;


namespace BirthdayCalculatorTests
{
    public class UnitTest1
    {
        [Fact]
        public void TodayIsPersonsBirthday()
        {
            //Arange
            var response = new BirthdayResponse { Day = DateTime.Now.Day, Month = DateTime.Now.Month, Year = DateTime.Now.Year, Name = "Vandaag" };

            var birthDate = response.getBirthDate();

            Assert.Equal(DateTime.Today, birthDate);
        }

        [Fact]
        public void IndexShouldreturnIndexView()
        {
            var sut = new HomeController();

            var result = sut.Index() as ViewResult;

            Assert.Equal("Index", result.ViewName);
        }

        [Fact]
        public void ShouldReturnGongratulationsIfTodayIsBirthDay()
        {
            var sut = new HomeController();

            BirthdayResponse response = new BirthdayResponse { 
                Name = "Rathalos", 
                Day = DateTime.Today.Day, 
                Month = DateTime.Today.Month, 
                Year = DateTime.Today.Year - 10 
            };

            var result = sut.BirthdayForm(response);

            Assert.Equal("Gongratulations", result.ViewName);
        }

        [Fact]
        public void ShouldReturnThanksIfTodayIsNotBirthDay()
        {
            var sut = new HomeController();

            BirthdayResponse response = new BirthdayResponse
            {
                Name = "Rathalos",
                Day = DateTime.Today.Day + 1,
                Month = DateTime.Today.Month,
                Year = DateTime.Today.Year - 10
            };

            var result = sut.BirthdayForm(response);

            Assert.Equal(1, result.ViewData["Days"]);
            Assert.Equal("DaysToWait", result.ViewName);
        }
    }
}