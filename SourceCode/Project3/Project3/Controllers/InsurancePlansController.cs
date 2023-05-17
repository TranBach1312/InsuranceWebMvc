using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project3.Models;

namespace Project3.Controllers
{
    public class InsurancePlansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InsurancePlansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InsurancePlans
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InsurancePlans.Include(i => i.InsuranceType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InsurancePlans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.InsurancePlans == null)
            {
                return NotFound();
            }

            var insurancePlan = await _context.InsurancePlans
                .Include(i => i.InsuranceType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insurancePlan == null)
            {
                return NotFound();
            }

            return View(insurancePlan);
        }

        // GET: InsurancePlans/Create
        public IActionResult Create()
        {
            ViewData["InsuranceTypeId"] = new SelectList(_context.InsuranceTypes, "Id", "Name");
            return View();
        }

        // POST: InsurancePlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Premium,TermType,CreatedDate,UpdatedDate,InsuranceTypeId")] InsurancePlan insurancePlan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insurancePlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InsuranceTypeId"] = new SelectList(_context.InsuranceTypes, "Id", "Name", insurancePlan.InsuranceTypeId);
            return View(insurancePlan);
        }

        // GET: InsurancePlans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InsurancePlans == null)
            {
                return NotFound();
            }

            var insurancePlan = await _context.InsurancePlans.FindAsync(id);
            if (insurancePlan == null)
            {
                return NotFound();
            }
            ViewData["InsuranceTypeId"] = new SelectList(_context.InsuranceTypes, "Id", "Name", insurancePlan.InsuranceTypeId);
            return View(insurancePlan);
        }

        // POST: InsurancePlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Premium,TermType,CreatedDate,UpdatedDate,InsuranceTypeId")] InsurancePlan insurancePlan)
        {
            if (id != insurancePlan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    insurancePlan.UpdatedDate = DateTime.Now;
                    _context.Update(insurancePlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsurancePlanExists(insurancePlan.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("InsurancePlans", "Admins");
            }
            ViewData["InsuranceTypeId"] = new SelectList(_context.InsuranceTypes, "Id", "Name", insurancePlan.InsuranceTypeId);
            return View(insurancePlan);
        }

        // GET: InsurancePlans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InsurancePlans == null)
            {
                return NotFound();
            }

            var insurancePlan = await _context.InsurancePlans
                .Include(i => i.InsuranceType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insurancePlan == null)
            {
                return NotFound();
            }

            return View(insurancePlan);
        }

        // POST: InsurancePlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InsurancePlans == null)
            {
                return Problem("Entity set 'ApplicationDbContext.InsurancePlans'  is null.");
            }
            var insurancePlan = await _context.InsurancePlans.FindAsync(id);
            if (insurancePlan != null)
            {
                _context.InsurancePlans.Remove(insurancePlan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsurancePlanExists(int id)
        {
          return (_context.InsurancePlans?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
