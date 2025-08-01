using System;
using System.Collections.Generic;
using ControleFinanceiro.Domain.Entities;

namespace ControleFinanceiro.Domain.Repositories
{
    public interface ICategoriaRepository
    {
        Categoria GetById(Guid id);
        IEnumerable<Categoria> GetByPessoa(Guid pessoaId);
        void Add(Categoria categoria);
        void Update(Categoria categoria);
        void Delete(Guid id);
    }
}
