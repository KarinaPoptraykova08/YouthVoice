using Microsoft.AspNetCore.Mvc;

namespace YouthVoice.Controllers
{
    public class ModeratorController : Controller
    {
        public IActionResult ModeratorPage()
        {
            return View();
        }
    }
}
