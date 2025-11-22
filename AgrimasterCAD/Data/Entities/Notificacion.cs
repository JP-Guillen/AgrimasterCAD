namespace AgrimasterCAD.Data.Entities;

public class Notificacion
{
    public int Id { get; set; }

    public string UserId { get; set; } = default!;
    public ApplicationUser User { get; set; } = default!;

    public string Mensaje { get; set; } = string.Empty;
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    public bool Leida { get; set; }
}
