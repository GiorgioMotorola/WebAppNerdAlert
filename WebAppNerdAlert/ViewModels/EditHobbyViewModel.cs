using WebAppNerdAlert.Data.Enum;
using WebAppNerdAlert.Models;

namespace WebAppNerdAlert.ViewModels
{
    public class EditHobbyViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
        public string? Url { get; set; }
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        public HobbyCategory? HobbyCategory { get; set; }
    }
}
