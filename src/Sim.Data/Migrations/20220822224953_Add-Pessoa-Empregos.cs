using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sim.Data.Migrations
{
    public partial class AddPessoaEmpregos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PessoaId",
                table: "Empregos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empregos_PessoaId",
                table: "Empregos",
                column: "PessoaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Empregos_Pessoa_PessoaId",
                table: "Empregos",
                column: "PessoaId",
                principalTable: "Pessoa",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empregos_Pessoa_PessoaId",
                table: "Empregos");

            migrationBuilder.DropIndex(
                name: "IX_Empregos_PessoaId",
                table: "Empregos");

            migrationBuilder.DropColumn(
                name: "PessoaId",
                table: "Empregos");
        }
    }
}
