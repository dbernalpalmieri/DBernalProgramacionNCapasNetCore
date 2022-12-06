using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class EstadoCivil
    {
        int idEstadoCivil;
        string nombre;
        List<object>? estadosCiviles;

        public int IdEstadoCivil { get => idEstadoCivil; set => idEstadoCivil = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public List<object>? EstadosCiviles { get => estadosCiviles; set => estadosCiviles = value; }
    }
}
