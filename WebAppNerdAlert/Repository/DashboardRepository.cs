using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
            var currentUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userEvents = _context.Events.Where(r => r.AppUser.Id == currentUser);
            return userEvents.ToList();
        }

        public async Task<List<Hobby>> GetAllUserHobbies()
        {
            var currentUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userHobbies = _context.Hobbies.Where(r => r.AppUser.Id == currentUser);
            return userHobbies.ToList();
        }

        public async Task<AppUser> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetByIdNoTracking(string id)
        {
            return await _context.Users.Where(u => u.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public bool Update(AppUser user)
        {
            _context.Users.Update(user);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
