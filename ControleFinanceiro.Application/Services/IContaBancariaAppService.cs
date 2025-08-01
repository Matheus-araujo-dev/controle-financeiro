using System;
using System.Collections.Generic;
using ControleFinanceiro.Domain.Entities;

namespace ControleFinanceiro.Application.Services
{
    public interface IContaBancariaAppService
    {
        ContaBancaria GetById(Guid id);
        IEnumerable<ContaBancaria> GetByPessoa(Guid pessoaId);
        void Add(ContaBancaria conta);
        void Update(ContaBancaria conta);
        void Delete(Guid id);
    }
}
