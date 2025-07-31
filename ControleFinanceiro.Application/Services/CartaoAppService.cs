using System;
using System.Collections.Generic;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;

namespace ControleFinanceiro.Application.Services
{
    public class CartaoAppService : ICartaoAppService
    {
        private readonly ICartaoRepository _repository;

        public CartaoAppService(ICartaoRepository repository)
        {
            _repository = repository;
        }

        public void Add(Cartao cartao)
        {
            if (cartao.Limite <= 0)
            {
                throw new InvalidOperationException("Limite deve ser maior que zero.");
            }
            if (cartao.DataValidade <= DateTime.Today)
            {
                throw new InvalidOperationException("Data de validade inválida.");
            }
            _repository.Add(cartao);
        }

        public void Update(Cartao cartao)
        {
            if (cartao.Limite <= 0)
            {
                throw new InvalidOperationException("Limite deve ser maior que zero.");
            }
            if (cartao.DataValidade <= DateTime.Today)
            {
                throw new InvalidOperationException("Data de validade inválida.");
            }
            _repository.Update(cartao);
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public Cartao GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<Cartao> GetByPessoa(Guid pessoaId)
        {
            return _repository.GetByPessoa(pessoaId);
        }

        public IEnumerable<Cartao> GetVencendoAte(DateTime data)
        {
            return _repository.GetVencendoAte(data);
        }
    }
}
