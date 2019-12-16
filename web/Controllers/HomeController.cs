using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ES;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RabbitmQ;
using Service.Queue;
using web.Models;

namespace web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQueueService _queueService;
        public HomeController(IQueueService queueService)
        {
            _queueService = queueService;
        }

        public IActionResult Index()
        {
            return View();
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
