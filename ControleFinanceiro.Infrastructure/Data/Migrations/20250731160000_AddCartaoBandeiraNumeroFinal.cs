using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleFinanceiro.Infrastructure.Data.Migrations
{
    public partial class AddCartaoBandeiraNumeroFinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bandeira",
                table: "Cartoes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NumeroFinal",
                table: "Cartoes",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bandeira",
                table: "Cartoes");

            migrationBuilder.DropColumn(
                name: "NumeroFinal",
                table: "Cartoes");
        }
    }
}

