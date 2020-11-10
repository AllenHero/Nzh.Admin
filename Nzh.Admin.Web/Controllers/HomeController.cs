using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nzh.Admin.IService;
using Nzh.Admin.Web.Models;

namespace Nzh.Admin.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IDemoService _demoService;

        public HomeController(ILogger<HomeController> logger, IDemoService demoService)
        {
            _logger = logger;
            _demoService = demoService;
        }

        public IActionResult Index()
        {
            long Id = 1325637489460908032;
            dynamic result =  _demoService.GetDemoById(Id).Result;
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
