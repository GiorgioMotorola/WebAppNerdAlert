using WebAppNerdAlert.Models;

namespace WebAppNerdAlert.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Event>> GetAllUserEvents();
        Task<List<Hobby>> GetAllUserHobbies();
        
    }
}
