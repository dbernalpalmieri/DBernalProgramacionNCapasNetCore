using DL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Aseguradora
    {
        public static Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {

                using (DbernalProgramacionNcapasContext context = new DbernalProgramacionNcapasContext())
                {
                    var execute = context.Aseguradoras.FromSqlInterpolated($"AseguradoraGet @IdAseguradora={null}").ToList();

                    result.Objects = new List<object>();

                    if (execute.Count > 0)
                    {
                        ML.Aseguradora aseguradora = null;

                        foreach (var item in execute)
                        {
                            aseguradora = new ML.Aseguradora();
                            aseguradora.IdAseguradora = item.IdAseguradora;
                            aseguradora.Nombre = item.Nombre;
                            aseguradora.FechaCreacion = item.FechaCreacion.ToString();
                            aseguradora.FechaModificacion = item.FechaModificacion.ToString();

                            aseguradora.Usuario = new ML.Usuario();

                            aseguradora.Usuario.IdUsuario = item.IdUsuario;
                            aseguradora.Usuario.Nombre = item.NombreUsuario;
                            aseguradora.Usuario.ApellidoPaterno = item.ApellidoPaterno;
                            aseguradora.Usuario.ApellidoMaterno = item.ApellidoMaterno;

                            aseguradora.Usuario.NombreCompleto = $"{item.NombreUsuario} {item.ApellidoPaterno} {item.ApellidoMaterno}";

                            result.Objects.Add(aseguradora);

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

                result.Correct = false;
                result.Exeption = error;
                result.Message = $"Ups ocurrió un error: {error.Message}";
            }

            return result;

        }

        public static Result GetById(int IdAseguradora)
        {
            ML.Result result = new ML.Result();

            try
            {

                using (DbernalProgramacionNcapasContext context = new DbernalProgramacionNcapasContext())
                {
                    var execute = context.Aseguradoras.FromSqlInterpolated($"AseguradoraGet @IdAseguradora={IdAseguradora}").AsEnumerable().SingleOrDefault();


                    if (execute != null)
                    {
                        ML.Aseguradora aseguradora = new ML.Aseguradora();
                        aseguradora.IdAseguradora = execute.IdAseguradora;
                        aseguradora.Nombre = execute.Nombre;
                        aseguradora.FechaCreacion = execute.FechaCreacion.ToString();
                        aseguradora.FechaModificacion = execute.FechaModificacion.ToString();

                        aseguradora.Usuario = new ML.Usuario();

                        aseguradora.Usuario.IdUsuario = execute.IdUsuario;
                        aseguradora.Usuario.Nombre = execute.NombreUsuario;
                        aseguradora.Usuario.ApellidoPaterno = execute.ApellidoPaterno;
                        aseguradora.Usuario.ApellidoMaterno = execute.ApellidoMaterno;

                        result.Object = aseguradora;

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

                result.Correct = false;
                result.Exeption = error;
                result.Message = $"Ups ocurrió un error: {error.Message}";
            }


            return result;
        }

        public static Result Add(ML.Aseguradora aseguradora)
        {
            Result result = new Result();
            try
            {
                using (DbernalProgramacionNcapasContext context = new DbernalProgramacionNcapasContext())
                {
                    var Mensaje = new SqlParameter
                    {
                        ParameterName = "@Mensaje",
                        DbType = DbType.String,
                        Size = 100,
                        Direction = ParameterDirection.Output
                    };
                    var IdAseguradora = new SqlParameter
                    {
                        ParameterName = "@IdAseguradora",
                        DbType = DbType.Int32,
                        Direction = ParameterDirection.Output
                    };

                    int execute = context.Database.ExecuteSqlInterpolated($"AseguradoraAdd @Nombre={aseguradora.Nombre}, @IdUsuario={aseguradora.Usuario.IdUsuario}, @Mensaje={Mensaje} OUT, @IdAseguradora={IdAseguradora} OUT");


                    string message = Convert.ToString(Mensaje.Value);
                    int idAseguradora = Convert.ToInt32(IdAseguradora.Value);

                    result.Message = $"Mensaje: {message}";



                    if (execute > 0)
                    {
                        result.Object = new ML.Aseguradora
                        {
                            IdAseguradora = idAseguradora
                        };
                        result.Correct = true;
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

        public static Result Update(ML.Aseguradora aseguradora)
        {
            Result result = new Result();
            try
            {
                using (DbernalProgramacionNcapasContext context = new DbernalProgramacionNcapasContext())
                {
                    var Mensaje = new SqlParameter
                    {
                        ParameterName = "@Mensaje",
                        DbType = DbType.String,
                        Size = 100,
                        Direction = ParameterDirection.Output
                    };
                    
                    int execute = context.Database.ExecuteSqlInterpolated($"AseguradoraUpdate @Nombre={aseguradora.Nombre}, @IdUsuario={aseguradora.Usuario.IdUsuario}, @IdAseguradora={aseguradora.IdAseguradora}, @Mensaje={Mensaje} OUT");

                    string message = Convert.ToString(Mensaje.Value);
                    result.Message = $"Mensaje: {message}";

                    if (execute > 0)
                    {
                        result.Correct = true;
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

        public static Result Delete(int IdAseguradora)
        {
            Result result = new Result();
            try
            {
                using (DbernalProgramacionNcapasContext context = new DbernalProgramacionNcapasContext())
                {

                    var Mensaje = new SqlParameter
                    {
                        ParameterName = "@Mensaje",
                        DbType = DbType.String,
                        Size = 100,
                        Direction = ParameterDirection.Output
                    };

                    int execute = context.Database.ExecuteSqlInterpolated($"AseguradoraDelete @IdAseguradora={IdAseguradora}, @Mensaje={Mensaje} OUT");

                    string message = Convert.ToString(Mensaje.Value);
                    result.Message = $"Mensaje: {message}";

                    if (execute > 0)
                    {
                        result.Correct = true;
                    }
                }

            }
            catch (Exception error)
            {

                result.Exeption = error;
                result.Correct = false;
                result.Message = $"Ups ocurrió un error: {error.Message}";
            }

            return result;
        }

    }
}
