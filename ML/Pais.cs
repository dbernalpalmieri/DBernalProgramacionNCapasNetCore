using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Pais
    {
        int idPais;
        string? nombre;
        List<object>? paises;

       
        [DisplayName("País")]
        public int IdPais { get => idPais; set => idPais = value; }
        [DisplayName("País")]
        public string? Nombre { get => nombre; set => nombre = value; }
        public List<object>? Paises { get => paises; set => paises = value; }
    }
}
