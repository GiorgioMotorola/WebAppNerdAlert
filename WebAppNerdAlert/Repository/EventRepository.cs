using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebAppNerdAlert.Data;
using WebAppNerdAlert.Interfaces;
using WebAppNerdAlert.Models;

namespace WebAppNerdAlert.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;

        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Event events)
        {
            _context.Add(events);
            return Save();
        }

        public bool Delete(Event events)
        {
            _context.Remove(events);
            return Save();
        }

        public async Task<IEnumerable<Event>> GetAll()
        {
           return await _context.Events.ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetAllEventsByCity(string city)
        {
            return await _context.Events.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public async Task<Event> GetByIdAsync(int id)
        {
            return await _context.Events.Include(i => i.Address).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Event> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Events.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Event events)
        {
            _context.Update(events);
            return Save();
        }
    }
}
