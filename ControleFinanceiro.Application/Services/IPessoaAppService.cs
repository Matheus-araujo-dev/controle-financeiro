using System;
using System.Collections.Generic;
using ControleFinanceiro.Domain.Entities;

namespace ControleFinanceiro.Application.Services
{
    public interface IPessoaAppService
    {
        Pessoa GetById(Guid id);
        IEnumerable<Pessoa> GetAll();
        IEnumerable<Pessoa> GetByNome(string nome);
        Pessoa GetByDocumento(string documento);
        void Create(Pessoa pessoa);
        void Update(Pessoa pessoa);
        void Delete(Guid id);
    }
}
