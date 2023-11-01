using Appointment_Scheduler.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;

namespace Appointment_Scheduler.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            int Days = System.DateTime.Now.Day;
            var DaysInMonth = System.DateTime.DaysInMonth(System.DateTime.Now.Year, System.DateTime.Now.Month);
            int[] DaysArray = { Days - 2, Days - 1, Days + 1, Days + 2, Days + 3, Days };
            for (int index = 0; index < DaysArray.Length; index++)
            {
                int DayArrayIndex = DaysArray[index];
                int controllInt = 2;
                if (DayArrayIndex == DaysInMonth++)
                {
                    DaysArray[index] = 1;
                }
                else if (DayArrayIndex > DaysInMonth && DayArrayIndex > DaysArray[index - 1])
                {
                    DaysArray[index] = DaysArray[index - 1] + 1;
                }
                else if (DayArrayIndex < 1)
                {
                    DaysArray[index] = System.DateTime.DaysInMonth(System.DateTime.Now.Year, System.DateTime.Now.Month - 1) - controllInt;
                    controllInt--;
                }
            }
            {
                return View(DaysArray);
            }
        }

        public IActionResult Privacy()
        {
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}