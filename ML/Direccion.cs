using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Direccion
    {
        int? idDireccion;
        string calle;
        string numeroInterior;
        string numeroExterior;
        ML.Colonia? colonia;
        //  ModelLayer.Usuario usuario;
        List<object>? direcciones;


        public int? IdDireccion { get => idDireccion; set => idDireccion = value; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Mínimo tres caracteres.")]
        public string Calle { get => calle; set => calle = value; }

        [Required]
        [DisplayName("Numero Interior")]
        [StringLength(5, MinimumLength = 1)]
        public string NumeroInterior { get => numeroInterior; set => numeroInterior = value; }

        [Required]
        [DisplayName("Numero Exterior")]
        [StringLength(5, MinimumLength = 1)]
        public string NumeroExterior { get => numeroExterior; set => numeroExterior = value; }

        public Colonia? Colonia { get => colonia; set => colonia = value; }

        public List<object>? Direcciones { get => direcciones; set => direcciones = value; }
    }
}
