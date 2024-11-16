using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
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
            return View(await users.ToListAsync());
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

            ViewBag.Profiles = new SelectList(_context.Profiles, "Id", "UserTypeName");
            ViewBag.Plans = new SelectList(_context.Plans, "Id", "Name");

        return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Users users)
        {
            ModelState.Remove("DocumentContent");
            
            if (ModelState.IsValid)
            {
                if (users.Document != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await users.Document.CopyToAsync(memoryStream);
                        users.DocumentContent = memoryStream.ToArray();  
                    }
                    
                    users.DocumentName = users.Document.FileName;      
                    users.DocumentContent = users.DocumentContent;    
                }

                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Profiles = new SelectList(_context.Profiles, "Id", "UserTypeName");
            ViewBag.Plans = new SelectList(_context.Plans, "Id", "Name");

            return View(users);
        }

        [HttpGet("Users/Edit/{id}")]
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

            ViewBag.Profiles = new SelectList(_context.Profiles, "Id", "UserTypeName");
            ViewBag.Plans = new SelectList(_context.Plans, "Id", "Name");

            return View(users);
        }

        [HttpPost("Users/Edit/{id}")]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Dni,Name,LastName,Mail,Phone,Address,Password,Active,ProfileId,PlanId,MedicalDocument")] Users users, IFormFile? Document)
        {
            if (id != users.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingUser = await _context.Users.FindAsync(id);

                    if (existingUser == null)
                    {
                        return NotFound();
                    }

                    if (Document != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await Document.CopyToAsync(memoryStream);
                            existingUser.DocumentContent = memoryStream.ToArray();
                            existingUser.DocumentName = Document.FileName; 
                        }
                    }
                    else if (!users.MedicalDocument)
                    {
                        existingUser.DocumentContent = null;
                        existingUser.DocumentName = null;
                    }

                    existingUser.Dni = users.Dni;
                    existingUser.Name = users.Name;
                    existingUser.LastName = users.LastName;
                    existingUser.Mail = users.Mail;
                    existingUser.Phone = users.Phone;
                    existingUser.Address = users.Address;
                    existingUser.Password = users.Password;
                    existingUser.Active = users.Active;
                    existingUser.ProfileId = users.ProfileId;
                    existingUser.PlanId = users.PlanId;
                    existingUser.MedicalDocument = users.MedicalDocument;

                    _context.Update(existingUser);
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

            ViewBag.Profiles = new SelectList(_context.Profiles, "Id", "UserTypeName");
            ViewBag.Plans = new SelectList(_context.Plans, "Id", "Name");

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