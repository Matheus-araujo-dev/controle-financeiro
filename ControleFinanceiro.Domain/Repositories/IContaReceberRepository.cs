using System;
using System.Collections.Generic;
using ControleFinanceiro.Domain.Entities;

namespace ControleFinanceiro.Domain.Repositories
{
    public interface IContaReceberRepository
    {
        ContaReceber GetById(Guid id);
        IEnumerable<ContaReceber> GetByPessoa(Guid pessoaId);
        IEnumerable<ContaReceber> GetByPeriodo(DateTime inicio, DateTime fim);
        IEnumerable<ContaReceber> GetByStatus(bool estaRecebido);
        void Add(ContaReceber contaReceber);
        void Update(ContaReceber contaReceber);
        void Delete(Guid id);
    }
}
