using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgrimasterCAD.Migrations
{
    /// <inheritdoc />
    public partial class InitialDomainEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MetodosPagoCliente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroEnmascarado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetodosPagoCliente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MetodosPagoCliente_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Mensaje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Leida = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notificaciones_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudesPlano",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClienteId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AgrimensorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Superficie = table.Column<double>(type: "float", nullable: false),
                    UbicacionLat = table.Column<double>(type: "float", nullable: false),
                    UbicacionLng = table.Column<double>(type: "float", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    CostoEstimado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostoFinal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FechaSolicitud = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaAceptacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaFinalizacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesPlano", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitudesPlano_AspNetUsers_AgrimensorId",
                        column: x => x.AgrimensorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SolicitudesPlano_AspNetUsers_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EstadoActividades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolicitudId = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoActividades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstadoActividades_SolicitudesPlano_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "SolicitudesPlano",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolicitudId = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Metodo = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    FechaPago = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UrlReciboTransferencia = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pagos_SolicitudesPlano_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "SolicitudesPlano",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanosFinales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolicitudId = table.Column<int>(type: "int", nullable: false),
                    UrlPlano = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlFactura = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaSubida = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanosFinales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanosFinales_SolicitudesPlano_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "SolicitudesPlano",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudDocumentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolicitudId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaSubida = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudDocumentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitudDocumentos_SolicitudesPlano_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "SolicitudesPlano",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EstadoActividades_SolicitudId",
                table: "EstadoActividades",
                column: "SolicitudId");

            migrationBuilder.CreateIndex(
                name: "IX_MetodosPagoCliente_UserId",
                table: "MetodosPagoCliente",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_UserId",
                table: "Notificaciones",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_SolicitudId",
                table: "Pagos",
                column: "SolicitudId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanosFinales_SolicitudId",
                table: "PlanosFinales",
                column: "SolicitudId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudDocumentos_SolicitudId",
                table: "SolicitudDocumentos",
                column: "SolicitudId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesPlano_AgrimensorId",
                table: "SolicitudesPlano",
                column: "AgrimensorId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesPlano_ClienteId",
                table: "SolicitudesPlano",
                column: "ClienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstadoActividades");

            migrationBuilder.DropTable(
                name: "MetodosPagoCliente");

            migrationBuilder.DropTable(
                name: "Notificaciones");

            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.DropTable(
                name: "PlanosFinales");

            migrationBuilder.DropTable(
                name: "SolicitudDocumentos");

            migrationBuilder.DropTable(
                name: "SolicitudesPlano");
        }
    }
}
