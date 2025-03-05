using YouthVoice.Data;

namespace YouthVoice.Models
{
    public class FetchDataFromDbService
    {
        private readonly YouthVoiceDbContext _dbContext;

        public FetchDataFromDbService(YouthVoiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string GetCityById(int id)
        {
            var city = _dbContext.Cities
                .FirstOrDefault(c => c.CityId == id);

            return city?.Name ?? "Unknown City";
        }

        public List<Event> GetEventByOrganisationId(int id)
        {
            var events = _dbContext.OrganisationEvent
                .Where(oe => oe.OrganisationId == id)
                .Select(oe => oe.Event)
                .ToList();

            return events;
        }

        public Organisation GetOrganisationByOrganisationId(int id)
        {
            Organisation org = _dbContext.Organisations
                .FirstOrDefault(o => o.OrganisationId == id);

            return org;
        }
    }
}
