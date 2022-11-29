using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sim.Data.Migrations
{
    public partial class migrationBPP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BPPContratos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    DataSituacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Pagamento = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(255)", nullable: true),
                    AppUser = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BPPContratos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BPPContratos_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BPPContratos_Pessoa_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Pessoa",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ERenegociacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContratoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Situacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ERenegociacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ERenegociacoes_BPPContratos_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "BPPContratos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BPPContratos_ClienteId",
                table: "BPPContratos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_BPPContratos_EmpresaId",
                table: "BPPContratos",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_ERenegociacoes_ContratoId",
                table: "ERenegociacoes",
                column: "ContratoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ERenegociacoes");

            migrationBuilder.DropTable(
                name: "BPPContratos");
        }
    }
}
