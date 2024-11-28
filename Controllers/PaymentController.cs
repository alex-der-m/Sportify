using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sportify_back.Models;
using System.Data.SqlClient;
using Sportify_Back.Models;
using System.Security.Claims;


namespace Sportify_Back.Controllers
{
public class PaymentController : Controller
{

         private readonly SportifyDbContext _context;    

        public PaymentController(SportifyDbContext context)
        {
            _context = context;
        }
        

        // GET: Pagos
        public async Task<IActionResult> Index()
        {
            // Obtén el perfil del usuario desde los claims
            var profile = User.FindFirstValue("Profile");

            // Obtén el ID del usuario autenticado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            IQueryable<Payments> paymentsQuery;

            if (profile == "Administrador")
            {
                // Si es Administrador, mostrar todos los movimientos
                paymentsQuery = _context.Payments
                    .Include(p => p.ApplicationUser)
                    .Include(p => p.Plans)
                    .Include(p => p.PaymentMethod);
            }
            else
            {
                // Si no es Administrador, mostrar solo los movimientos del usuario actual
                paymentsQuery = _context.Payments
                    .Where(p => p.UsersId == userId)
                    .Include(p => p.ApplicationUser)
                    .Include(p => p.Plans)
                    .Include(p => p.PaymentMethod);
            }

            return View(await paymentsQuery.ToListAsync());
        }



        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Buscar el pago con el ID especificado, incluyendo las relaciones
            var payment = await _context.Payments
                .Include(p => p.ApplicationUser)      // Relación con usuario
                .Include(p => p.Plans)               // Relación con plan
                .Include(p => p.PaymentMethod)       // Relación con método de pago
                .FirstOrDefaultAsync(m => m.Id == id);

            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        //Función para pasarle el modelo de datos a la vista          
        public IActionResult ModelAction()

        {
            var users = new ApplicationUser ();  
            var PaymentMethod = new PaymentMethod();
            var plans = new Plans();
        


            var viewModel = new Payments
            {
                ApplicationUser = users,
                PaymentMethod = PaymentMethod,
                Plans = plans

                
            };
    
    return View(viewModel);

}


//Esta función permite obtener el monto a partir del plan seleccionado
public IActionResult GetPlanAmount(int planId)
{
    var plans = _context.Plans.FirstOrDefault(p => p.Id == planId);
    if (plans == null)
    {
        return Json(new { success = false, message = "Plan no encontrado." });
    }
    return Json(new { success = true, monto =plans.Monto});
}


        public IActionResult Create()
        {
            var currentUserId = User.Identity.Name; // O usar otro método para obtener el ID del usuario actual

            // Encontrar el usuario actual en la base de datos (suponiendo que 'User' es de tipo ApplicationUser)
            var currentUser = _context.Users.FirstOrDefault(u => u.UserName == currentUserId); // O usar 'Id' si es más apropiado

            // Crear un SelectList que contiene solo el usuario actual
            ViewData["UsersId"] = new SelectList(new List<ApplicationUser> { currentUser }, "Id", "Name", currentUser?.Id);

            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "Id", "Tipo");
            ViewData["PlansId"] = new SelectList(_context.Plans, "Id", "Name");

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        //CrearPago
        public async Task<IActionResult> Create([Bind("Id,UsersId,PlansId,PaymentMethodId,Fecha")] Payments payments)
        {
            if (ModelState.IsValid)
            {
                payments.PaymentMethod = await _context.PaymentMethod
                    .FirstOrDefaultAsync(pm => pm.Id == payments.PaymentMethodId);    

                if (payments.PaymentMethodId == 0)
                {
                    ModelState.AddModelError("PaymentMethodId", "Debe seleccionar un método de pago.");
                    return View(payments);
                }

                // Si la fecha no está siendo enviada (puede ser opcional, dependiendo de tu lógica),
                // puedes asignarla a la fecha actual si no se ha proporcionado.
                payments.Fecha = DateTime.Now; // Asignar la fecha actual si no se proporcionó
                

                // Crear el pago y guardarlo en la base de datos
                _context.Add(payments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));    
            }

            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "Id", "Tipo", payments.PaymentMethodId);
            ViewData["PlansId"] = new SelectList(_context.Plans, "Id", "Name", payments.PlansId);
            ViewData["UsersId"] = new SelectList(_context.Users, "Id", "Name", payments.UsersId);

            return View(payments);
        }

    }
}
