using System;
using System.Collections.Generic;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;

namespace ControleFinanceiro.Application.Services
{
    public class FormaPagamentoAppService : IFormaPagamentoAppService
    {
        private readonly IFormaPagamentoRepository _repository;

        public FormaPagamentoAppService(IFormaPagamentoRepository repository)
        {
            _repository = repository;
        }

        public void Add(FormaPagamento forma)
        {
            if (string.IsNullOrWhiteSpace(forma.Descricao))
            {
                throw new InvalidOperationException("Descricao obrigatoria.");
            }
            _repository.Add(forma);
        }

        public void Update(FormaPagamento forma)
        {
            if (string.IsNullOrWhiteSpace(forma.Descricao))
            {
                throw new InvalidOperationException("Descricao obrigatoria.");
            }
            _repository.Update(forma);
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public FormaPagamento GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<FormaPagamento> GetByPessoa(Guid pessoaId)
        {
            return _repository.GetByPessoa(pessoaId);
        }

        public IEnumerable<FormaPagamento> GetByCartao(Guid cartaoId)
        {
            return _repository.GetByCartao(cartaoId);
        }
    }
}
