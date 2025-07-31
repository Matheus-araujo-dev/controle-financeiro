using System;
using System.Collections.Generic;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;

namespace ControleFinanceiro.Application.Services
{
    public class ContaReceberAppService : IContaReceberAppService
    {
        private readonly IContaReceberRepository _repository;

        public ContaReceberAppService(IContaReceberRepository repository)
        {
            _repository = repository;
        }

        public void Add(ContaReceber contaReceber)
        {
            if (contaReceber.Valor <= 0)
            {
                throw new InvalidOperationException("Valor deve ser maior que zero.");
            }
            _repository.Add(contaReceber);
        }

        public void Update(ContaReceber contaReceber)
        {
            if (contaReceber.Valor <= 0)
            {
                throw new InvalidOperationException("Valor deve ser maior que zero.");
            }
            _repository.Update(contaReceber);
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public ContaReceber GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<ContaReceber> GetByPessoa(Guid pessoaId)
        {
            return _repository.GetByPessoa(pessoaId);
        }

        public IEnumerable<ContaReceber> GetByPeriodo(DateTime inicio, DateTime fim)
        {
            return _repository.GetByPeriodo(inicio, fim);
        }

        public IEnumerable<ContaReceber> GetByStatus(bool estaRecebido)
        {
            return _repository.GetByStatus(estaRecebido);
        }
    }
}
