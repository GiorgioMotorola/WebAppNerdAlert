using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using WebAppNerdAlert.Data;
using WebAppNerdAlert.Interfaces;
using WebAppNerdAlert.Models;

namespace WebAppNerdAlert.Repository
{
    public class HobbyRepository : IHobbyRepository
    {
        private readonly ApplicationDbContext _context;

        public HobbyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Hobby hobbies)
        {
            _context.Add(hobbies);
            return Save();
        }

        public bool Delete(Hobby hobbies)
        {
            _context.Remove(hobbies);
            return Save();
        }

        public async Task<IEnumerable<Hobby>> GetAll()
        {
            return await _context.Hobbies.ToListAsync();
        }

        public async Task<Hobby> GetByIdAsync(int id)
        {
            return await _context.Hobbies.Include(i => i.Address).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Hobby> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Hobbies.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Hobby>> GetHobbyByCity(string city)
        {
            return await _context.Hobbies.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Hobby hobbies)
        {
            _context.Update(hobbies);
            return Save();
        }
    }
}
