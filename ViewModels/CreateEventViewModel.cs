using WebAppNerdAlert.Data.Enum;
using WebAppNerdAlert.Models;

namespace WebAppNerdAlert.ViewModels
{
    public class CreateEventViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Address Address { get; set; }
        public DateTime? StartTime { get; set; }
        public int? EntryFee { get; set; }
        public string? Website { get; set; }
        public string? Twitter { get; set; }
        public string? Facebook { get; set; }
        public string? Contact { get; set; }
        public IFormFile Image { get; set; }
        public EventCategory EventCategory { get; set; }
        public string AppUserId { get; set; }
    }
}
