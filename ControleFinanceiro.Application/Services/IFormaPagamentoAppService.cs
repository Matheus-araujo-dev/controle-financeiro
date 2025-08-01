using System;
using System.Collections.Generic;
using ControleFinanceiro.Domain.Entities;

namespace ControleFinanceiro.Application.Services
{
    public interface IFormaPagamentoAppService
    {
        void Add(FormaPagamento forma);
        void Update(FormaPagamento forma);
        void Delete(Guid id);
        FormaPagamento GetById(Guid id);
        IEnumerable<FormaPagamento> GetByPessoa(Guid pessoaId);
        IEnumerable<FormaPagamento> GetByCartao(Guid cartaoId);
    }
}
