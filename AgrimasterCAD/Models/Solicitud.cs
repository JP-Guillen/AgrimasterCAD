using System.ComponentModel.DataAnnotations;

namespace AgrimasterCAD.Models;

public class Solicitud
{
    [Key]
    public int SolicitudId { get; set; }
    public int UsuarioId { get; set; } 
    public int? AgrimensorId { get; set; }

    public string TituloTrabajo { get; set; }
    public double Superficie { get; set; }
    public string Ubicacion { get; set; }
    public string Estado { get; set; }

    public DateTime FechaSolicitud { get; set; }

    public double CostoEstimation { get; set; }
    public bool CotizaciónAceptada { get; set; }

    public Usuario Usuario { get; set; }
    public Agrimensor Agrimensor { get; set; }

    public Plano Plano { get; set; }
    public Pago Pago { get; set; }
    public Factura Factura { get; set; }
}
