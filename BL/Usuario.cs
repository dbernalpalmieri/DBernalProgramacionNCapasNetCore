using DL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.OleDb;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static Result GetAll(ML.Usuario usuario)
        {
            Result result = new Result();
            try
            {
                using (DbernalProgramacionNcapasContext context = new DbernalProgramacionNcapasContext())
                {
                    usuario.Rol = (usuario.Rol == null) ? new ML.Rol() : usuario.Rol;

                    var execute = context.Usuarios.FromSqlInterpolated($"UsuarioGet @IdUsuario={null}, @Nombre={usuario.Nombre}, @ApellidoPaterno={usuario.ApellidoPaterno}, @IdRol={usuario.Rol.IdRol}").ToList();
                    //Database.SqlQueryRaw<ML.Usuario>($"UsuarioGet").AsNoTracking().ToList();
                    //context.Set<ML.Usuario>().FromSqlRaw("UsuarioGet").AsNoTracking().ToList();
                    // context.Database.SqlQuery<List<ML.Usuario>>($"UsuarioGet");

                    result.Objects = new List<object>();


                    if (execute.Count > 0)
                    {
                        result.Objects = new List<object>();
                        ML.Usuario usuarioObjeto = null;


                        foreach (var item in execute)
                        {
                            usuarioObjeto = new ML.Usuario();


                            usuarioObjeto.IdUsuario = item.IdUsuario;
                            usuarioObjeto.Nombre = item.Nombre;
                            usuarioObjeto.ApellidoPaterno = item.ApellidoPaterno;
                            usuarioObjeto.ApellidoMaterno = item.ApellidoMaterno;
                            usuarioObjeto.FechaNacimiento = item.FechaNacimiento.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                            usuarioObjeto.Email = item.Email;
                            usuarioObjeto.Sexo = char.Parse(item.Sexo.Trim());
                            usuarioObjeto.Password = item.Password;
                            usuarioObjeto.UserName = item.UserName;
                            usuarioObjeto.Telefono = item.Telefono;
                            usuarioObjeto.Celular = item.Celular;
                            usuarioObjeto.Curp = item.Curp;
                            usuarioObjeto.Imagen = item.Imagen;
                            usuarioObjeto.Status = (bool)item.Status;

                            usuarioObjeto.NombreCompleto = $"{item.Nombre} {item.ApellidoPaterno} {item.ApellidoMaterno}";

                            usuarioObjeto.Rol = new ML.Rol();
                            usuarioObjeto.Rol.IdRol = item.IdRol;
                            usuarioObjeto.Rol.Nombre = item.RolNombre;

                            usuarioObjeto.Direccion = new ML.Direccion();
                            usuarioObjeto.Direccion.IdDireccion = item.IdDireccion;
                            usuarioObjeto.Direccion.Calle = item.Calle;
                            usuarioObjeto.Direccion.NumeroExterior = item.NumeroExterior;
                            usuarioObjeto.Direccion.NumeroInterior = item.NumeroInterior;

                            usuarioObjeto.Direccion.Colonia = new ML.Colonia();
                            usuarioObjeto.Direccion.Colonia.IdColonia = item.IdColonia;
                            usuarioObjeto.Direccion.Colonia.Nombre = item.ColoniaNombre;
                            usuarioObjeto.Direccion.Colonia.CodigoPostal = item.CodigoPostal;

                            usuarioObjeto.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuarioObjeto.Direccion.Colonia.Municipio.IdMunicipio = item.IdMunicipio;
                            usuarioObjeto.Direccion.Colonia.Municipio.Nombre = item.MunicipioNombre;

                            usuarioObjeto.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuarioObjeto.Direccion.Colonia.Municipio.Estado.IdEstado = item.IdEstado;
                            usuarioObjeto.Direccion.Colonia.Municipio.Estado.Nombre = item.EstadoNombre;

                            usuarioObjeto.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                            usuarioObjeto.Direccion.Colonia.Municipio.Estado.Pais.IdPais = item.IdPais;
                            usuarioObjeto.Direccion.Colonia.Municipio.Estado.Pais.Nombre = item.PaisNombre;

                            result.Objects.Add(usuarioObjeto);
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

        public static Result GetById(int IdUsuario)
        {
            Result result = new Result();
            try
            {
                using (DbernalProgramacionNcapasContext context = new DbernalProgramacionNcapasContext())
                {

                    var execute = context.Usuarios.FromSqlInterpolated($"UsuarioGet @IdUsuario={IdUsuario}").AsEnumerable().SingleOrDefault();

                    if (execute != null)
                    {

                        ML.Usuario usuario = new ML.Usuario();

                        usuario.IdUsuario = execute.IdUsuario;
                        usuario.Nombre = execute.Nombre;
                        usuario.ApellidoPaterno = execute.ApellidoPaterno;
                        usuario.ApellidoMaterno = execute.ApellidoMaterno;
                        usuario.FechaNacimiento = execute.FechaNacimiento.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        usuario.Email = execute.Email;
                        usuario.Sexo = char.Parse(execute.Sexo.Trim());
                        usuario.Password = execute.Password;
                        usuario.UserName = execute.UserName;
                        usuario.Telefono = execute.Telefono;
                        usuario.Celular = execute.Celular;
                        usuario.Curp = execute.Curp;
                        usuario.Imagen = execute.Imagen;
                        usuario.Status = execute.Status;

                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = execute.IdRol;
                        usuario.Rol.Nombre = execute.RolNombre;

                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.IdDireccion = execute.IdDireccion;
                        usuario.Direccion.Calle = execute.Calle;
                        usuario.Direccion.NumeroExterior = execute.NumeroExterior;
                        usuario.Direccion.NumeroInterior = execute.NumeroInterior;

                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.IdColonia = execute.IdColonia;
                        usuario.Direccion.Colonia.Nombre = execute.ColoniaNombre;
                        usuario.Direccion.Colonia.CodigoPostal = execute.CodigoPostal;

                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = execute.IdMunicipio;
                        usuario.Direccion.Colonia.Municipio.Nombre = execute.MunicipioNombre;

                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = execute.IdEstado;
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = execute.EstadoNombre;

                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = execute.IdPais;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = execute.PaisNombre;

                        result.Object = usuario;

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

        public static Result Add(ML.Usuario usuario)
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
                    var IdUsuario = new SqlParameter
                    {
                        ParameterName = "@IdUsuario",
                        DbType = DbType.Int32,
                        Direction = ParameterDirection.Output
                    };

                    int execute = context.Database.ExecuteSqlInterpolated($"UsuarioAdd @Nombre ={usuario.Nombre}, @ApellidoPaterno ={usuario.ApellidoPaterno}, @ApellidoMaterno={usuario.ApellidoMaterno}, @Sexo = {usuario.Sexo}, @FechaNacimiento ={usuario.FechaNacimiento}, @Email = {usuario.Email}, @Password={usuario.Password}, @UserName={usuario.UserName}, @IdRol={usuario.Rol.IdRol}, @Telefono={usuario.Telefono}, @Celular={usuario.Celular}, @CURP={usuario.Curp}, @Imagen={usuario.Imagen}, @Calle={usuario.Direccion.Calle}, @NumeroInterior={usuario.Direccion.NumeroInterior}, @NumeroExterior={usuario.Direccion.NumeroExterior}, @IdColonia={usuario.Direccion.Colonia.IdColonia}, @Mensaje={Mensaje} OUT, @IdUsuario={IdUsuario} OUT");

                    // Recuperamos el valor del parámetro que nos devuelve la base de datos
                    string message = Convert.ToString(Mensaje.Value);
                    result.Message = $"Mensaje: {message}";

                    if (execute > 0)
                    {
                        // Recuperamos el valor del parámetro que nos devuelve la base de datos
                        int idUsuario = Convert.ToInt32(IdUsuario.Value);
                        
                        result.Object = new ML.Usuario
                        {
                            IdUsuario = idUsuario
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

        public static Result Delete(int IdUsuario)
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

                    int execute = context.Database.ExecuteSqlInterpolated($"UsuarioDelete @IdUsuario={IdUsuario}, @Mensaje={Mensaje} OUT");

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

        public static Result Update(ML.Usuario usuario)
        {
            Result result = new Result();
            try
            {
                using (DbernalProgramacionNcapasContext context = new DbernalProgramacionNcapasContext())
                {
                    // Parámetros que nos regresa el procedimiento almacenado
                    var Mensaje = new SqlParameter {
                        ParameterName = "@Mensaje",
                        DbType = DbType.String,
                        Size = 100,
                        Direction = ParameterDirection.Output
                    };

                    // Ejecución de la Query
                    int execute = context.Database.ExecuteSqlInterpolated($"UsuarioUpdate @IdUsuario={usuario.IdUsuario}, @Nombre ={usuario.Nombre}, @ApellidoPaterno ={usuario.ApellidoPaterno}, @ApellidoMaterno={usuario.ApellidoMaterno}, @Sexo = {usuario.Sexo}, @FechaNacimiento ={usuario.FechaNacimiento}, @Email = {usuario.Email}, @Password={usuario.Password}, @UserName={usuario.UserName}, @IdRol={usuario.Rol.IdRol}, @Telefono={usuario.Telefono}, @Celular={usuario.Celular}, @CURP={usuario.Curp}, @Imagen={usuario.Imagen}, @Calle={usuario.Direccion.Calle}, @NumeroInterior={usuario.Direccion.NumeroInterior}, @NumeroExterior={usuario.Direccion.NumeroExterior}, @IdColonia={usuario.Direccion.Colonia.IdColonia}, @Mensaje={Mensaje} OUT");

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

        public static ML.Result ConvertXSLXtoDataTable(string conexionString)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (OleDbConnection context = new OleDbConnection(conexionString))
                {
                    // Leemos la primera hoja del excel enviado por el usuario 
                    string query = "SELECT * FROM [Hoja1$]";
                    using (OleDbCommand cmd = new OleDbCommand(query, context))
                    {

                        //cmd.CommandText = query;
                        //cmd.Connection = context;
                        context.Open();

                        DataSet dataSet = new DataSet();
                        OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, context);

                        context.Close();

                        dataAdapter.Fill(dataSet);
                        //dataAdapter.SelectCommand = cmd;

                        DataTable tableUsuario = dataSet.Tables[0];
                        //dataAdapter.Fill(tableUsuario);

                        if (tableUsuario.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow row in tableUsuario.Rows) //
                            {
                                ML.Usuario usuario = new ML.Usuario();

                                usuario.Nombre = row[0].ToString().TrimEnd();
                                usuario.ApellidoPaterno = row[1].ToString().TrimEnd();
                                usuario.ApellidoMaterno = row[2].ToString().TrimEnd();
                                usuario.FechaNacimiento = row[3].ToString().TrimEnd();
                                usuario.Email = row[4].ToString().Trim();

                                
                                
                                usuario.Sexo = (row[5].ToString().IsNullOrEmpty() || row[5].ToString().Length > 1 || !(row[5].ToString().ToUpper().Equals("M") || row[5].ToString().ToUpper().Equals("F")) ) ? '\0' : char.Parse(row[5].ToString().Trim());

                                usuario.Password = row[6].ToString().TrimEnd();
                                usuario.UserName = row[7].ToString().Trim();
                                usuario.Telefono = (row[8].ToString().IsNullOrEmpty() || !long.TryParse(row[8].ToString(), out _)) ?  "" : row[8].ToString().TrimEnd();
                                usuario.Celular = (row[9].ToString().IsNullOrEmpty() || !long.TryParse(row[9].ToString(), out _)) ? "" :  row[9].ToString().TrimEnd();
                                usuario.Curp = row[10].ToString().Trim();
                                usuario.Imagen = null;

                                usuario.Rol = new ML.Rol();
                                byte tempResult = 0;
                                usuario.Rol.IdRol = (row[11].ToString().IsNullOrEmpty() || !byte.TryParse(row[11].ToString(), out _)) ?  Convert.ToByte(0) : Convert.ToByte(row[11].ToString());
                                //byte.Parse(row[11].ToString());

                                usuario.Direccion = new ML.Direccion();
                                usuario.Direccion.Calle = row[12].ToString();
                                usuario.Direccion.NumeroExterior = row[13].ToString();
                                usuario.Direccion.NumeroInterior = row[14].ToString();

                                usuario.Direccion.Colonia = new ML.Colonia();
                                usuario.Direccion.Colonia.IdColonia = (row[15].ToString().IsNullOrEmpty() || !int.TryParse(row[15].ToString(),out _)) ? 0 : int.Parse(row[15].ToString());

                                result.Objects.Add(usuario);
                            }
                        }

                        result.Object = tableUsuario;

                        if (tableUsuario.Rows.Count > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.Message = "No existen registros en el excel";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;

            }

            return result;

        }

        public static ML.Result ValidarExcel(List<object> Object)
        {
            ML.Result result = new ML.Result();

            try
            {
                result.Objects = new List<object>();
                //DataTable  //Rows //Columns
                int i = 1;
                foreach (ML.Usuario usuario in Object)
                {
                    ML.ErrorExcel error = new ML.ErrorExcel();
                    error.IdRegistro = i++;

                    usuario.Nombre = (usuario.Nombre == "") ? error.Mensaje += "Ingresar el Nombre del Usuario\n " : usuario.Nombre;
                    usuario.ApellidoPaterno = (usuario.ApellidoPaterno == "") ? error.Mensaje += "Ingresar el Apellido Paterno del Usuario\n " : usuario.ApellidoPaterno;
                    usuario.ApellidoMaterno = (usuario.ApellidoMaterno == "") ? error.Mensaje += "Ingresar el Apellido Materno del Usuario\n " : usuario.ApellidoMaterno;
                    usuario.FechaNacimiento = (usuario.FechaNacimiento == "") ? error.Mensaje += "Ingresar la Fecha de Nacimiento del Usuario\n " : usuario.FechaNacimiento;
                    usuario.Email = (usuario.Email == "") ? error.Mensaje += "Ingresar el Email del Usuario \n" : usuario.Email;
                    if (usuario.Sexo == '\0') 
                    {
                        error.Mensaje += $"Ingresa el Sexo del Usuario\n ";
                    }
                       
                    usuario.Password = (usuario.Password == "") ? error.Mensaje += "Ingresar la Contraseña del Usuario\n " : usuario.Password;
                    usuario.UserName = (usuario.UserName == "") ? error.Mensaje += "Ingresar el User Name del Usuario\n " : usuario.UserName;
                    usuario.Telefono = (usuario.Telefono == "") ? error.Mensaje += "Ingresar el Teléfono del Usuario\n " : usuario.Telefono;
                    usuario.Celular = (usuario.Celular == "") ? error.Mensaje += "Ingresar el Celular del Usuario\n " : usuario.Telefono;
                    usuario.Curp = (usuario.Curp == "") ? error.Mensaje += "Ingresar el Curp del Usuario\n " : usuario.Telefono;
                    usuario.Imagen = null;
                    if (usuario.Rol.IdRol == 0)
                    {
                        error.Mensaje += $"Ingresar el Id Rol del Usuario\n ";
                    }
                    usuario.Direccion.Calle = (usuario.Direccion.Calle == "") ? error.Mensaje += $"Ingresar la calle del Usuario\n " : usuario.Direccion.Calle;
                    usuario.Direccion.NumeroExterior = (usuario.Direccion.NumeroExterior == "") ? error.Mensaje += "Ingresar el Numero Exterior del Usuario\n " : usuario.Direccion.NumeroExterior;
                    usuario.Direccion.NumeroInterior = (usuario.Direccion.NumeroInterior == "") ? error.Mensaje += "Ingresar el Numero Interior del Usuario\n " : usuario.Direccion.NumeroInterior;

                    if (usuario.Direccion.Colonia.IdColonia == 0)
                    {
                        error.Mensaje += $"Ingresar el Id Colonia del Usuario\n ";
                    }

                    if (error.Mensaje != null)
                    {
                        result.Objects.Add(error);
                    }



                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;

            }

            return result;
        }         

        public static ML.Result UpdateStatus(int IdUsuario)
        {
            ML.Result result = new ML.Result();
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
                    int execute = context.Database.ExecuteSqlInterpolated($"UsuarioChangeStatus @IdUsuario={IdUsuario}, @Mensaje={Mensaje} OUT");

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
