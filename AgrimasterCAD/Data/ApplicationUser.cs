using Microsoft.AspNetCore.Identity;

namespace AgrimasterCAD.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public string? NombreCompleto { get; set; }
    public string? Provincia { get; set; }
    public string? Municipio { get; set; }
    public string? Direccion { get; set; }
    public bool NotificacionesEmail { get; set; } = true;

    // Solo agrimensores
    public string? NumeroCodia { get; set; }
}
