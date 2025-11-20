using AgrimasterCAD.DAL;
using AgrimasterCAD.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class AdministradorService(IDbContextFactory<Contexto> DbFactory)
{
    private async Task<bool> Existe(int administradorId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Administradores.AnyAsync(a => a.AdministradorId == administradorId);
    }

    private async Task<bool> Insertar(Administrador administrador)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Administradores.Add(administrador);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Administrador administrador)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(administrador);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Administrador administrador)
    {
        if (!await Existe(administrador.AdministradorId))
            return await Insertar(administrador);
        else
            return await Modificar(administrador);
    }

    public async Task<Administrador?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Administradores
            .Include(a => a.Usuario)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.AdministradorId == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Administradores
            .Where(a => a.AdministradorId == id)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Administrador>> Listar(Expression<Func<Administrador, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Administradores
            .Where(criterio)
            .Include(a => a.Usuario)
            .AsNoTracking()
            .ToListAsync();
    }
}
