using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YouthVoice.Data;
using YouthVoice.Models;

namespace YouthVoice.Controllers
{
    public class OrganisationsController : Controller
    {
        private YouthVoiceDbContext _dbContext;
        private FetchDataFromDbService _fetchDataFromDbService;

        public OrganisationsController(YouthVoiceDbContext dbContext)
        {
            _dbContext = dbContext;
            _fetchDataFromDbService = new FetchDataFromDbService(dbContext);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Organisations()
        {
            // Fetching data about the organisations from the db
            var fetchOrganisations = _dbContext.Organisations.ToList();            

            // Using the CityId to fetch data for its name
            foreach (var org in fetchOrganisations)
            {
                if (org.City == null)
                {
                    org.City = new City();
                }
                org.City.Name = _fetchDataFromDbService.GetCityById(org.CityId);
            }

            if (fetchOrganisations == null) return NotFound();            

            return View(fetchOrganisations);            
        }

        public IActionResult OrganisationDetails(int id)
        {
            var fetchOrganisation = _dbContext.Organisations
                .Include(o => o.OrganisationEvents)
                .ThenInclude(oe => oe.Event)
                .FirstOrDefault(o => o.OrganisationId == id);

            if (fetchOrganisation == null) return ViewBag.NotFound = "Организацията все още няма събития.";

            fetchOrganisation.City = new City();
            fetchOrganisation.City.Name = _fetchDataFromDbService.GetCityById(fetchOrganisation.CityId);                        

            return View(fetchOrganisation);
        }

        public IActionResult Events()
        {
            return View();
        }

        public IActionResult EventDetails(int id)
        {
            var fetchEvent = _dbContext.Events
                .Include(o => o.OrganisationEvents)
                .ThenInclude(oe => oe.Organisation)
                .FirstOrDefault(e => e.EventId == id);

            if (fetchEvent == null) return NotFound();

            return View(fetchEvent);
        }
    }
}
