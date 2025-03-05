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
        private readonly YouthVoiceDbContext _dbContext;
        private readonly SaveFileService _saveFileService;
        private readonly FetchDataFromDbService _fetchDataFromDbService;

        public ModeratorController(YouthVoiceDbContext youthvoiceDbContext)
        {
            _dbContext = youthvoiceDbContext;
            _saveFileService = new SaveFileService();
            _fetchDataFromDbService = new FetchDataFromDbService(youthvoiceDbContext);
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
        public async Task<IActionResult> CreateAnEvent(OrganisationEvent organisationEvent)
        {
            try
            {
                if (organisationEvent == null)
                {
                    ViewBag.ErrorMessage = "Невалидни входни данни.";
                    return View();
                }

                var organisation = _fetchDataFromDbService.GetOrganisationByOrganisationId(organisationEvent.OrganisationId);
                if (organisation == null)
                {
                    ViewBag.ErrorMessage = "Организацията не е намерена";
                    return View();
                }

                organisationEvent.Event.ImagePath = await _saveFileService.SaveFile(organisationEvent.Event.File, "images/Events/");

                //if (organisationEvent.Event.Address == null) organisationEvent.Event.Address = "empty";
                //if (organisationEvent.Event.VirtualMeetingLink == null) organisationEvent.Event.VirtualMeetingLink = "empty";

                _dbContext.Organisations.Attach(organisation);
                _dbContext.Events.Add(organisationEvent.Event);

                var newOrganisationEvent = new OrganisationEvent
                {
                    EventId = organisationEvent.EventId,
                    Event = organisationEvent.Event,

                    OrganisationId = organisation.OrganisationId,
                    Organisation = organisation
                };

                _dbContext.OrganisationEvent.Add(newOrganisationEvent);
                _dbContext.SaveChanges();

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Възникна грешка при създаване на събитето.";

                return View();
            }
        }
    }
}
