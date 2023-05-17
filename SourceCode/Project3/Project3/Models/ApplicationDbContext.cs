using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Project3.Models
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<InsuranceType> InsuranceTypes { get; set; }
        public DbSet<InsurancePlan> InsurancePlans { get; set; }
        public DbSet<InsuranceInformation> InsuranceInformations { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<Loan> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<InsurancePlan>()
                .Property(e => e.TermType)
                .HasConversion(v => v.ToString(),
                v => (TermType)Enum.Parse(typeof(TermType), v))
                .HasMaxLength(20);
            builder.Entity<User>()
                .Property(e => e.Gender)
                .HasConversion(v => v.ToString(),
                v => (Gender)Enum.Parse(typeof(Gender), v))
                .HasMaxLength(20);
            builder.Entity<User>()
                .Property(u => u.LockoutEnd)
                .HasDefaultValue(DateTimeOffset.MaxValue);
            builder.Entity<Loan>()
                .Property(e => e.Status)
                .HasConversion(v => v.ToString(),
                v => (Status)Enum.Parse(typeof(Status), v))
                .HasMaxLength(20);
        }
    }
}
