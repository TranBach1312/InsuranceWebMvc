using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project3.Models
{
    public enum TermType
    {
        Monthly,
        Quarterly,
        HalfYearly,
        Yearly
    }
    public class InsurancePlan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(100)]
        public String? Name { get; set; }
        [Column(TypeName = "text")]
        public String? Description { get; set; }
        public decimal Premium { get; set; }
        [EnumDataType(typeof(TermType))]
        public TermType TermType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int InsuranceTypeId { get; set; }
        [ForeignKey(nameof(InsuranceTypeId))]
        public InsuranceType? InsuranceType { get; set; }
        public List<Policy>? Policies { get; set; }
    }
}
