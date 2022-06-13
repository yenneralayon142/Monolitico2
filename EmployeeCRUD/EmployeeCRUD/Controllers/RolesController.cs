using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EmployeeCRUD.Models.ViewModels;
using EmployeeCRUD.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using EmployeeCRUD.Data;

namespace EmployeeCRUD.Controllers
{
    public class RolesController : Controller
    {
        private readonly UserManager <IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _applicationDbContext;

        public RolesController (UserManager <IdentityUser> userManager,RoleManager<IdentityRole> roleManager,ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _applicationDbContext = applicationDbContext;
        }

        //Lista de Roles
        public IActionResult Index()
        {
            return View();
        }


        //Crear Roles
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Crear(IdentityRole identityRole)
        {
            if (ModelState.IsValid)
            {
                return View (identityRole);

            }

            if (await _roleManager.RoleExistsAsync(identityRole.Name))
            {
                TempData["Error"] = "El error ya existe";
                return RedirectToAction(nameof(Index));

            }

            //Crear el Rol
            await _roleManager.CreateAsync(new IdentityRole() { Name=identityRole.Name });
            TempData["Correcto"] = "Rol crado correctamente";
            return RedirectToAction(nameof(Index));

            //Editar Rol
            [HttpGet]
            public IActionResult Editar(string id);
            {
                if (string.IsNullOrEmpty(id))
                {
                    return View();
                }
                else
                {
                    var rol = _applicationDbContext.Roles.FirstOrDefault(r => r.Id == id);
                    //Considerar que pasa si existe en la DB
                    return View();
                }
                        
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Tasdk <IActionResult>Editar(IdentityRole role)
            {
                if (!ModelState.IsValid || role.Name == null)
                {
                    return View (role)
                }
                if (await _roleManager.RoleExistsAsync(role.Name))
                {
                    TempData["Error"] = "El rol ya existe";
                    return RedirectToAction(nameof(Index));
                }

                //Actualiza el rol
                var roleDB = _applicationDbContext.Roles.FirstOrDefault(r => r.Id == role.Id);
                if (roleDB == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                roleDB.Name = role.Name;
                roleDB.NormalizedName = role.Name.ToUpper();
                var resultado = await _roleManager.UpdateAsync(roleDB);
                TempData["Correcto"] = "Rol editado correctamente";

            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Borrar(string id)
            {
                var rol=_applicationDbContext.Roles.FirstOrDefault(r=>r.Id== id);
                if (rol == null)
                {
                    TempData["Error"] = "No existe el Rol";
                    return RedirectToAction(nameof(Index));

                }
             
                var usuariosRol = _applicationDbContext.UserRoles.Where(u => u.RoleId == id).Count();

                if (usuariosRol > 0)
                {
                    TempData["Error"] = "El rol tiene ususarios no se puede borrar";
                    return RedirectToAction(nameof(Index));

                }

                await _roleManager.DeleteAsync(rol); //Borrar rol de la DB
            }
        }
    }
}
