using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeCRUD.Models
{

    [Table("Usuario")]
    public class UsuarioRegistrado:IdentityUser
    {
       
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Pais { get; set; }
        public string CodigoPais { get; set; }
        public string Ciudad { get; set; }
        public string Direccion { get; set; }
        public string Celular { get; set; }
        public bool Estado { get; set; } = true;
        public string? Url { get; set; }
    }
}
