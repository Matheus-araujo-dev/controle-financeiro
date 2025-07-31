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

        public DateTime? DataPagamento { get; set; }

        public bool EstaPaga { get; set; }

        public Pessoa Pessoa { get; set; }
    }
}
