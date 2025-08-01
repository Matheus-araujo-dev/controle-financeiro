using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleFinanceiro.Infrastructure.Data.Migrations
{
    public partial class AddContaIdsToMovimentacaoFinanceira : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ContaPagarId",
                table: "MovimentacoesFinanceiras",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ContaReceberId",
                table: "MovimentacoesFinanceiras",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacoesFinanceiras_ContaPagarId",
                table: "MovimentacoesFinanceiras",
                column: "ContaPagarId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacoesFinanceiras_ContaReceberId",
                table: "MovimentacoesFinanceiras",
                column: "ContaReceberId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentacoesFinanceiras_ContasPagar_ContaPagarId",
                table: "MovimentacoesFinanceiras",
                column: "ContaPagarId",
                principalTable: "ContasPagar",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentacoesFinanceiras_ContasReceber_ContaReceberId",
                table: "MovimentacoesFinanceiras",
                column: "ContaReceberId",
                principalTable: "ContasReceber",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovimentacoesFinanceiras_ContasPagar_ContaPagarId",
                table: "MovimentacoesFinanceiras");

            migrationBuilder.DropForeignKey(
                name: "FK_MovimentacoesFinanceiras_ContasReceber_ContaReceberId",
                table: "MovimentacoesFinanceiras");

            migrationBuilder.DropIndex(
                name: "IX_MovimentacoesFinanceiras_ContaPagarId",
                table: "MovimentacoesFinanceiras");

            migrationBuilder.DropIndex(
                name: "IX_MovimentacoesFinanceiras_ContaReceberId",
                table: "MovimentacoesFinanceiras");

            migrationBuilder.DropColumn(
                name: "ContaPagarId",
                table: "MovimentacoesFinanceiras");

            migrationBuilder.DropColumn(
                name: "ContaReceberId",
                table: "MovimentacoesFinanceiras");
        }
    }
}
