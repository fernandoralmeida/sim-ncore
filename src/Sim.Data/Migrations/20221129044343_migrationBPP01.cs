using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sim.Data.Migrations
{
    public partial class migrationBPP01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ERenegociacoes_BPPContratos_ContratoId",
                table: "ERenegociacoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ERenegociacoes",
                table: "ERenegociacoes");

            migrationBuilder.RenameTable(
                name: "ERenegociacoes",
                newName: "BPPRenegociacoes");

            migrationBuilder.RenameIndex(
                name: "IX_ERenegociacoes_ContratoId",
                table: "BPPRenegociacoes",
                newName: "IX_BPPRenegociacoes_ContratoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BPPRenegociacoes",
                table: "BPPRenegociacoes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BPPRenegociacoes_BPPContratos_ContratoId",
                table: "BPPRenegociacoes",
                column: "ContratoId",
                principalTable: "BPPContratos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BPPRenegociacoes_BPPContratos_ContratoId",
                table: "BPPRenegociacoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BPPRenegociacoes",
                table: "BPPRenegociacoes");

            migrationBuilder.RenameTable(
                name: "BPPRenegociacoes",
                newName: "ERenegociacoes");

            migrationBuilder.RenameIndex(
                name: "IX_BPPRenegociacoes_ContratoId",
                table: "ERenegociacoes",
                newName: "IX_ERenegociacoes_ContratoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ERenegociacoes",
                table: "ERenegociacoes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ERenegociacoes_BPPContratos_ContratoId",
                table: "ERenegociacoes",
                column: "ContratoId",
                principalTable: "BPPContratos",
                principalColumn: "Id");
        }
    }
}
