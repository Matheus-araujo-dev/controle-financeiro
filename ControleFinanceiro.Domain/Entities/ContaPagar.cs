using System;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Domain.Entities
{
    public class ContaPagar : BaseEntity
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

        public DateTime? DataPagamento { get; set; }

        public bool EstaPaga { get; set; }

        public MetodoPagamento MetodoPagamento { get; set; }

        public Pessoa Pessoa { get; set; }
    }
}
