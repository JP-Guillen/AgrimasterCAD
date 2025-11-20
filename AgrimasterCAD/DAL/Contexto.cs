using AgrimasterCAD.Models;
using Microsoft.EntityFrameworkCore;

namespace AgrimasterCAD.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Agrimensor> Agrimensores { get; set; }
    public DbSet<Administrador> Administradores { get; set; }
    public DbSet<Solicitud> Solicitudes { get; set; }
    public DbSet<Plano> Planos { get; set; }
    public DbSet<Pago> Pagos { get; set; }
    public DbSet<Factura> Facturas { get; set; }
    public DbSet<Notificacion> Notificaciones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.Solicitudes)
            .WithOne(s => s.Usuario)
            .HasForeignKey(s => s.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.Notificaciones)
            .WithOne(n => n.Usuario)
            .HasForeignKey(n => n.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Agrimensor)
            .WithOne(a => a.Usuario)
            .HasForeignKey<Agrimensor>(a => a.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Administrador)
            .WithOne(ad => ad.Usuario)
            .HasForeignKey<Administrador>(ad => ad.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Agrimensor>()
            .HasMany(a => a.Solicitudes)
            .WithOne(s => s.Agrimensor)
            .HasForeignKey(s => s.AgrimensorId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Plano>()
            .HasOne(p => p.Solicitud)
            .WithOne(s => s.Plano)
            .HasForeignKey<Plano>(p => p.SolicitudId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Pago>()
            .HasOne(p => p.Solicitud)
            .WithOne(s => s.Pago)
            .HasForeignKey<Pago>(p => p.SolicitudId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Factura>()
            .HasOne(f => f.Solicitud)
            .WithOne(s => s.Factura)
            .HasForeignKey<Factura>(f => f.SolicitudId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
