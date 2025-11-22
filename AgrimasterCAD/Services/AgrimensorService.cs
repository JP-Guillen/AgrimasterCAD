using AgrimasterCAD.Data;
using AgrimasterCAD.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AgrimasterCAD.Services;

public class AgrimensorService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AgrimensorService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<List<ApplicationUser>> ObtenerAgrimensoresAsync()
    {
        return await _userManager.Users
            .Where(u => u.NumeroCodia != null)
            .ToListAsync();
    }
}
