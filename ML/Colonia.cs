using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Colonia
    {
        int idColonia;
        string? nombre;
        string? codigoPostal;
        ML.Municipio? municipio;
        List<object>? colonias;

        
        [DisplayName("Colonia")]
        public int IdColonia { get => idColonia; set => idColonia = value; }
        
        [DisplayName("Colonia")]
        public string? Nombre { get => nombre; set => nombre = value; }
        
        [DisplayName("Código Postal")]
        public string? CodigoPostal { get => codigoPostal; set => codigoPostal = value; }
        public Municipio? Municipio { get => municipio; set => municipio = value; }
        public List<object>? Colonias { get => colonias; set => colonias = value; }
    }
}
