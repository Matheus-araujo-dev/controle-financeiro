using System;
using System.Collections.Generic;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;

namespace ControleFinanceiro.Application.Services
{
    public class ContaBancariaAppService : IContaBancariaAppService
    {
        private readonly IContaBancariaRepository _repository;

        public ContaBancariaAppService(IContaBancariaRepository repository)
        {
            _repository = repository;
        }

        public void Add(ContaBancaria conta)
        {
            if (string.IsNullOrWhiteSpace(conta.Banco) ||
                string.IsNullOrWhiteSpace(conta.Agencia) ||
                string.IsNullOrWhiteSpace(conta.Numero))
            {
                throw new InvalidOperationException("Banco, Agencia e Numero sao obrigatorios.");
            }
            _repository.Add(conta);
        }

        public void Update(ContaBancaria conta)
        {
            if (string.IsNullOrWhiteSpace(conta.Banco) ||
                string.IsNullOrWhiteSpace(conta.Agencia) ||
                string.IsNullOrWhiteSpace(conta.Numero))
            {
                throw new InvalidOperationException("Banco, Agencia e Numero sao obrigatorios.");
            }
            _repository.Update(conta);
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public ContaBancaria GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<ContaBancaria> GetByPessoa(Guid pessoaId)
        {
            return _repository.GetByPessoa(pessoaId);
        }
    }
}
