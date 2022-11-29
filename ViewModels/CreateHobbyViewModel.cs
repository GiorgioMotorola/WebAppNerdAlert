using WebAppNerdAlert.Data.Enum;
using WebAppNerdAlert.Models;

namespace WebAppNerdAlert.ViewModels
{
    public class CreateHobbyViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Address Address { get; set; }
        public IFormFile Image { get; set; }
        public HobbyCategory HobbyCategory { get; set; }
        public string AppUserId { get; set; }
    }
}
