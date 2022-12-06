using DL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace BL
{
    public class Empleado
    {
        public static Result GetAll()
        {
            ML.Result result = new Result();
            try
            {
                using (DbernalProgramacionNcapasContext context = new DbernalProgramacionNcapasContext())
                {
                    var execute = context.Empleados.FromSqlInterpolated($"EmpleadoGet @NumeroEmpleado={null}").ToList();
                    //Database.SqlQueryRaw<ML.Usuario>($"UsuarioGet").AsNoTracking().ToList();
                    //context.Set<ML.Usuario>().FromSqlRaw("UsuarioGet").AsNoTracking().ToList();
                    // context.Database.SqlQuery<List<ML.Usuario>>($"UsuarioGet");

                    result.Objects = new List<object>();


                    if (execute.Count > 0)
                    {
                        result.Objects = new List<object>();
                        ML.Empleado empleado = null;

                        foreach (var item in execute)
                        {
                            empleado = new ML.Empleado();
                            empleado.NumeroEmpleado = item.NumeroEmpleado;
                            empleado.Rfc = item.Rcf;
                            empleado.Nombre = item.Nombre;
                            empleado.ApellidoPaterno = item.ApellidoPaterno;
                            empleado.ApellidoMaterno = item.ApellidoMaterno;
                            empleado.Email = item.Email;
                            empleado.Telefono = item.Telefono;
                            empleado.FechaNacimiento = item.FechaNacimiento.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                            empleado.Nss = item.Nss;
                            empleado.FechaIngreso = item.FechaIngreso.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture); ;
                            empleado.Foto = item.Foto;

                            empleado.NombreCompleto = $"{item.Nombre} {item.ApellidoPaterno} {item.ApellidoMaterno}";

                            empleado.Empresa = new ML.Empresa();
                            empleado.Empresa.IdEmpresa = item.IdEmpresa;
                            empleado.Empresa.Nombre = item.EmpresaNombre;
                            empleado.Empresa.Telefono = item.EmpresaTelefono;
                            empleado.Empresa.Email = item.EmpresaEmail;
                            empleado.Empresa.DireccionWeb = item.DireccionWeb;
                            empleado.Empresa.Logo = item.Logo;

                            result.Objects.Add(empleado);
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

        public static Result GetById(string NumeroEmpleado)
        {
            Result result = new Result();
            try
            {
                using (DbernalProgramacionNcapasContext context = new DbernalProgramacionNcapasContext())
                {
                    var execute = context.Empleados.FromSqlInterpolated($"EmpleadoGet @NumeroEmpleado={NumeroEmpleado}").AsEnumerable().SingleOrDefault();
                    if (execute != null)
                    {

                        ML.Empleado empleado = new ML.Empleado();

                        empleado = new ML.Empleado();
                        empleado.NumeroEmpleado = execute.NumeroEmpleado;
                        empleado.Rfc = execute.Rcf;
                        empleado.Nombre = execute.Nombre;
                        empleado.ApellidoPaterno = execute.ApellidoPaterno;
                        empleado.ApellidoMaterno = execute.ApellidoMaterno;
                        empleado.Email = execute.Email;
                        empleado.Telefono = execute.Telefono;
                        empleado.FechaNacimiento = execute.FechaNacimiento.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        empleado.Nss = execute.Nss;
                        empleado.FechaIngreso = execute.FechaIngreso.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture); ;
                        empleado.Foto = execute.Foto;

                        empleado.NombreCompleto = $"{execute.Nombre} {execute.ApellidoPaterno} {execute.ApellidoMaterno}";

                        empleado.Empresa = new ML.Empresa();
                        empleado.Empresa.IdEmpresa = execute.IdEmpresa;
                        empleado.Empresa.Nombre = execute.EmpresaNombre;
                        empleado.Empresa.Telefono = execute.EmpresaTelefono;
                        empleado.Empresa.Email = execute.EmpresaEmail;
                        empleado.Empresa.DireccionWeb = execute.DireccionWeb;

                        result.Object = empleado;

                        result.Correct = true;
                        result.Message = $"Información obtenida con éxito de la base de datos.";
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

        public static Result Add(ML.Empleado empleado)
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
                    var IdEmpleado = new SqlParameter
                    {
                        ParameterName = "@IdEmpleado",
                        DbType = DbType.String,
                        Size = 50,
                        Direction = ParameterDirection.Output
                    };
                    int execute = context.Database.ExecuteSqlInterpolated($"EmpleadoAdd @NumeroEmpleado ={empleado.NumeroEmpleado}, @RCF={empleado.Rfc}, @Nombre={empleado.Nombre}, @ApellidoPaterno ={empleado.ApellidoPaterno}, @ApellidoMaterno={empleado.ApellidoMaterno}, @Email = {empleado.Email}, @Telefono={empleado.Telefono}, @FechaNacimiento ={empleado.FechaNacimiento}, @NSS={empleado.Nss}, @FechaIngreso={empleado.FechaIngreso}, @Foto={empleado.Foto}, @IdEmpresa={empleado.Empresa.IdEmpresa},  @Mensaje={Mensaje} OUT, @IdEmpleado={IdEmpleado} OUT");

                    string message = Convert.ToString(Mensaje.Value);
                    result.Message = $"Mensaje: {message}";

                    if (execute > 0)
                    {
                        // Recuperamos el valor del parámetro que nos devuelve la base de datos
                        string idEmpleado = Convert.ToString(IdEmpleado.Value);

                        result.Object = new ML.Empleado
                        {
                            NumeroEmpleado = idEmpleado
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

        public static Result Update(ML.Empleado empleado)
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
                    int execute = context.Database.ExecuteSqlInterpolated($"EmpleadoUpdate @NumeroEmpleado ={empleado.NumeroEmpleado}, @RCF={empleado.Rfc}, @Nombre={empleado.Nombre}, @ApellidoPaterno ={empleado.ApellidoPaterno}, @ApellidoMaterno={empleado.ApellidoMaterno}, @Email = {empleado.Email}, @Telefono={empleado.Telefono}, @FechaNacimiento ={empleado.FechaNacimiento}, @NSS={empleado.Nss}, @FechaIngreso={empleado.FechaIngreso}, @Foto={empleado.Foto}, @IdEmpresa={empleado.Empresa.IdEmpresa},  @Mensaje={Mensaje} OUT");

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

        public static Result Delete(string NumeroEmpleado)
        {
            Result result = new Result();
            try
            {
                using (DbernalProgramacionNcapasContext context = new DbernalProgramacionNcapasContext())
                {
                    // Parámetros que nos regresa el procedimiento almacenado
                    var Mensaje = new SqlParameter
                    {
                        ParameterName = "@Mensaje",
                        DbType = DbType.String,
                        Size = 100,
                        Direction = ParameterDirection.Output
                    };

                    int execute = context.Database.ExecuteSqlInterpolated($"EmpleadoDelete @NumeroEmpleado={NumeroEmpleado}, @Mensaje={Mensaje} OUT");

                    // Recuperamos el valor del parámetro que nos devuelve la base de datos
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
