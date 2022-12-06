using System;
using System.Collections.Generic;

namespace DL;

public partial class Dependiente
{
    public int IdDependiente { get; set; }

    public string NumeroEmpleado { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public DateTime FechaNacimiento { get; set; }

    public int? EstadoCivil { get; set; }

    public string Genero { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Rfc { get; set; } = null!;

    public int? IdDependienteTipo { get; set; }

    public virtual EstadoCivil? EstadoCivilNavigation { get; set; }

    public virtual DependienteTipo? IdDependienteTipoNavigation { get; set; }

    // Agregadas
    public string DependienteTipoNombre { get; set; }
    public string EstadoCivilNombre { get; set; }
}
