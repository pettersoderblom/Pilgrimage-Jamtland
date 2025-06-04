using Microsoft.AspNetCore.Mvc;

namespace pilgrimsvandringen_grupp_5_e.Controllers
{
    public class InformationsController : Controller
    {        

        public IActionResult InformationsSummary()
        {
            return View();
        }

    }
}
