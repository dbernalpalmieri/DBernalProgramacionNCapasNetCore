using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Municipio
    {
        int idMunicipio;
        string? nombre;
        ML.Estado? estado;
        List<object>? municipios;

        
        [DisplayName("Municipio")]
        public int IdMunicipio { get => idMunicipio; set => idMunicipio = value; }
        
        [DisplayName("Municipio")]
        public string? Nombre { get => nombre; set => nombre = value; }
        public Estado? Estado { get => estado; set => estado = value; }
        public List<object>? Municipios { get => municipios; set => municipios = value; }
    }
}
