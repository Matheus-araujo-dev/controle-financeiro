using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Domain.Entities
{
    public class Pessoa : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(14)]
        public string Documento { get; set; }

        public DateTime? DataNascimento { get; set; }

        public ICollection<Cartao> Cartoes { get; set; } = new List<Cartao>();
        public ICollection<ContaBancaria> ContasBancarias { get; set; } = new List<ContaBancaria>();
        public ICollection<ContaPagar> ContasPagar { get; set; } = new List<ContaPagar>();
        public ICollection<ContaReceber> ContasReceber { get; set; } = new List<ContaReceber>();
        public ICollection<MovimentacaoFinanceira> MovimentacoesFinanceiras { get; set; } = new List<MovimentacaoFinanceira>();
    }
}
