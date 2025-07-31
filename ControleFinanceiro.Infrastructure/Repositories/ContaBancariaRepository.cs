using System;
using System.Collections.Generic;
using System.Linq;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;
using ControleFinanceiro.Infrastructure.Data;

namespace ControleFinanceiro.Infrastructure.Repositories
{
    public class ContaBancariaRepository : IContaBancariaRepository
    {
        private readonly FinanceiroDbContext _context;

        public ContaBancariaRepository(FinanceiroDbContext context)
        {
            _context = context;
        }

        public void Add(ContaBancaria conta)
        {
            _context.ContasBancarias.Add(conta);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var entity = _context.ContasBancarias.Find(id);
            if (entity != null)
            {
                _context.ContasBancarias.Remove(entity);
                _context.SaveChanges();
            }
        }

        public ContaBancaria GetById(Guid id)
        {
            return _context.ContasBancarias.Find(id);
        }

        public IEnumerable<ContaBancaria> GetByPessoa(Guid pessoaId)
        {
            return _context.ContasBancarias.Where(c => c.PessoaId == pessoaId).ToList();
        }

        public void Update(ContaBancaria conta)
        {
            _context.ContasBancarias.Update(conta);
            _context.SaveChanges();
        }
    }
}
