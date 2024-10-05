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
using RskAnalysis.WEBB.Services.CitiesSer;
using RskAnalysis.WEBB.Services.SectorsSer;

namespace RskAnalysis.WEBB.Controllers
{
    public class BusinessesController : Controller
    {

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

            return View(res);
        }

        // GET: Businesses/Details/5
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
            ViewData["SectorId"] = new SelectList(_sectorsWServices.GetSectorsAsync().Result, "SectorId", "SectorName");
            
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

            ViewData["SectorId"] = new SelectList(await _sectorsWServices.GetSectorsAsync(), "SectorId", "SectorName");
            
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

            ViewData["SectorId"] = new SelectList(secc ,"SectorId", "SectorName", res[0].Sector.SectorId);
            
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

            buss.BusinessId = businesses.BusinessId;
            buss.BusinessName = businesses.BusinessName;
            buss.BusinessDescription = businesses.BusinessDescription;
            buss.CreatedDate = businesses.CreatedDate;
            buss.RiskFactor = businesses.RiskFactor;

            var upt = await _businessesWServices.UpdateBusiness(buss);
            return RedirectToAction(nameof(Index));
        }

        // GET: Businesses/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buss = await _businessesWServices.GetBusinessByIdWithSector(id);
            if (buss == null )
            {
                return NotFound();
            }

            var sect = await _sectorsWServices.GetSectorById(buss[0].SectorId);

            buss[0].Sector.SectorId = sect.SectorId;
            buss[0].Sector.SectorName = sect.SectorName;
            buss[0].Sector.SectorDescription = sect.SectorDescription;
            buss[0].Sector.CreatedDate = sect.CreatedDate;

            return View(buss[0]);

        }

        // POST: Businesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Businesses buss)
        {

            _businessesWServices.DeleteBusiness(buss);

            return RedirectToAction(nameof(Index));
        }

    }
}
