using AgrimasterCAD.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AgrimasterCAD.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<ComprobantesPago> ComprobantesPagos { get; set; }
        public DbSet<MetodosPago> MetodosPago { get; set; }
        public DbSet<Notificaciones> Notificaciones { get; set; }
        public DbSet<Pagos> Pagos { get; set; }
        public DbSet<Planos> Planos { get; set; }
        public DbSet<Solicitudes> Solicitudes { get; set; }
        public DbSet<SolicitudDocumentos> SolicitudDocumentos { get; set; }
        public DbSet<SolicitudSeguimientos> SolicitudSeguimientos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Solicitudes>()
            .HasOne(s => s.Cliente)
            .WithMany()
            .HasForeignKey(s => s.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Solicitudes>()
            .HasOne(s => s.Agrimensor)
            .WithMany()
            .HasForeignKey(s => s.AgrimensorId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SolicitudDocumentos>()
            .HasOne(d => d.Solicitud)
            .WithMany(s => s.Documentos)
            .HasForeignKey(d => d.SolicitudId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SolicitudSeguimientos>()
            .HasOne(sg => sg.Solicitud)
            .WithMany(s => s.Seguimientos)
            .HasForeignKey(sg => sg.SolicitudId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Planos>()
            .HasOne(p => p.Solicitud)
            .WithOne(s => s.Plano)
            .HasForeignKey<Planos>(p => p.SolicitudId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ComprobantesPago>()
            .HasOne(c => c.Solicitud)
            .WithOne(s => s.ComprobantePago)
            .HasForeignKey<ComprobantesPago>(c => c.SolicitudId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Pagos>()
            .HasOne(p => p.Solicitud)
            .WithMany(s => s.Pagos)
            .HasForeignKey(p => p.SolicitudId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<MetodosPago>()
            .HasOne(m => m.Usuario)
            .WithMany()
            .HasForeignKey(m => m.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Notificaciones>()
            .HasOne(n => n.Usuario)
            .WithMany()
            .HasForeignKey(n => n.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

            var roles = new List<IdentityRole>
            {
                new IdentityRole { Id = "1", Name = "Cliente", NormalizedName = "CLIENTE", ConcurrencyStamp = "cliente-stamp" },
                new IdentityRole { Id = "2", Name = "Agrimensor", NormalizedName = "AGRIMENSOR", ConcurrencyStamp = "agrimensor-stamp" },
                new IdentityRole { Id = "3", Name = "Administrador", NormalizedName = "ADMINISTRADOR", ConcurrencyStamp = "admin-stamp" }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            var adminUser = new ApplicationUser
            {
                Id = "100",
                UserName = "admin@agrimaster.com",
                NormalizedUserName = "ADMIN@AGRIMASTER.COM",
                Email = "admin@agrimaster.com",
                NormalizedEmail = "ADMIN@AGRIMASTER.COM",
                PasswordHash = "AQAAAAIAAYagAAAAEMnRieJLtO0k/Mv75oJw3AWMP/CFhqba42LJNnLiGlLXCmqmXFJ+MM9flIMz4PXa9g==",
                SecurityStamp = "admin-security-stamp",
                ConcurrencyStamp = "admin-concurrency-stamp",

                NombreCompleto = "Administrador del sistema",
                NumeroCedula = "000-0000000-0",
                Ciudad = "N/A",
                Direccion = "N/A",
            };

            builder.Entity<ApplicationUser>().HasData(adminUser);

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "3",
                    UserId = "100"
                }
            );
        }
    }
}
