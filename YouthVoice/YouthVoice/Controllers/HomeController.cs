using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using YouthVoice.Models;

namespace YouthVoice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult TermsAndConditions()
        {
            return View();
        }

        public IActionResult FAQ()
        {
            return View();
        }

        public async Task<IActionResult> SendFeedback(EmailService emailService)
        {
            try
            {
                if (emailService.FromEmail == string.Empty)
                {
                    emailService.FromEmail = User.FindFirst(ClaimTypes.Email)?.Value;
                }
                emailService.SendEmail(emailService.FromEmail, "youthvoice.burgas@gmail.com", emailService.Subject, emailService.Body);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Нещо се обърка. Имейлът не беше изпратен. Моля опитайте отново.";
            }

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
