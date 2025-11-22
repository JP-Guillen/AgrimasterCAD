using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgrimasterCAD.Migrations
{
    /// <inheritdoc />
    public partial class AddApplicationUserFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Municipio",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombreCompleto",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NotificacionesEmail",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "NumeroCodia",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Provincia",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Direccion",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Municipio",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NombreCompleto",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NotificacionesEmail",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NumeroCodia",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Provincia",
                table: "AspNetUsers");
        }
    }
}
