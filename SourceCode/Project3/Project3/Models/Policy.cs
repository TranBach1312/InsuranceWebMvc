using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project3.Models
{
    public class Policy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(100)]
        public String? Name { get; set; }
        [Column(TypeName = "text")]
        public String? Description { get; set; }
        public bool Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
        public int InsurancePlanId { get; set; }
        [ForeignKey("InsurancePlanId")]
        public InsurancePlan? InsurancePlan { get; set; }
        public int InsuranceInformationId { get; set; }
        [ForeignKey("InsuranceInformationId")]
        public InsuranceInformation? InsuranceInformation { get; set; }
    }
}
