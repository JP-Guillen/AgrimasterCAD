namespace AgrimasterCAD.Models;

public class Solicitud
{
    [Key]
    public int SolicitudId { get; set; }
    public int UsuarioId { get; set; }
    public int AgrimensorId { get; set; }
    public string TituloTrabajo { get; set; }
    public double Superficie { get; set; }
    public string Ubicación { get; set; }
    public string Estado { get; set; }
    public DateTime FechaSolicitud { get; set; }
    public double CostoEstimation { get; set; }
    public bool CotizaciónAceptada { get; set; }

    public virtual Usuario Usuario { get; set; }
    public virtual Agrimensor Agrimensor { get; set; }
}
