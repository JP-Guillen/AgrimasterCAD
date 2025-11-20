namespace AgrimasterCAD.Models;

public class Agrimensor
{
    [Key]
    public int AgrimensorId { get; set; }
    public int UsuarioId { get; set; }
    public string CodigoCodia { get; set; }
    public string Telefono { get; set; }
    public DateTime FechaAsignacion { get; set; }

    public virtual Usuario Usuario { get; set; }
}
