using System;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Domain.Entities
{
    public class ContaReceber : BaseEntity
    {
        [Required]
        public Guid PessoaId { get; set; }

        [Required]
        [StringLength(200)]
        public string Descricao { get; set; }

        [Required]
        [StringLength(100)]
        public string Responsavel { get; set; }

        [Required]
        public decimal ValorTotal { get; set; }

        [Required]
        public int NumeroParcelas { get; set; }

        [Required]
        public decimal Valor { get; set; }

        [Required]
        public DateTime DataVencimento { get; set; }

        public Guid? FormaPagamentoId { get; set; }

        public FormaPagamento FormaPagamento { get; set; }

        public DateTime? DataRecebimento { get; set; }

        public bool EstaRecebido { get; set; }

        public Pessoa Pessoa { get; set; }
    }
}
