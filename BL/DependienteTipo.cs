using DL;
using Microsoft.EntityFrameworkCore;
using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class DependienteTipo
    {
        public static Result GetAll()
        {
            Result result = new Result();
            try
            {
                using (DbernalProgramacionNcapasContext context = new DbernalProgramacionNcapasContext())
                {
                    var execute = context.DependienteTipos.FromSqlInterpolated($"DependienteTipoGet @IdDependienteTipo={null}").ToList();
                    if (execute.Count > 0)
                    {
                        result.Objects = new List<object>();
                        ML.DependienteTipo dependienteTipo = null;

                        foreach (var item in execute)
                        {
                            dependienteTipo = new ML.DependienteTipo();

                            dependienteTipo.IdDependienteTipo = item.IdDependienteTipo;
                            dependienteTipo.Nombre = item.Nombre;

                            result.Objects.Add(dependienteTipo);
                        }
                        result.Correct = true;
                        result.Message = $"Información obtenida con éxito de la base de datos";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = $"404 NOT FOUND";
                    }
                }
            }
            catch (Exception error)
            {
                result.Exeption = error;
                result.Message = $"Ups ocurrió un error: {error.Message}";
                result.Correct = false;
            }
            return result;
        }
    }
}
