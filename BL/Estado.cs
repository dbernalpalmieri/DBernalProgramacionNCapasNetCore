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
    public class Estado
    {
        public static Result GetByIdPais(int IdPais)
        {
            Result result = new Result();
            try
            {
                using (DbernalProgramacionNcapasContext context = new DbernalProgramacionNcapasContext())
                {
                    var execute = context.Estados.FromSqlInterpolated($"EstadoGetByIdPais @IdPais={IdPais}").ToList();
                    
                    if (execute.Count > 0)
                    {
                        result.Objects = new List<object>();
                        ML.Estado estado = null;


                        foreach (var item in execute)
                        {
                            estado = new ML.Estado();
                            estado.Pais = new ML.Pais();

                            estado.IdEstado = item.IdEstado;
                            estado.Nombre = item.Nombre;
                            estado.Pais.IdPais = item.IdPais;
                            result.Objects.Add(estado);
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
