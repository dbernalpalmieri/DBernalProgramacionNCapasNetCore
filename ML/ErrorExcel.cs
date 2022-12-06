using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
     public class ErrorExcel
    {
        int idRegistro;
        string mensaje;
        List<object> errores;
        bool correct;

        public int IdRegistro { get => idRegistro; set => idRegistro = value; }
        public string Mensaje { get => mensaje; set => mensaje = value; }
        public List<object> Errores { get => errores; set => errores = value; }
        public bool Correct { get => correct; set => correct = value; }
    }
}
