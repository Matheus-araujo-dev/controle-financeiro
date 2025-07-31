using System;
using System.Collections.Generic;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;

namespace ControleFinanceiro.Application.Services
{
    public class MovimentacaoFinanceiraAppService : IMovimentacaoFinanceiraAppService
    {
        private readonly IMovimentacaoFinanceiraRepository _repository;

        public MovimentacaoFinanceiraAppService(IMovimentacaoFinanceiraRepository repository)
        {
            _repository = repository;
        }

        public void Add(MovimentacaoFinanceira movimentacao)
        {
            if (movimentacao.Valor <= 0)
            {
                throw new InvalidOperationException("Valor deve ser maior que zero.");
            }
            _repository.Add(movimentacao);
        }

        public void Update(MovimentacaoFinanceira movimentacao)
        {
            if (movimentacao.Valor <= 0)
            {
                throw new InvalidOperationException("Valor deve ser maior que zero.");
            }
            _repository.Update(movimentacao);
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public MovimentacaoFinanceira GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<MovimentacaoFinanceira> GetByPessoa(Guid pessoaId)
        {
            return _repository.GetByPessoa(pessoaId);
        }

        public IEnumerable<MovimentacaoFinanceira> GetByPeriodo(DateTime inicio, DateTime fim)
        {
            return _repository.GetByPeriodo(inicio, fim);
        }

        public IEnumerable<MovimentacaoFinanceira> GetByTipo(TipoMovimentacao tipo)
        {
            return _repository.GetByTipo(tipo);
        }
    }
}
