using System;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        [Required]
        public Guid PessoaId { get; set; }

        [Required]
        public TipoMovimentacao Tipo { get; set; }

        [Required]
        public decimal Valor { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [StringLength(200)]
        public string Descricao { get; set; }

        public Pessoa Pessoa { get; set; }
    }
}
