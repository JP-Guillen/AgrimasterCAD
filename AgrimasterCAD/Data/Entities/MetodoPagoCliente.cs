namespace AgrimasterCAD.Data.Entities;

public class MetodoPagoCliente
{
    public int Id { get; set; }

    public string UserId { get; set; } = default!;
    public ApplicationUser User { get; set; } = default!;

    public string Tipo { get; set; } = string.Empty;
    public string NumeroEnmascarado { get; set; } = string.Empty;
    public string? Token { get; set; }
}
