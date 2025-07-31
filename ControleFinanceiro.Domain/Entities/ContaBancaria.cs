using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Domain.Entities
{
    public class ContaBancaria : BaseEntity
    {
        [Required]
        public Guid PessoaId { get; set; }

        [Required]
        [StringLength(100)]
        public string Banco { get; set; }

        [Required]
        [StringLength(20)]
        public string Agencia { get; set; }

        [Required]
        [StringLength(20)]
        public string Numero { get; set; }

        public Pessoa Pessoa { get; set; }
        public ICollection<MovimentacaoFinanceira> MovimentacoesFinanceiras { get; set; } = new List<MovimentacaoFinanceira>();
        public ICollection<Transaction> TransacoesOrigem { get; set; } = new List<Transaction>();
        public ICollection<Transaction> TransacoesDestino { get; set; } = new List<Transaction>();
    }
}
