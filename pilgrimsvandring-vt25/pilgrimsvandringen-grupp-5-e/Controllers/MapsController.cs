using Microsoft.AspNetCore.Mvc;

namespace pilgrimsvandringen_grupp_5_e.Controllers 
{
    public class MapsController : Controller 
    {
        public IActionResult DefaultMap() 
        {
            return View(); 
        }
    } 
}
