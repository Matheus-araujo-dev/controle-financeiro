using System;
using System.Collections.Generic;
using ControleFinanceiro.Domain.Entities;

namespace ControleFinanceiro.Domain.Repositories
{
    public interface IMovimentacaoFinanceiraRepository
    {
        MovimentacaoFinanceira GetById(Guid id);
        IEnumerable<MovimentacaoFinanceira> GetByPessoa(Guid pessoaId);
        IEnumerable<MovimentacaoFinanceira> GetByPeriodo(DateTime inicio, DateTime fim);
        IEnumerable<MovimentacaoFinanceira> GetByTipo(TipoMovimentacao tipo);
        void Add(MovimentacaoFinanceira movimentacao);
        void Update(MovimentacaoFinanceira movimentacao);
        void Delete(Guid id);
    }
}
