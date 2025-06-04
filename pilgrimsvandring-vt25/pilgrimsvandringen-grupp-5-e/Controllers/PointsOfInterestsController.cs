using Microsoft.AspNetCore.Mvc;
using pilgrimsvandringen_grupp_5_e.Services;
using pilgrimsvandringen_grupp_5_e.Services.Interfaces;


namespace pilgrimsvandringen_grupp_5_e.Controllers
{
    public class PointsOfInterestsController : Controller
    {
        private readonly IPointOfinterestService _pointOfinterestService;

        public PointsOfInterestsController(IPointOfinterestService pointOfinterestService)
        {
            this._pointOfinterestService = pointOfinterestService;
        }        

        public async Task<IActionResult> InformationPointOfInterest(int id)
        {
            var model = await _pointOfinterestService.GetPointOfInterest(id);
            return View("~/Views/Informations/InformationPointOfInterest.cshtml", model); // TODO develop a structured view for pointsofinterest
        }

        public async Task<IActionResult> GetAll()
        {
            var points = await _pointOfinterestService.GetAllPointsOfInterests();
            return Json(points);

        }
    }
}


