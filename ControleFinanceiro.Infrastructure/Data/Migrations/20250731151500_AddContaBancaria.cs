using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleFinanceiro.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddContaBancaria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContasBancarias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PessoaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Banco = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Agencia = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContasBancarias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContasBancarias_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddColumn<Guid>(
                name: "ContaBancariaId",
                table: "MovimentacoesFinanceiras",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContaOrigemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContaDestinoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_ContasBancarias_ContaDestinoId",
                        column: x => x.ContaDestinoId,
                        principalTable: "ContasBancarias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_ContasBancarias_ContaOrigemId",
                        column: x => x.ContaOrigemId,
                        principalTable: "ContasBancarias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContasBancarias_PessoaId",
                table: "ContasBancarias",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacoesFinanceiras_ContaBancariaId",
                table: "MovimentacoesFinanceiras",
                column: "ContaBancariaId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ContaDestinoId",
                table: "Transactions",
                column: "ContaDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ContaOrigemId",
                table: "Transactions",
                column: "ContaOrigemId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentacoesFinanceiras_ContasBancarias_ContaBancariaId",
                table: "MovimentacoesFinanceiras",
                column: "ContaBancariaId",
                principalTable: "ContasBancarias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovimentacoesFinanceiras_ContasBancarias_ContaBancariaId",
                table: "MovimentacoesFinanceiras");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "ContasBancarias");

            migrationBuilder.DropIndex(
                name: "IX_MovimentacoesFinanceiras_ContaBancariaId",
                table: "MovimentacoesFinanceiras");

            migrationBuilder.DropColumn(
                name: "ContaBancariaId",
                table: "MovimentacoesFinanceiras");
        }
    }
}
