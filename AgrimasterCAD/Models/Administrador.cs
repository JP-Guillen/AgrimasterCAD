using System.ComponentModel.DataAnnotations;

namespace AgrimasterCAD.Models;

public class Administrador
{
    [Key]
    public int AdministradorId { get; set; }

    public int UsuarioId { get; set; }
    public string Rol { get; set; }

    public Usuario Usuario { get; set; }
}
