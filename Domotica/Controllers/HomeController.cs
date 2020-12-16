using Domotica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Domotica.Data;

namespace Domotica.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly DomoticaContext _context;

        public HomeController(ILogger<HomeController> logger, DomoticaContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Dashboard()
        {
            DeviceData.Instance.Setup(_context, User.FindFirstValue(ClaimTypes.NameIdentifier));

            ViewBag.Devices = DeviceData.Instance.DeviceList;
            return View();
        }

        [Authorize]
        public IActionResult AddDevice()
        {
            return View();
        }

        public IActionResult AddNewDevice(DeviceModel device)
        {
            //Add new device to database
            new Database(_context).AddDevice(User.FindFirstValue(ClaimTypes.NameIdentifier), device);
            DeviceData.Instance.AddNewDevice(device);

            return Redirect("/Home/Dashboard");
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult OurProducts()
        {
            return View();
        }

        public IActionResult Contact()
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
