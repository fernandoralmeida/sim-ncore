using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sim.Data.Migrations
{
    public partial class AddOrgRemSetorPref : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Canal_Secretaria_SecretariaId",
                table: "Canal");

            migrationBuilder.DropForeignKey(
                name: "FK_Canal_Setor_SetorId",
                table: "Canal");

            migrationBuilder.DropForeignKey(
                name: "FK_Parceiros_Secretaria_SecretariaId",
                table: "Parceiros");

            migrationBuilder.DropForeignKey(
                name: "FK_Secretaria_Prefeitura_OwnerId",
                table: "Secretaria");

            migrationBuilder.DropForeignKey(
                name: "FK_Servico_Secretaria_SecretariaId",
                table: "Servico");

            migrationBuilder.DropForeignKey(
                name: "FK_Servico_Setor_SetorId",
                table: "Servico");

            migrationBuilder.DropForeignKey(
                name: "FK_Tipos_Secretaria_OwnerId",
                table: "Tipos");

            migrationBuilder.DropTable(
                name: "Prefeitura");

            migrationBuilder.DropTable(
                name: "Setor");

            migrationBuilder.DropIndex(
                name: "IX_Servico_SecretariaId",
                table: "Servico");

            migrationBuilder.DropIndex(
                name: "IX_Secretaria_OwnerId",
                table: "Secretaria");

            migrationBuilder.DropIndex(
                name: "IX_Canal_SecretariaId",
                table: "Canal");

            migrationBuilder.DropColumn(
                name: "SecretariaId",
                table: "Servico");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Secretaria");

            migrationBuilder.DropColumn(
                name: "SecretariaId",
                table: "Canal");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Tipos",
                newName: "DominioId");

            migrationBuilder.RenameIndex(
                name: "IX_Tipos_OwnerId",
                table: "Tipos",
                newName: "IX_Tipos_DominioId");

            migrationBuilder.RenameColumn(
                name: "SetorId",
                table: "Servico",
                newName: "DominioId");

            migrationBuilder.RenameIndex(
                name: "IX_Servico_SetorId",
                table: "Servico",
                newName: "IX_Servico_DominioId");

            migrationBuilder.RenameColumn(
                name: "SecretariaId",
                table: "Parceiros",
                newName: "DominioId");

            migrationBuilder.RenameIndex(
                name: "IX_Parceiros_SecretariaId",
                table: "Parceiros",
                newName: "IX_Parceiros_DominioId");

            migrationBuilder.RenameColumn(
                name: "SetorId",
                table: "Canal",
                newName: "DominioId");

            migrationBuilder.RenameIndex(
                name: "IX_Canal_SetorId",
                table: "Canal",
                newName: "IX_Canal_DominioId");

            migrationBuilder.AddColumn<string>(
                name: "Dominio",
                table: "Secretaria",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Hierarquia",
                table: "Secretaria",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Canal_Secretaria_DominioId",
                table: "Canal",
                column: "DominioId",
                principalTable: "Secretaria",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parceiros_Secretaria_DominioId",
                table: "Parceiros",
                column: "DominioId",
                principalTable: "Secretaria",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Servico_Secretaria_DominioId",
                table: "Servico",
                column: "DominioId",
                principalTable: "Secretaria",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tipos_Secretaria_DominioId",
                table: "Tipos",
                column: "DominioId",
                principalTable: "Secretaria",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Canal_Secretaria_DominioId",
                table: "Canal");

            migrationBuilder.DropForeignKey(
                name: "FK_Parceiros_Secretaria_DominioId",
                table: "Parceiros");

            migrationBuilder.DropForeignKey(
                name: "FK_Servico_Secretaria_DominioId",
                table: "Servico");

            migrationBuilder.DropForeignKey(
                name: "FK_Tipos_Secretaria_DominioId",
                table: "Tipos");

            migrationBuilder.DropColumn(
                name: "Dominio",
                table: "Secretaria");

            migrationBuilder.DropColumn(
                name: "Hierarquia",
                table: "Secretaria");

            migrationBuilder.RenameColumn(
                name: "DominioId",
                table: "Tipos",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Tipos_DominioId",
                table: "Tipos",
                newName: "IX_Tipos_OwnerId");

            migrationBuilder.RenameColumn(
                name: "DominioId",
                table: "Servico",
                newName: "SetorId");

            migrationBuilder.RenameIndex(
                name: "IX_Servico_DominioId",
                table: "Servico",
                newName: "IX_Servico_SetorId");

            migrationBuilder.RenameColumn(
                name: "DominioId",
                table: "Parceiros",
                newName: "SecretariaId");

            migrationBuilder.RenameIndex(
                name: "IX_Parceiros_DominioId",
                table: "Parceiros",
                newName: "IX_Parceiros_SecretariaId");

            migrationBuilder.RenameColumn(
                name: "DominioId",
                table: "Canal",
                newName: "SetorId");

            migrationBuilder.RenameIndex(
                name: "IX_Canal_DominioId",
                table: "Canal",
                newName: "IX_Canal_SetorId");

            migrationBuilder.AddColumn<Guid>(
                name: "SecretariaId",
                table: "Servico",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Secretaria",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SecretariaId",
                table: "Canal",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Prefeitura",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Cidade = table.Column<string>(type: "varchar(max)", nullable: true),
                    Nome = table.Column<string>(type: "varchar(max)", nullable: true),
                    UF = table.Column<string>(type: "varchar(2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prefeitura", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Setor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SecretariaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Nome = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Setor_Secretaria_SecretariaId",
                        column: x => x.SecretariaId,
                        principalTable: "Secretaria",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Servico_SecretariaId",
                table: "Servico",
                column: "SecretariaId");

            migrationBuilder.CreateIndex(
                name: "IX_Secretaria_OwnerId",
                table: "Secretaria",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Canal_SecretariaId",
                table: "Canal",
                column: "SecretariaId");

            migrationBuilder.CreateIndex(
                name: "IX_Setor_SecretariaId",
                table: "Setor",
                column: "SecretariaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Canal_Secretaria_SecretariaId",
                table: "Canal",
                column: "SecretariaId",
                principalTable: "Secretaria",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Canal_Setor_SetorId",
                table: "Canal",
                column: "SetorId",
                principalTable: "Setor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parceiros_Secretaria_SecretariaId",
                table: "Parceiros",
                column: "SecretariaId",
                principalTable: "Secretaria",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Secretaria_Prefeitura_OwnerId",
                table: "Secretaria",
                column: "OwnerId",
                principalTable: "Prefeitura",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Servico_Secretaria_SecretariaId",
                table: "Servico",
                column: "SecretariaId",
                principalTable: "Secretaria",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Servico_Setor_SetorId",
                table: "Servico",
                column: "SetorId",
                principalTable: "Setor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tipos_Secretaria_OwnerId",
                table: "Tipos",
                column: "OwnerId",
                principalTable: "Secretaria",
                principalColumn: "Id");
        }
    }
}
