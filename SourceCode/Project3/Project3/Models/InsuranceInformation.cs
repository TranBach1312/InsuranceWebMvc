using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project3.Models
{
    public class InsuranceInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public String? FullName { get; set; }
        public String? Email { get; set; }
        public String? PhoneNumber { get; set; }
        [MaxLength(100)]
        public String? PolicyHolderCId { get; set; }
        [MaxLength(100)]
        public String? PolicyHolderAddress { get; set; }
        [MaxLength(100)]
        public String? LicensePlateNumber { get; set; }
        [MaxLength(100)]
        public String? VehicleModel { get; set; }
        [MaxLength(100)]
        public String? VehicleYear { get; set; }
    }
}
