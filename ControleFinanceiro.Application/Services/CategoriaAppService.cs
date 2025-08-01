using System;
using System.Collections.Generic;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;

namespace ControleFinanceiro.Application.Services
{
    public class CategoriaAppService : ICategoriaAppService
    {
        private readonly ICategoriaRepository _repository;

        public CategoriaAppService(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        public void Add(Categoria categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria.Nome))
            {
                throw new InvalidOperationException("Nome obrigatorio.");
            }
            _repository.Add(categoria);
        }

        public void Update(Categoria categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria.Nome))
            {
                throw new InvalidOperationException("Nome obrigatorio.");
            }
            _repository.Update(categoria);
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public Categoria GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<Categoria> GetByPessoa(Guid pessoaId)
        {
            return _repository.GetByPessoa(pessoaId);
        }
    }
}
