using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project3.Models
{
    public enum Gender 
    {
        MALE, FEMALE, OTHER
    }
    public class User : IdentityUser<int>
    {
        
        [MaxLength(100)]
        public String? Fullname { get; set; }
        [MaxLength(100)]
        public String? Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }
        
        public List<Role>? Roles { get; set; }
        
        public List<Policy>? Policies { get; set; }
        
        public List<Loan>? Loans { get; set; }

    }
}
