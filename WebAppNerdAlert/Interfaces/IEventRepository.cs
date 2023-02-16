using System.Diagnostics;
using WebAppNerdAlert.Models;

namespace WebAppNerdAlert.Interfaces
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAll();
        Task<Event> GetByIdAsync(int id);
        Task<Event> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Event>> GetAllEventsByCity(string city);
        bool Add(Event events);
        bool Update(Event events);
        bool Delete(Event events);
        bool Save();
    }
}
