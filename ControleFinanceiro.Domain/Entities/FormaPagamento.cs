using System;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Domain.Entities
{
    public class FormaPagamento : BaseEntity
    {
        [Required]
        public Guid PessoaId { get; set; }

        [Required]
        [StringLength(100)]
        public string Descricao { get; set; }

        public bool BaixarAutomaticamente { get; set; }

        public Guid? CartaoId { get; set; }

        public Pessoa Pessoa { get; set; }
        public Cartao Cartao { get; set; }
    }
}
