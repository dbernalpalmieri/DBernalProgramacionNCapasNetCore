using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Dependiente
    {
        int? idDependiente;
        string nombre;
        string apellidoPaterno;
        string apellidoMaterno;
        string fechaNacimiento;
        char genero;
        string? telefono;
        string rfc;

        ML.EstadoCivil? estadoCivil;
        ML.DependienteTipo? dependienteTipo;
        ML.Empleado? empleado;
        List<object> dependientes;

        string? nombreCompleto;

        public int? IdDependiente { get => idDependiente; set => idDependiente = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string ApellidoPaterno { get => apellidoPaterno; set => apellidoPaterno = value; }
        public string ApellidoMaterno { get => apellidoMaterno; set => apellidoMaterno = value; }
        public string FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
        public char Genero { get => genero; set => genero = value; }
        public string? Telefono { get => telefono; set => telefono = value; }
        public string Rfc { get => rfc; set => rfc = value; }
        public EstadoCivil? EstadoCivil { get => estadoCivil; set => estadoCivil = value; }
        public DependienteTipo? DependienteTipo { get => dependienteTipo; set => dependienteTipo = value; }
        public Empleado? Empleado { get => empleado; set => empleado = value; }
        public List<object> Dependientes { get => dependientes; set => dependientes = value; }
        public string? NombreCompleto { get => nombreCompleto; set => nombreCompleto = value; }
    }
}
