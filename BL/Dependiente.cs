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

namespace BL
{
    public class Dependiente
    {
        public static Result GetByNumeroEmpleado(string NumeroEmpleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DbernalProgramacionNcapasContext context = new DbernalProgramacionNcapasContext())
                {
                    var execute = context.Dependientes.FromSqlInterpolated($"DependienteGetByIdNumeroEmpleado @NumeroEmpleado={NumeroEmpleado}").ToList();

                    result.Objects = new List<object>();
                    if (execute.Count > 0)
                    {
                        
                        ML.Dependiente dependiente = null;


                        foreach (var item in execute)
                        {
                            dependiente = new ML.Dependiente();
                            dependiente.DependienteTipo = new ML.DependienteTipo();
                            dependiente.Empleado = new ML.Empleado();
                            dependiente.EstadoCivil= new ML.EstadoCivil();

                            dependiente.IdDependiente = item.IdDependiente;
                            dependiente.Nombre = item.Nombre;
                            dependiente.ApellidoPaterno = item.ApellidoPaterno;
                            dependiente.ApellidoMaterno = item.ApellidoMaterno;
                            dependiente.FechaNacimiento = item.FechaNacimiento.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture); 
                            dependiente.Genero = char.Parse(item.Genero.Trim());
                            dependiente.Telefono = item.Telefono;
                            dependiente.Rfc = item.Rfc;

                            dependiente.EstadoCivil.Nombre = item.EstadoCivilNombre;

                            dependiente.NombreCompleto = $"{item.Nombre} {item.ApellidoPaterno}, {item.ApellidoMaterno}";

                            dependiente.DependienteTipo.IdDependienteTipo = item.IdDependiente;
                            //dependiente.DependienteTipo.Nombre = item.DependienteTipoNombre;

                            dependiente.Empleado.NumeroEmpleado = item.NumeroEmpleado;


                            result.Objects.Add(dependiente);
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


        public static Result GetById(int IdDependiente)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DbernalProgramacionNcapasContext context = new DbernalProgramacionNcapasContext())
                {
                    var execute = context.Dependientes.FromSqlInterpolated($"DependienteGet @IdDependiente={IdDependiente}").AsEnumerable().SingleOrDefault();

                    if (execute != null)
                    {

                        ML.Dependiente dependiente = new ML.Dependiente();
                        dependiente.DependienteTipo = new ML.DependienteTipo();
                        dependiente.Empleado = new ML.Empleado();
                        dependiente.EstadoCivil = new ML.EstadoCivil(); 

                        dependiente.IdDependiente = execute.IdDependiente;
                        dependiente.Nombre = execute.Nombre;
                        dependiente.ApellidoPaterno = execute.ApellidoPaterno;
                        dependiente.ApellidoMaterno = execute.ApellidoMaterno;
                        dependiente.FechaNacimiento = execute.FechaNacimiento.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture); ;
                        dependiente.Genero = char.Parse(execute.Genero.Trim());
                        dependiente.Telefono = execute.Telefono;
                        dependiente.Rfc = execute.Rfc;

                        dependiente.EstadoCivil.Nombre = execute.EstadoCivilNombre;

                        dependiente.NombreCompleto = $"{execute.Nombre} {execute.ApellidoPaterno}, {execute.ApellidoMaterno}";

                        dependiente.DependienteTipo.IdDependienteTipo = (Int32)execute.IdDependienteTipo;
                        //dependiente.DependienteTipo.Nombre = execute.DependienteTipoNombre;

                        dependiente.Empleado.NumeroEmpleado = execute.NumeroEmpleado;

                        result.Object = dependiente;

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

        public static Result Add(ML.Dependiente dependiente)
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
                    var IdDependiente = new SqlParameter
                    {
                        ParameterName = "@IdDependiente",
                        DbType = DbType.Int32,
                        Direction = ParameterDirection.Output
                    };
                    int execute = context.Database.ExecuteSqlInterpolated($"DependienteAdd @NumeroEmpleado={dependiente.Empleado.NumeroEmpleado}, @Nombre={dependiente.Nombre}, @ApellidoPaterno ={dependiente.ApellidoPaterno}, @ApellidoMaterno={dependiente.ApellidoMaterno}, @FechaNacimiento ={dependiente.FechaNacimiento}, @EstadoCivil={dependiente.EstadoCivil}, @Genero={dependiente.Genero}, @Telefono={dependiente.Telefono}, @RCF={dependiente.Rfc}, @IdDependienteTipo={dependiente.DependienteTipo.IdDependienteTipo}, @Mensaje={Mensaje} OUT, @IdDependiente={IdDependiente} OUT");

                    string message = Convert.ToString(Mensaje.Value);
                    result.Message = $"Mensaje: {message}";

                    if (execute > 0)
                    {
                        // Recuperamos el valor del parámetro que nos devuelve la base de datos
                        int idDependiente = Convert.ToInt32(IdDependiente.Value);

                        result.Object = new ML.Dependiente
                        {
                            IdDependiente = idDependiente
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

        public static Result Update(ML.Dependiente dependiente)
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

                    // Ejecución de la Query
                    int execute = context.Database.ExecuteSqlInterpolated($"DependienteUpdate @NumeroEmpleado={dependiente.IdDependiente}, @IdEmpleado ={dependiente.Empleado.NumeroEmpleado}, @Nombre={dependiente.Nombre}, @ApellidoPaterno ={dependiente.ApellidoPaterno}, @ApellidoMaterno={dependiente.ApellidoMaterno}, @FechaNacimiento ={dependiente.FechaNacimiento}, @EstadoCivil={dependiente.EstadoCivil}, @Genero={dependiente.Genero}, @Telefono={dependiente.Telefono}, @RCF={dependiente.Rfc}, @IdDependienteTipo={dependiente.DependienteTipo.IdDependienteTipo}, @Mensaje={Mensaje} OUT");

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

        public static Result Delete(int IdDependiente)
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

                    int execute = context.Database.ExecuteSqlInterpolated($"DependienteDelete @IdDependiente={IdDependiente}, @Mensaje={Mensaje} OUT");

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
