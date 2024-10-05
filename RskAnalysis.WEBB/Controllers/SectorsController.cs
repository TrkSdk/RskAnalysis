using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RskAnalysis.CORE.Models;
using RskAnalysis.DATA;
using RskAnalysis.WEBB.Services.SectorsSer;

namespace RskAnalysis.WEBB.Controllers
{
    public class SectorsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly SectorsWServices _sectorsWServices;

        public SectorsController(SectorsWServices sectorsWServices)
        {
            _sectorsWServices = sectorsWServices;
        }

        // GET: Sectors
        public async Task<IActionResult> Index()
        {
            var res = await _sectorsWServices.GetSectorsAsync() as IEnumerable<Sectors>;

            return View(res);
            //return _context.Sectors != null ? 
            //              View(await _context.Sectors.ToListAsync()) :
            //              Problem("Entity set 'AppDbContext.Sectors'  is null.");
        }

        // GET: Sectors/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var res = await _sectorsWServices.GetSectorById(id);

            if (res == null)
            {
                return NotFound();
            }

            return View(res);
        }

        // GET: Sectors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sectors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SectorId,SectorName,SectorDescription,CreatedDate")] Sectors sectors)
        {
            var res = await _sectorsWServices.AddSector(sectors);
            //return View(res);
            return RedirectToAction("Index");
        }

        // GET: Sectors/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var city = await _sectorsWServices.GetSectorById(id);


            return View(city);
        }

        // POST: Sectors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SectorId,SectorName,SectorDescription,CreatedDate")] Sectors sectors)
        {
            if (id != sectors.SectorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                var res = await _sectorsWServices.UpdateSector(sectors);
                return RedirectToAction(nameof(Index));
            }
            return View(sectors);
        }

        // GET: Sectors/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _sectorsWServices.GetSectorById(id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // POST: Sectors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Bind("SectorId", "SectorName", "CreatedDate", "SectorDescription")] Sectors sect)
        {
            _sectorsWServices.DeleteSector(sect);

            return RedirectToAction(nameof(Index));
        }

        private bool SectorsExists(int id)
        {
          return (_context.Sectors?.Any(e => e.SectorId == id)).GetValueOrDefault();
        }
    }
}
