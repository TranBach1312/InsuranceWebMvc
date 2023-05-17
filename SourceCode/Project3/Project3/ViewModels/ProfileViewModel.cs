using Project3.Models;

namespace Project3.ViewModels
{
    public class ProfileViewModel
    {
        public string FullName { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
    }
}
