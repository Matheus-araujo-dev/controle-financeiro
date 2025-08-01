using System;
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
        [StringLength(10)]
        public string Agencia { get; set; }

        [Required]
        [MaxLength(512)]
        public string Numero { get; set; }

        public Pessoa Pessoa { get; set; }
    }
}
