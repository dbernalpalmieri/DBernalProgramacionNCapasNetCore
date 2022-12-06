using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Empresa
    {
        int? idEmpresa;
	    string nombre;
        string telefono;
        string email;
        string direccionWeb;
        string? logo;
        List<object>? empresas;

        public int? IdEmpresa { get => idEmpresa; set => idEmpresa = value; }

        //[Required]
        //[StringLength(50, MinimumLength = 3)]
        //[MaxLength(50)]
        //[DisplayName("Texto")]
        //[RegularExpression(@"^[a-zA-Z][a-zA-Z0-9]*$")]
        public string Nombre { get => nombre; set => nombre = value; }
        
        //[Required]
        //[DataType(DataType.PhoneNumber)]
        //[StringLength(10, MinimumLength = 10)]
        //[Phone(ErrorMessage = "No has ingresado números.")]
        public string Telefono { get => telefono; set => telefono = value; }
        
        //[Required]
        //[DataType(DataType.EmailAddress)]
        //[RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Correo Invalido.")]
        public string Email { get => email; set => email = value; }
        
        //[Required]
        //[DisplayName("Dirección Web")]
        //[RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Dirección Web Invalido.")]
        public string DireccionWeb { get => direccionWeb; set => direccionWeb = value; }
        public List<object>? Empresas { get => empresas; set => empresas = value; }
        public string? Logo { get => logo; set => logo = value; }
    }
}
