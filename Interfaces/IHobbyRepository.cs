using WebAppNerdAlert.Models;

namespace WebAppNerdAlert.Interfaces
{
    public interface IHobbyRepository
    {
        Task<IEnumerable<Hobby>> GetAll();
        Task<Hobby> GetByIdAsync(int id);
        Task<Hobby> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Hobby>> GetHobbyByCity(string city);
        bool Add(Hobby hobbies);
        bool Update(Hobby hobbies);
        bool Delete(Hobby hobbies);
        bool Save();
    }
}
