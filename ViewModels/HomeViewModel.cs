using WebAppNerdAlert.Models;

namespace WebAppNerdAlert.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Hobby> Hobbies { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
