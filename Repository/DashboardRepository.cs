using WebAppNerdAlert.Data;
using WebAppNerdAlert.Interfaces;
using WebAppNerdAlert.Models;

namespace WebAppNerdAlert.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Event>> GetAllUserEvents()
        {
            var currentUser = _httpContextAccessor.HttpContext?.User;
            var userEvents = _context.Events.Where(r => r.AppUser.Id == currentUser.ToString());
            return userEvents.ToList();
        }

        public async Task<List<Hobby>> GetAllUserHobbies()
        {
            var currentUser = _httpContextAccessor.HttpContext?.User;
            var userHobbies = _context.Hobbies.Where(r => r.AppUser.Id == currentUser.ToString());
            return userHobbies.ToList();
        }
    }
}
