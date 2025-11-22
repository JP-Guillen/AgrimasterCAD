using AgrimasterCAD.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AgrimasterCAD.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<SolicitudPlano> SolicitudesPlano { get; set; }
    public DbSet<SolicitudDocumento> SolicitudDocumentos { get; set; }
    public DbSet<Pago> Pagos { get; set; }
    public DbSet<PlanoFinal> PlanosFinales { get; set; }
    public DbSet<Notificacion> Notificaciones { get; set; }
    public DbSet<MetodoPagoCliente> MetodosPagoCliente { get; set; }
    public DbSet<EstadoActividad> EstadoActividades { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // ============================
        // SolicitudPlano -> Cliente
        // ============================
        builder.Entity<SolicitudPlano>()
            .HasOne(s => s.Cliente)
            .WithMany()
            .HasForeignKey(s => s.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        // ============================
        // SolicitudPlano -> Agrimensor (opcional)
        // ============================
        builder.Entity<SolicitudPlano>()
            .HasOne(s => s.Agrimensor)
            .WithMany()
            .HasForeignKey(s => s.AgrimensorId)
            .OnDelete(DeleteBehavior.Restrict);

        // ============================
        // SolicitudDocumento -> SolicitudPlano
        // ============================
        builder.Entity<SolicitudDocumento>()
            .HasOne(d => d.Solicitud)
            .WithMany(s => s.Documentos)
            .HasForeignKey(d => d.SolicitudId)
            .OnDelete(DeleteBehavior.Cascade);

        // ============================
        // EstadoActividad -> SolicitudPlano
        // ============================
        builder.Entity<EstadoActividad>()
            .HasOne(a => a.Solicitud)
            .WithMany(s => s.Actividades)
            .HasForeignKey(a => a.SolicitudId)
            .OnDelete(DeleteBehavior.Cascade);

        // ============================
        // Pago -> SolicitudPlano
        // ============================
        builder.Entity<Pago>()
            .HasOne(p => p.Solicitud)
            .WithMany()
            .HasForeignKey(p => p.SolicitudId)
            .OnDelete(DeleteBehavior.Cascade);

        // ============================
        // PlanoFinal -> SolicitudPlano
        // ============================
        builder.Entity<PlanoFinal>()
            .HasOne(p => p.Solicitud)
            .WithMany()
            .HasForeignKey(p => p.SolicitudId)
            .OnDelete(DeleteBehavior.Cascade);

        // ============================
        // MetodoPagoCliente -> User
        // ============================
        builder.Entity<MetodoPagoCliente>()
            .HasOne(m => m.User)
            .WithMany()
            .HasForeignKey(m => m.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // ============================
        // Notificacion -> User
        // ============================
        builder.Entity<Notificacion>()
            .HasOne(n => n.User)
            .WithMany()
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

