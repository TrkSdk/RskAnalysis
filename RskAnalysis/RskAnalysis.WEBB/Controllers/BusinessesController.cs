using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.WebEncoders.Testing;
using RskAnalysis.API.DTOs;
using RskAnalysis.CORE.Models;
using RskAnalysis.DATA;
using RskAnalysis.WEBB.Services.BusinessesSer;
using RskAnalysis.WEBB.Services.SectorsSer;

namespace RskAnalysis.WEBB.Controllers
{
    public class BusinessesController : Controller
    {
        private readonly AppDbContext _context;

        private readonly BusinessesWServices _businessesWServices;
        private readonly SectorsWServices _sectorsWServices;
        


        public BusinessesController(BusinessesWServices businessesWServices, SectorsWServices sectorsWServices)
        {
            
            _businessesWServices = businessesWServices;
            _sectorsWServices = sectorsWServices;
            
        }

        // GET: Businesses with sectors
        public async Task<IActionResult> Index()
        {
            var res = await _businessesWServices.GetBusinessWithSect() as IEnumerable<Businesses>;

            var rrr = res.ToList();

            return View(res);
            //var appDbContext = _context.Businesses.Include(b => b.Sector);
            //return View(await appDbContext.ToListAsync());
        }

        // GET: Businesses/Details/5+
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var res = await _businessesWServices.GetBusinessById(id);

            if (res == null)
            {
                return NotFound();
            }

            return View(res);
        }

        // GET: Businesses/Create
        public IActionResult Create()
        {
            ViewData["SectorId"] = new SelectList(_sectorsWServices.GetSectorsAsync().Result, "SectorId", "SectorDescription");
            return View();
        }

        // POST: Businesses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Businesses businesses)
        {
            //[Bind("BusinessId,SectorId,BusinessName,BusinessDescription,RiskFactor,CreatedDate,Sector")]
            //businesses.Sector = null;
            ModelState.Remove("Sector.SectorName");
            ModelState.Remove("Sector.SectorDescription");

            if (ModelState.IsValid)
            {
                var res = await _businessesWServices.AddBusiness(businesses);
                return RedirectToAction(nameof(Index));
            }

            ViewData["SectorId"] = new SelectList(await _sectorsWServices.GetSectorsAsync(), "SectorId", "SectorDescription");
            return View(businesses);
        }

        // GET: Businesses/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var res = await _businessesWServices.GetBusinessByIdWithSector(id);
            if (res == null || res.Count == 0)
            {
                return NotFound();
            }

            var secc = new List<Sectors> { res[0].Sector };

            ViewData["SectorId"] = new SelectList(secc ,"SectorId", "SectorDescription", res[0].Sector.SectorId);
            
            return View(res[0]);
        }

        // POST: Businesses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Businesses businesses)
        {
            if (id != businesses.BusinessId)
            {
                return NotFound();
            }

            var buss = await _businessesWServices.GetBusinessById(businesses.BusinessId);

            buss.Sector = null;
            buss.BusinessName = businesses.BusinessName;
            buss.BusinessDescription=businesses.BusinessDescription;
            buss.CreatedDate=businesses.CreatedDate;
            buss.RiskFactor=businesses.RiskFactor;
            buss.BusinessId=businesses.BusinessId;

            var upt = await _businessesWServices.UpdateBusiness(buss);
            return RedirectToAction(nameof(Index));
            //if (ModelState.IsValid)
            //{
            //    var upt = await _businessesWServices.UpdateBusiness(businesses);
            //    return RedirectToAction(nameof(Index));
            //}
            //else
            //{
            //    var res = await _businessesWServices.GetBusinessByIdWithSector(id);
            //    var secc = new List<Sectors> { res[0].Sector };

            //    ViewData["SectorId"] = new SelectList(secc, "SectorId", "SectorDescription", res[0].Sector.SectorId);
            //    return View(businesses);
            //}

        }

        // GET: Businesses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Businesses == null)
            {
                return NotFound();
            }

            var businesses = await _context.Businesses
                .Include(b => b.Sector)
                .FirstOrDefaultAsync(m => m.BusinessId == id);
            if (businesses == null)
            {
                return NotFound();
            }

            return View(businesses);
        }

        // POST: Businesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Businesses == null)
            {
                return Problem("Entity set 'AppDbContext.Businesses'  is null.");
            }
            var businesses = await _context.Businesses.FindAsync(id);
            if (businesses != null)
            {
                _context.Businesses.Remove(businesses);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusinessesExists(int id)
        {
          return (_context.Businesses?.Any(e => e.BusinessId == id)).GetValueOrDefault();
        }
    }
}
