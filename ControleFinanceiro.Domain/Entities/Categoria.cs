using System;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Domain.Entities
{
    public class Categoria : BaseEntity
    {
        [Required]
        public Guid PessoaId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [StringLength(200)]
        public string? Descricao { get; set; }

        public Pessoa Pessoa { get; set; }
    }
}
