using System;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        [Required]
        public Guid ContaOrigemId { get; set; }

        [Required]
        public Guid ContaDestinoId { get; set; }

        [Required]
        public decimal Valor { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [StringLength(200)]
        public string Descricao { get; set; }

        public ContaBancaria ContaOrigem { get; set; }
        public ContaBancaria ContaDestino { get; set; }
    }
}
