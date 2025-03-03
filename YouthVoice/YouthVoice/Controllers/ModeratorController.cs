using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using YouthVoice.Data;
using YouthVoice.Models;

namespace YouthVoice.Controllers
{
    public class ModeratorController : Controller
    {
        // Forbit link copying
        private readonly YouthVoiceDbContext _youthvoiceDbContext;

        public ModeratorController(YouthVoiceDbContext youthvoiceDbContext)
        {
            _youthvoiceDbContext = youthvoiceDbContext;
        }

        public IActionResult CreateAnEvent()
        {
            return View();
        }

        public IActionResult ModeratorDashboard()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Event newEvent, IFormFile imageFile)
        {
            //if (imageFile != null && imageFile.Length > 0)
            //{
            //    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

            //    using (var stream = new FileStream(filePath, FileMode.Create))
            //    {
            //        await imageFile.CopyToAsync(stream);
            //    }
            //    newEvent.ImagePath = filePath;
            //}

            //_youthvoiceDbContext.Events.Add(newEvent);
            //await _youthvoiceDbContext.SaveChangesAsync();
            return RedirectToAction("Index", "Home");            
        }

        public IActionResult AddCity()
        {
            var city = new City { Name="Burgas" };
            _youthvoiceDbContext.Cities.Add(city);
            _youthvoiceDbContext.SaveChanges();

            return RedirectToAction("ModeratorPage");
        }

        public IActionResult FetchCities()
        {
            var cities = _youthvoiceDbContext.Cities.ToList();
            
            return RedirectToAction("ModeratorPage");
        }
    }
}
