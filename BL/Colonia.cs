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
    public class Colonia
    {
        public static Result GetByIdMunicipio(int IdMunicipio)
        {
            Result result = new Result();
            try
            {
                using (DbernalProgramacionNcapasContext context = new DbernalProgramacionNcapasContext())
                {
                    var execute = context.Colonia.FromSqlInterpolated($"ColoniaGetByIdMunicipio @IdMunicipio={IdMunicipio}").ToList();

                    if (execute.Count > 0)
                    {
                        result.Objects = new List<object>();
                        ML.Colonia colonia = null;

                        foreach (var item in execute)
                        {
                            colonia = new ML.Colonia();
                            colonia.Municipio = new ML.Municipio();


                            colonia.IdColonia = item.IdColonia;
                            colonia.Nombre = item.Nombre;
                            colonia.CodigoPostal = item.CodigoPostal;
                            colonia.Municipio.IdMunicipio = item.IdMunicipio;

                            result.Objects.Add(colonia);
                        }
                        result.Correct = true;
                        result.Message = $"Información obtenida con éxito de la base de datos.";
                    }
                    else
                    {
                        result.Correct = true;
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
