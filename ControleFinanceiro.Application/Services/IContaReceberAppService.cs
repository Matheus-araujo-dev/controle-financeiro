using System;
using System.Collections.Generic;
using ControleFinanceiro.Domain.Entities;

namespace ControleFinanceiro.Application.Services
{
    public interface IContaReceberAppService
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
