using Microsoft.AspNetCore.Mvc;

namespace YouthVoice.Controllers
{
    public class ModeratorController : Controller
    {
        // Forbit link copying
        public IActionResult ModeratorPage()
        {
            return View();
        }
    }
}
