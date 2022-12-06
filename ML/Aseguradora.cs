using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Aseguradora
    {
        int idAseguradora;
        string nombre;
        string fechaCreacion;
        string fechaModificacion;
        List<Object>? aseguradoras;

        ML.Usuario? usuario;

        public int IdAseguradora { get => idAseguradora; set => idAseguradora = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
        public string FechaModificacion { get => fechaModificacion; set => fechaModificacion = value; }
        public ML.Usuario? Usuario { get => usuario; set => usuario = value; }
        public List<object>? Aseguradoras { get => aseguradoras; set => aseguradoras = value; }
    }
}
