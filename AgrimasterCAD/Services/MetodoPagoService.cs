using AgrimasterCAD.Data;
using AgrimasterCAD.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgrimasterCAD.Services;

public class MetodoPagoService
{
    private readonly ApplicationDbContext _context;

    public MetodoPagoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AgregarAsync(MetodoPagoCliente metodo)
    {
        _context.MetodosPagoCliente.Add(metodo);
        await _context.SaveChangesAsync();
    }

    public async Task EliminarAsync(int id)
    {
        var metodo = await _context.MetodosPagoCliente.FindAsync(id);
        if (metodo == null) return;

        _context.MetodosPagoCliente.Remove(metodo);
        await _context.SaveChangesAsync();
    }

    public async Task<List<MetodoPagoCliente>> ListarAsync(string userId)
    {
        return await _context.MetodosPagoCliente
            .Where(m => m.UserId == userId)
            .ToListAsync();
    }
}
