using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleFinanceiro.Infrastructure.Data.Migrations
{
    public partial class AddContaBancariaToMovimentacaoFinanceira : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ContaBancariaId",
                table: "MovimentacoesFinanceiras",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacoesFinanceiras_ContaBancariaId",
                table: "MovimentacoesFinanceiras",
                column: "ContaBancariaId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentacoesFinanceiras_ContasBancarias_ContaBancariaId",
                table: "MovimentacoesFinanceiras",
                column: "ContaBancariaId",
                principalTable: "ContasBancarias",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovimentacoesFinanceiras_ContasBancarias_ContaBancariaId",
                table: "MovimentacoesFinanceiras");

            migrationBuilder.DropIndex(
                name: "IX_MovimentacoesFinanceiras_ContaBancariaId",
                table: "MovimentacoesFinanceiras");

            migrationBuilder.DropColumn(
                name: "ContaBancariaId",
                table: "MovimentacoesFinanceiras");
        }
    }
}

