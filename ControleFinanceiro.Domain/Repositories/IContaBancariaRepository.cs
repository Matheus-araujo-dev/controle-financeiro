using System;
using System.Collections.Generic;
using ControleFinanceiro.Domain.Entities;

namespace ControleFinanceiro.Domain.Repositories
{
    public interface IContaBancariaRepository
    {
        ContaBancaria GetById(Guid id);
        IEnumerable<ContaBancaria> GetByPessoa(Guid pessoaId);
        void Add(ContaBancaria conta);
        void Update(ContaBancaria conta);
        void Delete(Guid id);
    }
}
