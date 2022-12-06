// See https://aka.ms/new-console-template for more information
using System.Globalization;

Console.WriteLine("Hello, World!");

ReadFile();

static void ReadFile()
{
    string file = @"C:\Users\digis\Documents\DanielBernalPalmieri\SQL\LayoutUsuarios.txt";

    if (File.Exists(file))
    {
        List<string> listError = new List<string>();

        StreamReader TextFile = new StreamReader(file);
        string line;
        
        line = TextFile.ReadLine();
        // Mínimo 10 registros 
        while ((line = TextFile.ReadLine()) != null)
        {
            string[] lines = line.Split('|');

            ML.Usuario usuario = new ML.Usuario();

            usuario.Nombre = lines[0];
            usuario.ApellidoPaterno = lines[1];
            usuario.ApellidoMaterno = lines[2];
            usuario.FechaNacimiento = lines[3];
            usuario.Email = lines[4];
            usuario.Sexo = char.Parse(lines[5].Trim());
            usuario.Password = lines[6];
            usuario.UserName = lines[7];
            usuario.Telefono = lines[8];
            usuario.Celular = lines[9];
            usuario.Curp = lines[10];
            usuario.Imagen = null;

            usuario.Rol = new ML.Rol();
            usuario.Rol.IdRol = byte.Parse(lines[11]);

            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Calle = lines[12];
            usuario.Direccion.NumeroExterior = lines[13];
            usuario.Direccion.NumeroInterior = lines[14];

            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.IdColonia = int.Parse(lines[15]);

            ML.Result result = BL.Usuario.Add(usuario);

            if (result.Correct)
            {
                Console.WriteLine($"{result.Message}");
            }
            else
            {
                // Crear .txt de errores
                listError.Add(result.Message);
            }
        }
        if (listError.Count > 0) { CreteFile(listError); }

    }
}


static void CreteFile(List<string> listErrors)
{

    string fileName = @"C:\Users\digis\Documents\DanielBernalPalmieri\SQL\ErrorLog.txt";
    FileInfo file = new FileInfo(fileName);

    try
    {
        // Check if file already exists. If yes, delete it.     
        if (file.Exists)
        {
            file.Delete();
        }

        // Create a new file     
        using (StreamWriter sw = file.CreateText())
        {
            sw.WriteLine("File of errors");
            sw.WriteLine($"Created at: {DateTime.Now.ToString()}");
            sw.WriteLine("Author: DBP");
            sw.WriteLine("List of errors ! ");
            listErrors.ForEach(error =>
            {
                sw.WriteLine(error);
            });
        }

        //// Write file contents on console.     
        //using (StreamReader sr = File.OpenText(fileName))
        //{
        //    string s = "";
        //    while ((s = sr.ReadLine()) != null)
        //    {
        //        Console.WriteLine(s);
        //    }
        //}
    }
    catch (Exception error)
    {
        Console.WriteLine(error.ToString());
    }
}