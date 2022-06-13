using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EmployeeCRUD.Models.ViewModels;
using EmployeeCRUD.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;


namespace EmployeeCRUD.Controllers
{
    public class CuentasController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;     // Manejador de Usuarios
        private readonly IEmailSender _emailSender;                  // Interfaz para manejo de Email
        private readonly SignInManager<IdentityUser> _signInManager; //Manejador de autenticacion
        private readonly RoleManager<IdentityUser> _roleManager;

        //Crear un constructor

        public CuentasController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> singInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = singInManager;
            _emailSender = emailSender;

        }

        [HttpGet]
        [AllowAnonymous]

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Registro(string returnurl = null)
        {
            ViewData["Returnurl"] = returnurl;
            RegisterViewModel registroViewModel = new RegisterViewModel();
            return View(registroViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Registro(RegisterViewModel registroViewModel, string returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            returnurl = returnurl ?? Url.Content("/");


            if (ModelState.IsValid)
            {
                var usuario = new UsuarioRegistrado
                {
                    Nombres = registroViewModel.Nombres,
                    Apellidos = registroViewModel.Apellidos,
                    UserName = registroViewModel.Email,
                    Email = registroViewModel.Email,
                    Url = registroViewModel.URL,
                    CodigoPais = registroViewModel.CodigoPais,
                    Pais = registroViewModel.Pais,
                    PhoneNumber = registroViewModel.Celular,
                    Ciudad = registroViewModel.Ciudad,
                    Direccion = registroViewModel.Direccion,
                    FechaNacimiento = registroViewModel.FechaNacimiento,
                    Estado = registroViewModel.Estado
                };

                var resultado = await _userManager.CreateAsync(usuario, registroViewModel.Password); // Manejador de usuarios del Framework de Identity

                if (resultado.Succeeded)
                {
                    //Pendiente agregar el usuario al rol por defecto
                    await 
                    //Confirmacion por email..

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(usuario);
                    var urlRetorno = Url.Action("ConfirmarEmail", "Cuentas", new { userid = usuario.Id, code = code }, protocol: HttpContext.Request.Scheme);
                    await _emailSender.SendEmailAsync (
                                                         registroViewModel.Email,
                                                         "Confirmar su cuenta = EmployeesCRUD" +
                                                         "Por favor confirme su cuenta dando click aqui <a>href=\"" + urlRetorno + "\">enlace</a>"
                                                         );
                    await _signInManager.SignInAsync(usuario, isPersistent: false); // Manejador de autenticación del Framework de identidad (Identity)
                    return LocalRedirect(returnurl);
                };

                ValidarErrores(resultado);




                return View(usuario);
            }
        }

        [AllowAnonymous]
        
        private void ValidarErrores(IdentityResult resultado)
        {
            foreach (var error in resultado.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);

            }
        }

        [HttpGet]
        [AllowAnonymous]

        public async Task<IActionResult> ConfirmarEmail(string useriId,string code)
        {
            if (useriId == null || code == null)
            {
                return View("Error");
            }

            var usuario = await _userManager.FindByIdAsync(useriId);
            if (usuario == null)
            {
                return View("Error");
            }

            var resultado = await _userManager.ConfirmEmailAsync(usuario, code);
            return View(resultado.Succeeded?"ConfirmarEmail":"Error");

        }

        [HttpGet]
        [AllowAnonymous]

        public IActionResult Acceso(string returnurl = null)
        {
            ViewData["Returnurl"] = returnurl;
            return View();
        }

    }
}
