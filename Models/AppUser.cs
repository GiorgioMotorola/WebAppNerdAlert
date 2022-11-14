using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace WebAppNerdAlert.Models
{
    public class AppUser
    {
        [Key]
        public string Id { get; set; }
        public int? TimeStart { get; set; }
        public int? TimeEnd { get; set; }
        public Address? Address { get; set; }
        public ICollection<Hobby> Hobbies { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}
