using System.ComponentModel.DataAnnotations;

namespace EmployeeCRUD.Models.ViewModels
{


    public class RegisterViewModel
    {

        public  string Nombres { get; set; }
        public  string Apellidos { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [Display(Name = "Fecha de nacimiento")]
        public  DateTime FechaNacimiento { get; set; }
      
        public   string  Pais { get; set; }
        public   string CodigoPais  { get; set; }
        public   string Ciudad { get; set; }
        public   string Direccion { get; set; }

        public string Celular { get; set; }
        public bool Estado { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress]
        public string Email { get; set; }


        [Required(ErrorMessage = "la contraseña es obligatoria")]
        [StringLength(50, ErrorMessage = "El {0} debe estar almenos entre {2} caracteres de longitud", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }


        [Required(ErrorMessage ="La confirmacion de la contraseña es obligatoria")]
        [Compare ("Password", ErrorMessage ="La contraseña y confirmacion de contraseña no coinciden")]
        [DataType(DataType.Password)]
        [Display(Name ="Confirmar contraseña")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "La confirmacion de la contraseña es obligatoria")]

        [Url]
        public string URL { get; set; }

    }
}
