using System;
using System.Collections.Generic;
using ControleFinanceiro.Domain.Entities;

namespace ControleFinanceiro.Domain.Repositories
{
    public interface IContaPagarRepository
    {
        ContaPagar GetById(Guid id);
        IEnumerable<ContaPagar> GetByPessoa(Guid pessoaId);
        IEnumerable<ContaPagar> GetByPeriodo(DateTime inicio, DateTime fim);
        IEnumerable<ContaPagar> GetByStatus(bool estaPaga);
        void Add(ContaPagar contaPagar);
        void Update(ContaPagar contaPagar);
        void Delete(Guid id);
    }
}
