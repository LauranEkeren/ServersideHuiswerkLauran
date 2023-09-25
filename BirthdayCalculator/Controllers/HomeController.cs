using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BirthdayCalculator.Models;

namespace BirthdayCalculator.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        string today = DateTime.Today.ToString("dd-MM-yyyy");
        return View("Index", today);
    }

    [HttpGet]
    public ViewResult BirthdayForm()
    {
        return View();
    }

    [HttpPost]
    public ViewResult BirthdayForm(BirthdayResponse response)
    {
        if (ModelState.IsValid)
        {
            if (response.Day == DateTime.Now.Day &&
                response.Month == DateTime.Now.Month)
            {
                ViewBag.Name = response.Name;
                ViewBag.Year = DateTime.Now.Year - response.Year;
                return View("Gongratulations");
            } else
            {
                DateTime birthday = response.getBirthDate();
                DateTime today = DateTime.Today;
                DateTime next = birthday.AddYears(today.Year - birthday.Year);

                if (next < today)
                    next = next.AddYears(1);

                int numDays = (next - today).Days;
                ViewBag.Name = response.Name;
                ViewBag.Days = numDays;
                return View("DaysToWait");
            }
        } else
        {
            return View();
        }
    }
}
