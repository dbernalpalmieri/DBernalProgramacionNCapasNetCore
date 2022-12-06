using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Empleado
    {
        string numeroEmpleado;
        string rfc;
        string nombre;
        string apellidoPaterno;
        string apellidoMaterno;
        string email;
        string telefono;
        string fechaNacimiento;
        string nss;
        string fechaIngreso;
        string? foto;
        string? nombreCompleto;

        ML.Empresa? empresa;
        List<object> empleados;

        string? action;

        public string NumeroEmpleado { get => numeroEmpleado; set => numeroEmpleado = value; }
        public string Rfc { get => rfc; set => rfc = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string ApellidoPaterno { get => apellidoPaterno; set => apellidoPaterno = value; }
        public string ApellidoMaterno { get => apellidoMaterno; set => apellidoMaterno = value; }
        public string Email { get => email; set => email = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
        public string Nss { get => nss; set => nss = value; }
        public string FechaIngreso { get => fechaIngreso; set => fechaIngreso = value; }
        public string? Foto { get => foto; set => foto = value; }
        public Empresa? Empresa { get => empresa; set => empresa = value; }
        public List<object> Empleados { get => empleados; set => empleados = value; }
        public string? NombreCompleto { get => nombreCompleto; set => nombreCompleto = value; }
        public string? Action { get => action; set => action = value; }
    }
}
