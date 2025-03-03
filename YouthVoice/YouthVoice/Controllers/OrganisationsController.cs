using Microsoft.AspNetCore.Mvc;

namespace YouthVoice.Controllers
{
    public class OrganisationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Organisations()
        {
            return View();
        }

        public IActionResult Events()
        {
            return View();
        }
    }
}
