using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Search;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project3.Models;
using Project3.ViewModels;

namespace Project3.Controllers
{
    public class InsuranceTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _usermanager;
        public InsuranceTypesController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _usermanager = userManager;
        }

        // GET: InsuranceTypes
        public async Task<IActionResult> Index()
        {
              return _context.InsuranceTypes != null ? 
                          View(await _context.InsuranceTypes.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.InsuranceTypes'  is null.");
        }

        // GET: InsuranceTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.InsuranceTypes == null)
            {
                return NotFound();
            }

            var insuranceType = await _context.InsuranceTypes
                .Include(i => i.InsurancePlans)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (insuranceType == null)
            {
                return NotFound();
            }
            OrderViewModel? model = new OrderViewModel();
            
            model.InsuranceType = insuranceType;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrder(OrderViewModel order)
        {

            if (ModelState.IsValid)
            {
                InsuranceInformation insuranceInformation = new InsuranceInformation();
                insuranceInformation.FullName = order.FullName;
                insuranceInformation.Email = order.Email;
                insuranceInformation.PhoneNumber = order.PhoneNumber;
                insuranceInformation.PolicyHolderAddress = order.Address;
                Policy policy = new Policy();
                policy.InsuranceInformation = insuranceInformation;
                policy.InsurancePlanId = order.InsurancePlanId ?? 1;
                if(string.IsNullOrEmpty(_usermanager.GetUserId(User)))
                {
                    return RedirectToAction("Login", "Auths", new { returnUrl = Url.Action("Details", "InsuranceTypes") });

                }
                policy.UserId = int.Parse(_usermanager.GetUserId(User));
                policy.CreatedDate = DateTime.Now;
                policy.UpdatedDate = DateTime.Now;
                policy.StartDate = DateTime.Now;
                policy.EndDate = DateTime.Now;
                _context.Add(policy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Details", new { id = order.InsuranceTypeId });
        }
        // GET: InsuranceTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InsuranceTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ImageUrl,IconUrl,Description,CreatedDate,UpdatedDate")] InsuranceType insuranceType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insuranceType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(insuranceType);
        }

        // GET: InsuranceTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InsuranceTypes == null)
            {
                return NotFound();
            }

            var insuranceType = await _context.InsuranceTypes.FindAsync(id);
            if (insuranceType == null)
            {
                return NotFound();
            }
            return View(insuranceType);
        }

        // POST: InsuranceTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ImageUrl,IconUrl,Description,CreatedDate,UpdatedDate")] InsuranceType insuranceType)
        {
            if (id != insuranceType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insuranceType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceTypeExists(insuranceType.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(insuranceType);
        }

        // GET: InsuranceTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InsuranceTypes == null)
            {
                return NotFound();
            }

            var insuranceType = await _context.InsuranceTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insuranceType == null)
            {
                return NotFound();
            }

            return View(insuranceType);
        }

        // POST: InsuranceTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InsuranceTypes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.InsuranceTypes'  is null.");
            }
            var insuranceType = await _context.InsuranceTypes.FindAsync(id);
            if (insuranceType != null)
            {
                _context.InsuranceTypes.Remove(insuranceType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsuranceTypeExists(int id)
        {
          return (_context.InsuranceTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
