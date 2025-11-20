namespace AgrimasterCAD.Models;

public class Administrador
{
    [Key]
    public int AdministradorId { get; set; }
    public int UsuarioId { get; set; }
    public string Rol { get; set; }

    public virtual Usuario Usuario { get; set; }
}
