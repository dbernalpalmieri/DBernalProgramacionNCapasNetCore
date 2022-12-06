using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Rol
    {
        byte idRol;
        string? nombre;
        List<object>? roles;

        [Required(ErrorMessage = "Please select a Rol")]
        [DisplayName("Rol")]
        public byte IdRol { get => idRol; set => idRol = value; }
        //[Display(Name = "Rol")]
        //[Required(AllowEmptyStrings = true)]
        //[DisplayName("Rol")]
        
        public List<object>? Roles { get => roles; set => roles = value; }
        public string? Nombre { get => nombre; set => nombre = value; }
    }
}
