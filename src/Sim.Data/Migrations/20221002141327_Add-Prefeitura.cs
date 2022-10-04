using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sim.Data.Migrations
{
    public partial class AddPrefeitura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Owner",
                table: "Tipos",
                newName: "Tipo");

            migrationBuilder.RenameColumn(
                name: "Owner",
                table: "Secretaria",
                newName: "Acronimo");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Tipos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Secretaria",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Prefeitura",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(max)", nullable: true),
                    Cidade = table.Column<string>(type: "varchar(max)", nullable: true),
                    UF = table.Column<string>(type: "varchar(2)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prefeitura", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tipos_OwnerId",
                table: "Tipos",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Secretaria_OwnerId",
                table: "Secretaria",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Secretaria_Prefeitura_OwnerId",
                table: "Secretaria",
                column: "OwnerId",
                principalTable: "Prefeitura",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tipos_Secretaria_OwnerId",
                table: "Tipos",
                column: "OwnerId",
                principalTable: "Secretaria",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Secretaria_Prefeitura_OwnerId",
                table: "Secretaria");

            migrationBuilder.DropForeignKey(
                name: "FK_Tipos_Secretaria_OwnerId",
                table: "Tipos");

            migrationBuilder.DropTable(
                name: "Prefeitura");

            migrationBuilder.DropIndex(
                name: "IX_Tipos_OwnerId",
                table: "Tipos");

            migrationBuilder.DropIndex(
                name: "IX_Secretaria_OwnerId",
                table: "Secretaria");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Tipos");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Secretaria");

            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "Tipos",
                newName: "Owner");

            migrationBuilder.RenameColumn(
                name: "Acronimo",
                table: "Secretaria",
                newName: "Owner");
        }
    }
}
