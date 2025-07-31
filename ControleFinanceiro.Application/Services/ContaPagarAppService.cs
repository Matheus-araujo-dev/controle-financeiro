using System;
using System.Collections.Generic;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;

namespace ControleFinanceiro.Application.Services
{
    public class ContaPagarAppService : IContaPagarAppService
    {
        private readonly IContaPagarRepository _repository;

        public ContaPagarAppService(IContaPagarRepository repository)
        {
            _repository = repository;
        }

        public void Add(ContaPagar contaPagar)
        {
            if (contaPagar.Valor <= 0)
            {
                throw new InvalidOperationException("Valor deve ser maior que zero.");
            }
            _repository.Add(contaPagar);
        }

        public void Update(ContaPagar contaPagar)
        {
            if (contaPagar.Valor <= 0)
            {
                throw new InvalidOperationException("Valor deve ser maior que zero.");
            }
            _repository.Update(contaPagar);
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public ContaPagar GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<ContaPagar> GetByPessoa(Guid pessoaId)
        {
            return _repository.GetByPessoa(pessoaId);
        }

        public IEnumerable<ContaPagar> GetByPeriodo(DateTime inicio, DateTime fim)
        {
            return _repository.GetByPeriodo(inicio, fim);
        }

        public IEnumerable<ContaPagar> GetByStatus(bool estaPaga)
        {
            return _repository.GetByStatus(estaPaga);
        }
    }
}
