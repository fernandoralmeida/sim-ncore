using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sim.Data.Migrations
{
    public partial class UpdateEmpregos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Experiencia",
                table: "Empregos",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "Genero",
                table: "Empregos",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Inclusivo",
                table: "Empregos",
                type: "varchar(50)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genero",
                table: "Empregos");

            migrationBuilder.DropColumn(
                name: "Inclusivo",
                table: "Empregos");

            migrationBuilder.AlterColumn<bool>(
                name: "Experiencia",
                table: "Empregos",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);
        }
    }
}
