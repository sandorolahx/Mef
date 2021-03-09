using System.Diagnostics;

using Mef.WebClient.Models;
using Mef.WebClient.Providers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mef.WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPrinterProvider _printerProvider;

        public HomeController(ILogger<HomeController> logger, IPrinterProvider printerProvider)
        {
            _logger = logger;
            _printerProvider = printerProvider;
        }

        public IActionResult Index()
        {
            var result = _printerProvider.GetPrinter().Print();

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
