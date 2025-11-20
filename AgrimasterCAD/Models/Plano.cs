namespace AgrimasterCAD.Models;

public class Plano
{
    [Key]
    public int PlanoId { get; set; }
    public int SolicitudId { get; set; }
    public string ArchivoPlano { get; set; }
    public DateTime FechaGeneración { get; set; }

    public virtual Solicitud Solicitud { get; set; }
}
