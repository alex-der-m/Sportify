using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sportify_back.Models;

namespace Sportify_Back.Controllers
{
    public class UsersController : Controller
    {
        private readonly SportifyDbContext _context;    

        public UsersController(SportifyDbContext context)
        {
            _context = context;
        }
        
        [Authorize(Policy = "AdministradorOnly")]
        public async Task<IActionResult> Index()
        {
            var users = _context.Users
                .Include(u => u.Profile)
                .Include(u => u.Plans);
            return View(await _context.Users.ToListAsync());
        }
        
        [Authorize(Policy = "AdministradorOnly")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .Include(u => u.Plans)
                .Include(u => u.Profile)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        [Authorize(Policy = "AdministradorOnly")]
        public IActionResult Create()
        {
            ViewData["Profiles"] = new SelectList(_context.Profiles, "Id", "UserTypeName");
            ViewData["Plans"] = new SelectList(_context.Plans, "Id", "Name");
/*            
            var profiles = _context.Profiles.ToList();
            var plans = _context.Plans.ToList();

            if(!profiles.Any() || !plans.Any())
            {
                ModelState.AddModelError(String.Empty, "No hay perfiles o planes disponibles.");
                return View();    
            }
            
            ViewBag.Profiles = new SelectList(_context.Profiles, "Id", "UserTypeName");
            ViewBag.Plans = new SelectList(_context.Plans, "Id", "Name");
*/
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("Id,Dni,Name,Mail,Phone,Address,Password,Active,ProfileId,PlanId")] Users users)
        {
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Profiles"] = new SelectList(_context.Profiles, "Id", "UserTypeName");
            ViewData["Plans"] = new SelectList(_context.Plans, "Id", "Name");
            /*
            var profiles = _context.Profiles.ToList();
            var plans = _context.Plans.ToList();

            ViewBag.Profiles = new SelectList(_context.Profiles, "Id", "UserTypeName");
            ViewBag.Plans = new SelectList(_context.Plans, "Id", "Name");
*/

           if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    ModelState.AddModelError(string.Empty, error.ErrorMessage); // Agrega errores al ModelState
                }
                return View(users); // Retorna la vista con errores de validación visibles
            }


            return View(users);
        }

        [Authorize(Policy = "AdministradorOnly")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            ViewData["Profiles"] = new SelectList(_context.Profiles, "Id", "UserTypeName");
            ViewData["Plans"] = new SelectList(_context.Plans, "Id", "Name");
/*
            ViewBag.Profiles = new SelectList(_context.Profiles, "Id", "UserTypeName");
            ViewBag.Plans = new SelectList(_context.Plans, "Id", "Name");
*/
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, [Bind("Id,Dni,Name,Mail,Phone,Address,Password,Active,ProfileId,PlanId")] Users users)
        {
            if (id != users.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.Id))
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

            ViewData["Profiles"] = new SelectList(_context.Profiles, "Id", "UserTypeName", users.ProfileId);
            ViewData["Plans"] = new SelectList(_context.Plans, "Id", "Name", users.PlanId);
/*
            ViewBag.Profiles = new SelectList(_context.Profiles, "Id", "UserTypeName");
            ViewBag.Plans = new SelectList(_context.Plans, "Id", "Name");
*/
            return View(users); 
        }
        [Authorize(Policy = "AdministradorOnly")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .Include(u => u.Profile)
                .Include(u => u.Plans)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }
        [Authorize(Policy = "AdministradorOnly")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users != null)
            {
                _context.Users.Remove(users);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}