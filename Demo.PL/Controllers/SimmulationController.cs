using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Demo.PL.Controllers
{
    public class SimmulationController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
