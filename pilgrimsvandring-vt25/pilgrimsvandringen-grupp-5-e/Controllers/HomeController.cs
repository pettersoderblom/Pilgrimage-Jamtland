using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using pilgrimsvandringen_grupp_5_e.Models;
using pilgrimsvandringen_grupp_5_e.Services;
using pilgrimsvandringen_grupp_5_e.Services.Interfaces;

namespace pilgrimsvandringen_grupp_5_e.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITrailInformationService _trailInformationService;

        public HomeController(ILogger<HomeController> logger, ITrailInformationService trailInformationService)
        {
            _logger = logger;
            _trailInformationService = trailInformationService;
        }

        public async Task<IActionResult> Index()
        {
            var trails = await _trailInformationService.GetAllTrails();
            ViewBag.Trails = trails;
            return View();
        }        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}
