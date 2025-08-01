using System;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Domain.Entities
{
    public class Cartao : BaseEntity
    {
        [Required]
        public Guid PessoaId { get; set; }

        [Required]
        [StringLength(16)]
        public string Numero { get; set; }

        [Required]
        [StringLength(20)]
        public string Bandeira { get; set; }

        [StringLength(4)]
        public string NumeroFinal { get; set; }

        [Required]
        [StringLength(100)]
        public string NomeImpresso { get; set; }

        [Required]
        public DateTime DataValidade { get; set; }

        [Required]
        [Range(0, 9999)]
        public int CodigoSeguranca { get; set; }

        [Required]
        public decimal Limite { get; set; }

        public Pessoa Pessoa { get; set; }
    }
}
