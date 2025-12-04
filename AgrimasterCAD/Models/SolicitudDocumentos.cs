using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgrimasterCAD.Models;

public class SolicitudDocumentos
{
    [Key]
    public int SolicitudDocumentoId { get; set; }
    public int SolicitudId { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public string RutaArchivo { get; set; } = string.Empty;

    [ForeignKey(nameof(SolicitudId))]
    public Solicitudes? Solicitud { get; set; }
}
