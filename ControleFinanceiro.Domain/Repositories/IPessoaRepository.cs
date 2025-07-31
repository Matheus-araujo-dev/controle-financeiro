using System;
using System.Collections.Generic;
using ControleFinanceiro.Domain.Entities;

namespace ControleFinanceiro.Domain.Repositories
{
    public interface IPessoaRepository
    {
        Pessoa GetById(Guid id);
        IEnumerable<Pessoa> GetAll();
        void Add(Pessoa pessoa);
        void Update(Pessoa pessoa);
        void Delete(Guid id);

        IEnumerable<Pessoa> GetByNome(string nome);
        Pessoa GetByDocumento(string documento);
    }
}
