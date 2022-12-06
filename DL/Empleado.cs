using System;
using System.Collections.Generic;

namespace DL;

public partial class Empleado
{
    public string NumeroEmpleado { get; set; } = null!;

    public string Rcf { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public DateTime FechaNacimiento { get; set; }

    public string Nss { get; set; } = null!;

    public DateTime FechaIngreso { get; set; }

    public string? Foto { get; set; }

    public int IdEmpresa { get; set; }

    public virtual Empresa IdEmpresaNavigation { get; set; } = null!;

    // Agregadas
    public string EmpresaNombre { get; set; }
    public string EmpresaTelefono { get; set; }
    public string EmpresaEmail { get; set; }
    public string DireccionWeb { get; set; }
    public string? Logo { get; set; }
}
