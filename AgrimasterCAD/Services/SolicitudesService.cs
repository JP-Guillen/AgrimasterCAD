using System.Linq.Expressions;
using AgrimasterCAD.Data;
using AgrimasterCAD.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;

namespace AgrimasterCAD.Services;

public class SolicitudesService(IDbContextFactory<ApplicationDbContext> DbFactory, IWebHostEnvironment env, R2StorageService storage)
{
    private async Task<bool> Existe(int solicitudId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Solicitudes.AnyAsync(s => s.SolicitudId == solicitudId);
    }

    public async Task<bool> ExisteAgrimensor(string agrimensorId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Users.AnyAsync(u => u.Id == agrimensorId);
    }

    private async Task<bool> Insertar(Solicitudes solicitud)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Solicitudes.Add(solicitud);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Solicitudes solicitud)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var sol = await contexto.Solicitudes
            .FirstOrDefaultAsync(s => s.SolicitudId == solicitud.SolicitudId);

        if (sol == null)
            return false;

        contexto.Entry(sol).CurrentValues.SetValues(solicitud);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Solicitudes solicitud)
    {
        if (!await Existe(solicitud.SolicitudId))
        {
            return await Insertar(solicitud);
        }
        else
        {
            return await Modificar(solicitud);
        }
    }

    public async Task<bool> Eliminar(int solicitudId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var solicitud = await contexto.Solicitudes
            .Include(s => s.Documentos)
            .Include(s => s.Plano)
            .Include(s => s.ComprobantePago)
            .Include(s => s.Pagos)
            .Include(s => s.Seguimientos)
            .FirstOrDefaultAsync(s => s.SolicitudId == solicitudId);

        if (solicitud == null)
            return false;

        if (solicitud.Estado != "Pendiente")
            throw new Exception("Solo se pueden eliminar solicitudes en estado 'Pendiente'.");

        void EliminarArchivo(string? ruta)
        {
            if (string.IsNullOrWhiteSpace(ruta))
                return;

            var path = Path.Combine(env.WebRootPath, ruta.TrimStart('/'));
            if (File.Exists(path))
                File.Delete(path);
        }

        foreach (var doc in solicitud.Documentos)
            EliminarArchivo(doc.RutaArchivo);

        if (solicitud.Plano != null)
            EliminarArchivo(solicitud.Plano.RutaArchivo);

        if (solicitud.ComprobantePago != null)
            EliminarArchivo(solicitud.ComprobantePago.Metodo);

        foreach (var pago in solicitud.Pagos)
            EliminarArchivo(pago.ReciboRuta);

        contexto.Solicitudes.Remove(solicitud);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<Solicitudes>> Listar(Expression<Func<Solicitudes, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Solicitudes
        .Where(criterio)
        .Include(s => s.Cliente)
        .Include(s => s.Agrimensor)
        .Include(s => s.ComprobantePago)
        .Include(s => s.Documentos)
        .Include(s => s.Plano)
        .Include(s => s.Pagos)
        .Include(s => s.Seguimientos)
        .ToListAsync();
    }

    public async Task<Solicitudes?> Buscar(int solicitudId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Solicitudes
        .Include(s => s.Cliente)
        .Include(s => s.Agrimensor)
        .Include(s => s.ComprobantePago)
        .Include(s => s.Documentos)
        .Include(s => s.Plano)
        .Include(s => s.Pagos)
        .Include(s => s.Seguimientos)
        .FirstOrDefaultAsync(s => s.SolicitudId == solicitudId);
    }

    private async Task<string?> GuardarEnR2(IBrowserFile archivo, string folder)
    {
        using var ms = new MemoryStream();
        await archivo.OpenReadStream(5 * 1024 * 1024).CopyToAsync(ms);

        var bytes = ms.ToArray();
        var ext = Path.GetExtension(archivo.Name);
        var nombre = $"{Guid.NewGuid()}{ext}";
        var key = $"{folder}/{nombre}";

        var url = await storage.UploadFileAsync(bytes, key);

        if (url == null)
            throw new Exception($"No se pudo subir archivo a R2: {storage.LastR2Error}");

        return url;
    }

    public async Task<string> GuardarArchivo(IBrowserFile archivo, string subcarpeta)
    {
        var url = await GuardarEnR2(archivo, subcarpeta);
        return url!;
    }


    public async Task<string> GuardarPlano(int solicitudId, IBrowserFile archivo)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var solicitud = await contexto.Solicitudes
            .Include(s => s.Plano)
            .FirstOrDefaultAsync(s => s.SolicitudId == solicitudId);

        if (solicitud == null)
            throw new Exception("La solicitud no existe.");

        var ruta = await GuardarArchivo(archivo, "planos");

        if (solicitud.Plano == null)
        {
            solicitud.Plano = new Planos
            {
                SolicitudId = solicitudId,
                RutaArchivo = ruta
            };

            contexto.Planos.Add(solicitud.Plano);
        }
        else
        {
            solicitud.Plano.RutaArchivo = ruta;
            contexto.Planos.Update(solicitud.Plano);
        }

        await contexto.SaveChangesAsync();

        return ruta;
    }

    public async Task AgregarSeguimiento(int solicitudId, string comentario)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var solicitud = await contexto.Solicitudes.FirstOrDefaultAsync(s => s.SolicitudId == solicitudId);

        if (solicitud == null)
            throw new Exception("La solicitud no existe.");

        var seguimiento = new SolicitudSeguimientos
        {
            SolicitudId = solicitudId,
            Comentario = comentario,
        };

        contexto.SolicitudSeguimientos.Add(seguimiento);
        await contexto.SaveChangesAsync();
    }
}
