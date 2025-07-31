using System;
using System.Collections.Generic;
using ControleFinanceiro.Domain.Entities;

namespace ControleFinanceiro.Domain.Repositories
{
    public interface IFormaPagamentoRepository
    {
        FormaPagamento GetById(Guid id);
        IEnumerable<FormaPagamento> GetByPessoa(Guid pessoaId);
        IEnumerable<FormaPagamento> GetByCartao(Guid cartaoId);
        void Add(FormaPagamento forma);
        void Update(FormaPagamento forma);
        void Delete(Guid id);
    }
}
