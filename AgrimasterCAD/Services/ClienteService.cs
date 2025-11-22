using AgrimasterCAD.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AgrimasterCAD.Services;

public class ClienteService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ClienteService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<List<ApplicationUser>> ObtenerClientesAsync()
    {
        return await _userManager.Users
            .Where(u => u.NumeroCodia == null)
            .ToListAsync();
    }
}
