using System.ComponentModel.DataAnnotations;

namespace AgrimasterCAD.Models;

public class Plano
{
    [Key]
    public int PlanoId { get; set; }

    public int SolicitudId { get; set; }
    public string ArchivoPlano { get; set; }
    public DateTime FechaGeneracion { get; set; }

    public Solicitud Solicitud { get; set; }
}
