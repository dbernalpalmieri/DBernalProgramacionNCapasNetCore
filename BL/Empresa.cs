using DL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empresa
    {
        /// Entity Framework Métodos
        public static Result GetById(int IdEmpresa)
        {
            Result result = new Result();
            try
            {
                using (DbernalProgramacionNcapasContext context = new DbernalProgramacionNcapasContext())
                {
                    var execute = context.Empresas.FromSqlInterpolated($"EmpresaGet @IdEmpresa={IdEmpresa}").AsEnumerable().SingleOrDefault();

                    if (execute != null)
                    {

                        ML.Empresa empresa = new ML.Empresa();
                        empresa.IdEmpresa = execute.IdEmpresa;
                        empresa.Nombre = execute.Nombre;
                        empresa.Telefono = execute.Telefono;
                        empresa.Email = execute.Email;
                        empresa.DireccionWeb = execute.DireccionWeb;
                        empresa.Logo = execute.Logo;

                        result.Object = empresa;
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

        public static Result GetAll(ML.Empresa empresa)
        {
            Result result = new Result();
            try
            {
                using (DbernalProgramacionNcapasContext context = new DbernalProgramacionNcapasContext())
                {
                    var execute = context.Empresas.FromSqlInterpolated($"EmpresaGet @IdEmpresa={null}, @Nombre={empresa.Nombre}").ToList();

                    result.Objects = new List<object>();
                    if (execute.Count > 0)
                    {
                       
                        ML.Empresa empresaObjeto = null;

                        foreach (var item in execute)
                        {
                            empresaObjeto = new ML.Empresa();
                            empresaObjeto.IdEmpresa = item.IdEmpresa;
                            empresaObjeto.Nombre = item.Nombre;
                            empresaObjeto.Telefono = item.Telefono;
                            empresaObjeto.Email = item.Email;
                            empresaObjeto.DireccionWeb = item.DireccionWeb;
                            empresaObjeto.Logo = item.Logo;

                            result.Objects.Add(empresaObjeto);
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

        public static Result Add(ML.Empresa empresa)
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
                    var IdEmpresa = new SqlParameter
                    {
                        ParameterName = "@IdEmpresa",
                        DbType = DbType.Int32,
                        Direction = ParameterDirection.Output
                    };

                    int execute = context.Database.ExecuteSqlInterpolated($"EmpresaAdd @Nombre={empresa.Nombre}, @Telefono={empresa.Telefono}, @Email={empresa.Email}, @DireccionWeb={empresa.DireccionWeb}, @Logo={empresa.Logo}, @Mensaje={Mensaje} OUT, @IdEmpresa={IdEmpresa} OUT");

                    string message = Convert.ToString(Mensaje.Value);
                    int idEmpresa = Convert.ToInt32(IdEmpresa.Value);

                    result.Message = $"Mensaje: {message}";

                    if (execute > 0)
                    {
                        result.Object = new ML.Empresa
                        {
                            IdEmpresa = idEmpresa
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

        public static Result Update(ML.Empresa empresa)
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
                    int execute = context.Database.ExecuteSqlInterpolated($"EmpresaUpdate @Nombre={empresa.Nombre}, @Telefono={empresa.Telefono}, @Email={empresa.Email}, @DireccionWeb={empresa.DireccionWeb}, @Logo={empresa.Logo}, @IdEmpresa={empresa.IdEmpresa}, @Mensaje={Mensaje} OUT");
                    //int execute = context.Database.ExecuteSqlRaw($"EmpresaUpdate '{empresa.Nombre}', '{empresa.Telefono}', '{empresa.Email}', '{empresa.DireccionWeb}', '{empresa.Logo}', '{empresa.IdEmpresa}', '{Mensaje} OUT'");

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

        public static Result Delete(int IdEmpresa)
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

                    int execute = context.Database.ExecuteSqlInterpolated($"EmpresaDelete @IdEmpresa={IdEmpresa}, @Mensaje={Mensaje} OUT");

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
    }
}
