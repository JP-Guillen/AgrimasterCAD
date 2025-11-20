using AgrimasterCAD.DAL;
using AgrimasterCAD.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class AgrimensorService(IDbContextFactory<Contexto> DbFactory)
{
    private async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Agrimensores.AnyAsync(a => a.AgrimensorId == id);
    }

    private async Task<bool> Insertar(Agrimensor agrimensor)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Agrimensores.Add(agrimensor);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Agrimensor agrimensor)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(agrimensor);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Agrimensor agrimensor)
    {
        if (!await Existe(agrimensor.AgrimensorId))
            return await Insertar(agrimensor);
        else
            return await Modificar(agrimensor);
    }

    public async Task<Agrimensor?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Agrimensores
            .Include(a => a.Usuario)
            .Include(a => a.Solicitudes)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.AgrimensorId == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Agrimensores
            .Where(a => a.AgrimensorId == id)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Agrimensor>> Listar(Expression<Func<Agrimensor, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Agrimensores
            .Where(criterio)
            .Include(a => a.Usuario)
            .Include(a => a.Solicitudes)
            .AsNoTracking()
            .ToListAsync();
    }
}
