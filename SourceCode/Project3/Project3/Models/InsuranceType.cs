using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project3.Models
{
    public class InsuranceType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(100)]
        public String? Name { get; set; }
        [Column(TypeName = "text")]
        public String? ImageUrl { get; set; }
        public String? IconUrl { get; set; }
        public String? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        
        public List<InsurancePlan>? InsurancePlans { get; set; }
    }
}
