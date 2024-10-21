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
    public class ProgrammingController : Controller
    {
        private readonly ProgrammingContext _context;

        public ProgrammingController(ProgrammingContext context)
        {
            _context = context;
        }

        // GET: Programming
        public async Task<IActionResult> Index()
        {
            return View(await _context.Programmings.ToListAsync());
        }

        // GET: Programming/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programmings = await _context.Programmings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (programmings == null)
            {
                return NotFound();
            }

            return View(programmings);
        }

        // GET: Programming/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Programming/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Active")] Programmings programmings)
        {
            if (ModelState.IsValid)
            {
                _context.Add(programmings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(programmings);
        }

        // GET: Programming/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programmings = await _context.Programmings.FindAsync(id);
            if (programmings == null)
            {
                return NotFound();
            }
            return View(programmings);
        }

        // POST: Programming/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Active")] Programmings programmings)
        {
            if (id != programmings.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(programmings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgrammingsExists(programmings.Id))
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
            return View(programmings);
        }

        // GET: Programming/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programmings = await _context.Programmings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (programmings == null)
            {
                return NotFound();
            }

            return View(programmings);
        }

        // POST: Programming/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var programmings = await _context.Programmings.FindAsync(id);
            if (programmings != null)
            {
                _context.Programmings.Remove(programmings);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProgrammingsExists(int id)
        {
            return _context.Programmings.Any(e => e.Id == id);
        }
    }
}
