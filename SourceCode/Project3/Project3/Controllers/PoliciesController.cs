using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project3.Models;
using Project3.ViewModels;

namespace Project3.Controllers
{
    public class PoliciesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PoliciesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Policies
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Policies.Include(p => p.InsuranceInformation).Include(p => p.InsurancePlan).Include(p => p.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Policies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Policies == null)
            {
                return NotFound();
            }

            var policy = await _context.Policies
                .Include(p => p.InsuranceInformation)
                .Include(p => p.InsurancePlan)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (policy == null)
            {
                return NotFound();
            }

            return View(policy);
        }

        // GET: Policies/Create
        public IActionResult Create()
        {
            ViewData["InsuranceInformationId"] = new SelectList(_context.InsuranceInformations, "Id", "Id");
            ViewData["InsurancePlanId"] = new SelectList(_context.InsurancePlans, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Policies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Status,StartDate,EndDate,CreatedDate,UpdatedDate,UserId,InsurancePlanId,InsuranceInformationId")] Policy policy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(policy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InsuranceInformationId"] = new SelectList(_context.InsuranceInformations, "Id", "Id", policy.InsuranceInformationId);
            ViewData["InsurancePlanId"] = new SelectList(_context.InsurancePlans, "Id", "Id", policy.InsurancePlanId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", policy.UserId);
            return View(policy);
        }
        

        // GET: Policies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Policies == null)
            {
                return NotFound();
            }

            var policy = await _context.Policies.Include(p => p.InsurancePlan).Include(p => p.InsuranceInformation).Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id);
            if (policy == null)
            {
                return NotFound();
            }
            ViewData["InsuranceInformationId"] = new SelectList(_context.InsuranceInformations, "Id", "Id", policy.InsuranceInformationId);
            ViewData["InsurancePlanId"] = new SelectList(_context.InsurancePlans, "Id", "Name", policy.InsurancePlanId);
            return View(policy);
        }

        // POST: Policies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Policy policy)
        {
            if (id != policy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    policy.UpdatedDate = DateTime.Now;
                    _context.Update(policy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolicyExists(policy.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Policies", "Admins");
            }
            ViewData["InsuranceInformationId"] = new SelectList(_context.InsuranceInformations, "Id", "Id", policy.InsuranceInformationId);
            ViewData["InsurancePlanId"] = new SelectList(_context.InsurancePlans, "Id", "Name", policy.InsurancePlanId);
            return View(policy);
        }

        // GET: Policies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Policies == null)
            {
                return NotFound();
            }

            var policy = await _context.Policies
                .Include(p => p.InsuranceInformation)
                .Include(p => p.InsurancePlan)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (policy == null)
            {
                return NotFound();
            }

            return View(policy);
        }

        // POST: Policies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Policies == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Policies'  is null.");
            }
            var policy = await _context.Policies.FindAsync(id);
            if (policy != null)
            {
                _context.Policies.Remove(policy);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PolicyExists(int id)
        {
          return (_context.Policies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
