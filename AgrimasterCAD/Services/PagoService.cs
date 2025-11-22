using AgrimasterCAD.Data;
using AgrimasterCAD.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgrimasterCAD.Services;

public class PagoService
{
    private readonly ApplicationDbContext _context;

    public PagoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Pago> CrearPagoAsync(Pago pago)
    {
        _context.Pagos.Add(pago);
        await _context.SaveChangesAsync();
        return pago;
    }

    public async Task<Pago?> ObtenerPagoAsync(int id)
    {
        return await _context.Pagos
            .Include(p => p.Solicitud)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Pago>> PagosPorClienteAsync(string clienteId)
    {
        return await _context.Pagos
            .Include(p => p.Solicitud)
            .Where(p => p.Solicitud.ClienteId == clienteId)
            .ToListAsync();
    }

    public async Task ConfirmarPagoAsync(int pagoId)
    {
        var pago = await _context.Pagos.FindAsync(pagoId);
        if (pago == null) return;

        pago.Estado = EstadoPago.Confirmado;
        pago.FechaPago = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }
}
