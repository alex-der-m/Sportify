using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sportify_Back.Models; 
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace Sportify_Back.Controllers
{
    // Restringe el acceso a usuarios autenticados
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        // Constructor único para la inyección de dependencias
        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        // Acción para mostrar la lista de usuarios
        [Authorize(Policy = "AdministradorOnly")]
        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users
                .Select(user => new ApplicationUser
                {
                    Id = user.Id, 
                    Name = user.Name,
                    LastName = user.LastName,
                    Email = user.Email,
                    DocumentName = user.DocumentName,
                    DocumentContent = user.DocumentContent,
                    DNI = user.DNI
                })
                .ToList();

            return View(users);
        }

        // Acción para ver detalles de un usuario
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // Acción GET para editar un usuario
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // Acción POST para editar un usuario
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Users/Edit/{id}")]
        public async Task<IActionResult> Edit(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                // Buscar el usuario existente por su ID
                var existingUser = await _userManager.FindByIdAsync(user.Id);
                
                if (existingUser != null)
                {
                    // Asignar los campos Name, LastName, y DNI
                    existingUser.Name = user.Name;
                    existingUser.LastName = user.LastName;
                    existingUser.DNI = user.DNI;

                    // Si se cargó un documento médico, procesarlo
                    if (user.Document != null && user.Document.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await user.Document.CopyToAsync(memoryStream);
                            existingUser.DocumentContent = memoryStream.ToArray(); // Guardar el contenido del archivo
                            existingUser.DocumentName = user.Document.FileName; // Guardar el nombre del archivo
                        }
                    }

                    // Actualizar el usuario con los cambios
                    var result = await _userManager.UpdateAsync(existingUser);

                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }

            return View(user);
        }

    }
}
