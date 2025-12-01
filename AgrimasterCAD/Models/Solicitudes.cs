using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AgrimasterCAD.Data;

namespace AgrimasterCAD.Models;

public class Solicitudes
{
    [Key]
    public int SolicitudId { get; set; }
    public string ClienteId { get; set; }
    public string? AgrimensorId { get; set; }
    public string Estado { get; set; } = "Pendiente";
    public string Provincia { get; set; }
    public string Municipio { get; set; }
    public string Direccion { get; set; }
    public decimal? CostoFinal { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now;
    public DateTime? FechaAceptada { get; set; }
    public DateTime? FechaFinalizada { get; set; }
    public DateTime? FechaCancelada { get; set; }

    [ForeignKey(nameof(ClienteId))]
    public ApplicationUser Cliente { get; set; }

    [ForeignKey(nameof(AgrimensorId))]
    public ApplicationUser? Agrimensor { get; set; }

    public Planos? Plano { get; set; }
    public ComprobantesPago? ComprobantePago { get; set; }

    public List<SolicitudDocumentos> Documentos { get; set; } = new();
    public List<SolicitudSeguimientos> Seguimientos { get; set; } = new();
    public List<Pagos> Pagos { get; set; } = new(); 
}
