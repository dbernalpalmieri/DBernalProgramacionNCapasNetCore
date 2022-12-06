using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        int idUsuario;
        string? nombre;
        string apellidoPaterno;
        string apellidoMaterno;
        char sexo;
        string fechaNacimiento;
        string email;
        string password;
        string userName;
        string telefono;
        string celular;
        string curp;
        ML.Rol? rol;
        List<object>? usuarios;
        ML.Direccion? direccion;

        string? imagen;

        bool? status;

        string? nombreCompleto;





        public int IdUsuario { get => idUsuario; set => idUsuario = value; }


        //[Required]
        //[StringLength(50, MinimumLength = 3)]
        //[MaxLength(50)]
        ////[DisplayName("Texto")]
        //[RegularExpression(@"^[a-zA-Z\s]+$")]
        public string? Nombre { get => nombre; set => nombre = value; }


        //[Required]
        //[StringLength(50, MinimumLength = 3)]
        //[MaxLength(50)]
        //[RegularExpression(@"^[a-zA-Z\s]+$")]
        public string ApellidoPaterno { get => apellidoPaterno; set => apellidoPaterno = value; }


        //[Required]
        //[StringLength(50, MinimumLength = 3)]
        //[MaxLength(50)]
        //[RegularExpression(@"^[a-zA-Z\s]+$")]
        public string ApellidoMaterno { get => apellidoMaterno; set => apellidoMaterno = value; }

        //[Required(ErrorMessage = "Escoge un sexo.")]
        public char Sexo { get => sexo; set => sexo = value; }


        //[Required]
        //[DataType(DataType.EmailAddress)]
        //[RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",ErrorMessage = "Correo Invalido.")]
        ////[RegularExpression(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$")]
        public string Email { get => email; set => email = value; }


        //[DataType(DataType.Password)]
        //[Required]
        ////[RegularExpression(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$")]
        //[StringLength(50, MinimumLength = 8)]
        //[MaxLength(50)]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "La contraseña debe de tener al menos 8 caracteres, una letra mayúscula, una minúscula y un carácter especial")]
        public string Password { get => password; set => password = value; }


        //[Required]
        public string UserName { get => userName; set => userName = value; }


        public Rol? Rol { get => rol; set => rol = value; }

        //[Required]
        //[DataType(DataType.PhoneNumber)]
        //[StringLength(14, MinimumLength = 10)]
        //[Phone(ErrorMessage ="No has ingresado números.")]
        public string Telefono { get => telefono; set => telefono = value; }

        //[Required]
        //[DataType(DataType.PhoneNumber)]
        //[StringLength(10, MinimumLength = 10)]
        //[Phone(ErrorMessage = "No has ingresado números.")]
        public string Celular { get => celular; set => celular = value; }

        //[Required]
        //[StringLength(18, MinimumLength = 18)]
        public string Curp { get => curp; set => curp = value; }


        //[Display (ResourceType = typeof(DateTimeConverter))]
        //[Required]
        public string FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
        public List<object>? Usuarios { get => usuarios; set => usuarios = value; }
        public Direccion? Direccion { get => direccion; set => direccion = value; }
        public string? Imagen { get => imagen; set => imagen = value; }
        public string? NombreCompleto { get => nombreCompleto; set => nombreCompleto = value; }
        public bool? Status { get => status; set => status = value; }
    }
}
