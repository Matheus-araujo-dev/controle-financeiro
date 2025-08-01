using System;
using System.Collections.Generic;
using ControleFinanceiro.Domain.Entities;

namespace ControleFinanceiro.Application.Services
{
    public interface ICategoriaAppService
    {
        Categoria GetById(Guid id);
        IEnumerable<Categoria> GetByPessoa(Guid pessoaId);
        void Add(Categoria categoria);
        void Update(Categoria categoria);
        void Delete(Guid id);
    }
}
