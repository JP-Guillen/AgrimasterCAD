using AgrimasterCAD.Data;
using AgrimasterCAD.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AgrimasterCAD.Services;

public class PagosService(IDbContextFactory<ApplicationDbContext> DbFactory, R2StorageService storage)
{
    private async Task<bool> Existe(int pagoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Pagos.AnyAsync(p => p.PagoId == pagoId);
    }

    private async Task<bool> Insertar(Pagos pago)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Pagos.Add(pago);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Pagos pago)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Pagos.Update(pago);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Pagos pago)
    {
        if (!await Existe(pago.PagoId))
        {
            return await Insertar(pago);
        }
        else
        {
            return await Modificar(pago);
        }
    }

    public async Task<string?> GuardarComprobante(IBrowserFile archivo, int solicitudId)
    {
        var nombre = $"{solicitudId}_{Guid.NewGuid()}.pdf";
        var key = $"comprobantes/{nombre}";

        using var ms = new MemoryStream();
        await archivo.OpenReadStream(5 * 1024 * 1024).CopyToAsync(ms);

        var url = await storage.UploadFileAsync(ms.ToArray(), key);

        return url;
    }

    public async Task<List<Pagos>> Listar(Expression<Func<Pagos, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Pagos.Where(criterio).Include(p => p.Solicitud).ToListAsync();
    }
}
