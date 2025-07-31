using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ControleFinanceiro.Infrastructure.Data;

#nullable disable

namespace ControleFinanceiro.Infrastructure.Data.Migrations
{
    [DbContext(typeof(FinanceiroDbContext))]
    partial class FinanceiroDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("ControleFinanceiro.Domain.Entities.Cartao", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<int>("CodigoSeguranca")
                    .HasColumnType("int");

                b.Property<DateTime>("DataValidade")
                    .HasColumnType("datetime2");

                b.Property<string>("NomeImpresso")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.Property<string>("Numero")
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasColumnType("nvarchar(16)");

                b.Property<Guid>("PessoaId")
                    .HasColumnType("uniqueidentifier");

                b.Property<decimal>("Limite")
                    .HasColumnType("decimal(18,2)");

                b.HasKey("Id");
                b.HasIndex("PessoaId");

                b.ToTable("Cartoes");
            });

            modelBuilder.Entity("ControleFinanceiro.Domain.Entities.ContaBancaria", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("Agencia")
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnType("nvarchar(20)");

                b.Property<string>("Banco")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.Property<string>("Numero")
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnType("nvarchar(20)");

                b.Property<Guid>("PessoaId")
                    .HasColumnType("uniqueidentifier");

                b.HasKey("Id");
                b.HasIndex("PessoaId");

                b.ToTable("ContasBancarias");
            });

            modelBuilder.Entity("ControleFinanceiro.Domain.Entities.ContaPagar", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<DateTime?>("DataPagamento")
                    .HasColumnType("datetime2");

                b.Property<DateTime>("DataVencimento")
                    .HasColumnType("datetime2");

                b.Property<string>("Descricao")
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnType("nvarchar(200)");

                b.Property<bool>("EstaPaga")
                    .HasColumnType("bit");

                b.Property<Guid>("PessoaId")
                    .HasColumnType("uniqueidentifier");

                b.Property<decimal>("Valor")
                    .HasColumnType("decimal(18,2)");

                b.HasKey("Id");

                b.HasIndex("PessoaId");

                b.ToTable("ContasPagar");
            });

            modelBuilder.Entity("ControleFinanceiro.Domain.Entities.ContaReceber", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<DateTime?>("DataRecebimento")
                    .HasColumnType("datetime2");

                b.Property<DateTime>("DataVencimento")
                    .HasColumnType("datetime2");

                b.Property<string>("Descricao")
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnType("nvarchar(200)");

                b.Property<bool>("EstaRecebido")
                    .HasColumnType("bit");

                b.Property<Guid>("PessoaId")
                    .HasColumnType("uniqueidentifier");

                b.Property<decimal>("Valor")
                    .HasColumnType("decimal(18,2)");

                b.HasKey("Id");

                b.HasIndex("PessoaId");

                b.ToTable("ContasReceber");
            });

            modelBuilder.Entity("ControleFinanceiro.Domain.Entities.MovimentacaoFinanceira", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<DateTime>("Data")
                    .HasColumnType("datetime2");

                b.Property<string>("Descricao")
                    .HasMaxLength(200)
                    .HasColumnType("nvarchar(200)");

                b.Property<Guid>("ContaBancariaId")
                    .HasColumnType("uniqueidentifier");

                b.Property<Guid>("PessoaId")
                    .HasColumnType("uniqueidentifier");

                b.Property<int>("Tipo")
                    .HasColumnType("int");

                b.Property<decimal>("Valor")
                    .HasColumnType("decimal(18,2)");

                b.HasKey("Id");
                b.HasIndex("ContaBancariaId");

                b.HasIndex("PessoaId");

                b.ToTable("MovimentacoesFinanceiras");
            });

            modelBuilder.Entity("ControleFinanceiro.Domain.Entities.Transaction", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<Guid>("ContaDestinoId")
                    .HasColumnType("uniqueidentifier");

                b.Property<Guid>("ContaOrigemId")
                    .HasColumnType("uniqueidentifier");

                b.Property<DateTime>("Data")
                    .HasColumnType("datetime2");

                b.Property<string>("Descricao")
                    .HasMaxLength(200)
                    .HasColumnType("nvarchar(200)");

                b.Property<decimal>("Valor")
                    .HasColumnType("decimal(18,2)");

                b.HasKey("Id");

                b.HasIndex("ContaDestinoId");

                b.HasIndex("ContaOrigemId");

                b.ToTable("Transactions");
            });

            modelBuilder.Entity("ControleFinanceiro.Domain.Entities.Pessoa", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<DateTime?>("DataNascimento")
                    .HasColumnType("datetime2");

                b.Property<string>("Documento")
                    .IsRequired()
                    .HasMaxLength(14)
                    .HasColumnType("nvarchar(14)");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Nome")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.HasKey("Id");

                b.ToTable("Pessoas");
            });
#pragma warning restore 612, 618
        }
    }
}
