using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sportify_back.Models;
using Sportify_solution_app.Models;

namespace Sportify_Back.Controllers
{
    public class LicenciesController : Controller
    {
        private readonly LicenciesContext _context;

        public LicenciesController(LicenciesContext context)
        {
            _context = context;
        }

        // GET: Licencies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Licenses.ToListAsync());
        }

        // GET: Licencies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var licenses = await _context.Licenses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (licenses == null)
            {
                return NotFound();
            }

            return View(licenses);
        }

        // GET: Licencies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Licencies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Active")] Licenses licenses)
        {
            if (ModelState.IsValid)
            {
                _context.Add(licenses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(licenses);
        }

        // GET: Licencies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var licenses = await _context.Licenses.FindAsync(id);
            if (licenses == null)
            {
                return NotFound();
            }
            return View(licenses);
        }

        // POST: Licencies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Active")] Licenses licenses)
        {
            if (id != licenses.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(licenses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LicensesExists(licenses.Id))
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
            return View(licenses);
        }

        // GET: Licencies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var licenses = await _context.Licenses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (licenses == null)
            {
                return NotFound();
            }

            return View(licenses);
        }

        // POST: Licencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var licenses = await _context.Licenses.FindAsync(id);
            if (licenses != null)
            {
                _context.Licenses.Remove(licenses);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LicensesExists(int id)
        {
            return _context.Licenses.Any(e => e.Id == id);
        }
    }
}
