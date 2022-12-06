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
    public class Municipio
    {
        public static Result GetByIdEstado(int IdEstado)
        {
            Result result = new Result();
            try
            {
                using (DbernalProgramacionNcapasContext context = new DbernalProgramacionNcapasContext())
                {
                    var execute = context.Municipios.FromSqlInterpolated($"MunicipioGetByIdEstado @IdEstado={IdEstado}").ToList();

                    if (execute.Count > 0)
                    {
                        result.Objects = new List<object>();
                        ML.Municipio municipio = null;

                        foreach (var item in execute)
                        {
                            municipio = new ML.Municipio();
                            municipio.Estado = new ML.Estado();

                            municipio.IdMunicipio = item.IdMunicipio;
                            municipio.Nombre = item.Nombre;
                            municipio.Estado.IdEstado = item.IdEstado;

                            result.Objects.Add(municipio);
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
