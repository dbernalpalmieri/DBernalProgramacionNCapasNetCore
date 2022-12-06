using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class DependienteTipo
    {
        int idDependienteTipo;
        string nombre;
        List<object>? dependienteTipos;

        public int IdDependienteTipo { get => idDependienteTipo; set => idDependienteTipo = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public List<object>? DependienteTipos { get => dependienteTipos; set => dependienteTipos = value; }
    }
}
