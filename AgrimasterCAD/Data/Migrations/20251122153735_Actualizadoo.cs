using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgrimasterCAD.Migrations
{
    /// <inheritdoc />
    public partial class Actualizadoo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolicitudesPlano_AspNetUsers_AgrimensorId",
                table: "SolicitudesPlano");

            migrationBuilder.DropForeignKey(
                name: "FK_SolicitudesPlano_AspNetUsers_ClienteId",
                table: "SolicitudesPlano");

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitudesPlano_AspNetUsers_AgrimensorId",
                table: "SolicitudesPlano",
                column: "AgrimensorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitudesPlano_AspNetUsers_ClienteId",
                table: "SolicitudesPlano",
                column: "ClienteId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolicitudesPlano_AspNetUsers_AgrimensorId",
                table: "SolicitudesPlano");

            migrationBuilder.DropForeignKey(
                name: "FK_SolicitudesPlano_AspNetUsers_ClienteId",
                table: "SolicitudesPlano");

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitudesPlano_AspNetUsers_AgrimensorId",
                table: "SolicitudesPlano",
                column: "AgrimensorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitudesPlano_AspNetUsers_ClienteId",
                table: "SolicitudesPlano",
                column: "ClienteId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
