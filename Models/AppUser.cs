using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppNerdAlert.Models
{
    public class AppUser : IdentityUser
    {
        public int? TimeStart { get; set; }
        public int? TimeEnd { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        [ForeignKey("Address")]
        public int? AddressId{ get; set; }
        public Address? Address { get; set; }
        public ICollection<Hobby>? Hobbies { get; set; }
        public ICollection<Event>? Events { get; set; }
    }
}
