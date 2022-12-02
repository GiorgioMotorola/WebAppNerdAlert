using WebAppNerdAlert.Models;

namespace WebAppNerdAlert.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Event>> GetAllUserEvents();
        Task<List<Hobby>> GetAllUserHobbies();

        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetByIdNoTracking(string id);

        bool Update(AppUser user);
        bool Save();
    }
}
