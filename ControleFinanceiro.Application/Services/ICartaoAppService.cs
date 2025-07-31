using System;
using System.Collections.Generic;
using ControleFinanceiro.Domain.Entities;

namespace ControleFinanceiro.Application.Services
{
    public interface ICartaoAppService
    {
        Cartao GetById(Guid id);
        IEnumerable<Cartao> GetByPessoa(Guid pessoaId);
        IEnumerable<Cartao> GetVencendoAte(DateTime data);
        void Add(Cartao cartao);
        void Update(Cartao cartao);
        void Delete(Guid id);
    }
}
