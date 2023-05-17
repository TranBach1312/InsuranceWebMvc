using System.ComponentModel.DataAnnotations;

namespace Project3.ViewModels
{
    public class ContactInfo
    {
        public String? Fullname { get; set; }
        [EmailAddress]
        public String? Email { get; set; }
        [Phone]
        public String? PhoneNumber { get; set; }
    }
}
