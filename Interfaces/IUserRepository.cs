using WebAppNerdAlert.Models;

namespace WebAppNerdAlert.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetAllUsers();
        Task<AppUser> GetUserById(string Id);
        bool Add(AppUser user);
        bool Update(AppUser user);  
        bool Delete(AppUser user);
        bool Save();
    }
}
