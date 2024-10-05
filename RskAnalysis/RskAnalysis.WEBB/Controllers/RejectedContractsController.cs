using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RskAnalysis.CORE.Models;
using RskAnalysis.DATA;

namespace RskAnalysis.WEBB.Controllers
{
    public class RejectedContractsController : Controller
    {
        private readonly AppDbContext _context;

        public RejectedContractsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: RejectedContracts
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.RejectedContracts.Include(r => r.Partner);
            return View(await appDbContext.ToListAsync());
        }

        // GET: RejectedContracts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RejectedContracts == null)
            {
                return NotFound();
            }

            var rejectedContracts = await _context.RejectedContracts
                .Include(r => r.Partner)
                .FirstOrDefaultAsync(m => m.RejectedContractId == id);
            if (rejectedContracts == null)
            {
                return NotFound();
            }

            return View(rejectedContracts);
        }

        // GET: RejectedContracts/Create
        public IActionResult Create()
        {
            ViewData["BusinessId"] = new SelectList(_context.Businesses, "BusinessId", "BusinessDescription");
            ViewData["PartnerId"] = new SelectList(_context.Partners, "PartnerId", "ContactEMail");
            return View();
        }

        // POST: RejectedContracts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RejectedContractId,Amount,BusinessId,PartnerId,ContractName,StartDate,EndDate,CreatedDate")] RejectedContracts rejectedContracts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rejectedContracts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BusinessId"] = new SelectList(_context.Businesses, "BusinessId", "BusinessDescription", rejectedContracts.PartnerId);
            ViewData["PartnerId"] = new SelectList(_context.Partners, "PartnerId", "ContactEMail", rejectedContracts.PartnerId);
            return View(rejectedContracts);
        }

        // GET: RejectedContracts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RejectedContracts == null)
            {
                return NotFound();
            }

            var rejectedContracts = await _context.RejectedContracts.FindAsync(id);
            if (rejectedContracts == null)
            {
                return NotFound();
            }
            ViewData["BusinessId"] = new SelectList(_context.Businesses, "BusinessId", "BusinessDescription", rejectedContracts.PartnerId);
            ViewData["PartnerId"] = new SelectList(_context.Partners, "PartnerId", "ContactEMail", rejectedContracts.PartnerId);
            return View(rejectedContracts);
        }

        // POST: RejectedContracts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RejectedContractId,Amount,BusinessId,PartnerId,ContractName,StartDate,EndDate,CreatedDate")] RejectedContracts rejectedContracts)
        {
            if (id != rejectedContracts.RejectedContractId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rejectedContracts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RejectedContractsExists(rejectedContracts.RejectedContractId))
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
            ViewData["BusinessId"] = new SelectList(_context.Businesses, "BusinessId", "BusinessDescription", rejectedContracts.PartnerId);
            ViewData["PartnerId"] = new SelectList(_context.Partners, "PartnerId", "ContactEMail", rejectedContracts.PartnerId);
            return View(rejectedContracts);
        }

        // GET: RejectedContracts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RejectedContracts == null)
            {
                return NotFound();
            }

            var rejectedContracts = await _context.RejectedContracts
                
                .Include(r => r.Partner)
                .FirstOrDefaultAsync(m => m.RejectedContractId == id);
            if (rejectedContracts == null)
            {
                return NotFound();
            }

            return View(rejectedContracts);
        }

        // POST: RejectedContracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RejectedContracts == null)
            {
                return Problem("Entity set 'AppDbContext.RejectedContracts'  is null.");
            }
            var rejectedContracts = await _context.RejectedContracts.FindAsync(id);
            if (rejectedContracts != null)
            {
                _context.RejectedContracts.Remove(rejectedContracts);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RejectedContractsExists(int id)
        {
          return (_context.RejectedContracts?.Any(e => e.RejectedContractId == id)).GetValueOrDefault();
        }
    }
}
