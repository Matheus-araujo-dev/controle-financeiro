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
        public decimal Valor { get; set; }

        [Required]
        public DateTime DataVencimento { get; set; }

        public DateTime? DataRecebimento { get; set; }

        public bool EstaRecebido { get; set; }

        public MetodoPagamento MetodoPagamento { get; set; }

        public Pessoa Pessoa { get; set; }
    }
}
