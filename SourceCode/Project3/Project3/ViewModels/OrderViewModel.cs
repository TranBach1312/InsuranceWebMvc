using Project3.Models;

namespace Project3.ViewModels
{
    public class OrderViewModel
    {
        public int InsuranceTypeId { get; set; }
        public InsuranceType? InsuranceType { get; set; }
        public int? InsurancePlanId { get; set; }
        public String? FullName { get; set; }
        public String? Email { get; set; }
        public String? PhoneNumber { get; set; }
        public String? Address { get; set; }
    }
}
