using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project3.Models;

namespace Project3.Controllers
{
    [Authorize(Roles = "ADMIN")]   
    
    public class AdminsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> InsurancePlans()
        {
            var insurancePlans = await _context.InsurancePlans.Include(i => i.InsuranceType).ToListAsync();
            return insurancePlans != null ?
                          View(insurancePlans) :
                          Problem("Entity set 'ApplicationDbContext.InsurancePlans'  is null.");
        }
        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return users != null ? 
                View(users) :
                Problem("Entity set 'ApplicationDbContext.Users'  is null.");
        }
        public async Task<IActionResult> Policies()
        {
            var policies = await _context.Policies.Include(p => p.InsurancePlan).Include(p => p.User).OrderByDescending(p => p.UpdatedDate).ToListAsync();
            return policies != null ?
                          View(policies) :
                          Problem("Entity set 'ApplicationDbContext.Policies'  is null.");
        }
            }
}
