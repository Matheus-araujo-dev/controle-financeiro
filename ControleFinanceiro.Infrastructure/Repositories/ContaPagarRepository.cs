using System;
using System.Collections.Generic;
using System.Linq;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;
using ControleFinanceiro.Infrastructure.Data;

namespace ControleFinanceiro.Infrastructure.Repositories
{
    public class ContaPagarRepository : IContaPagarRepository
    {
        private readonly FinanceiroDbContext _context;

        public ContaPagarRepository(FinanceiroDbContext context)
        {
            _context = context;
        }

        public void Add(ContaPagar contaPagar)
        {
            _context.ContasPagar.Add(contaPagar);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var entity = _context.ContasPagar.Find(id);
            if (entity != null)
            {
                _context.ContasPagar.Remove(entity);
                _context.SaveChanges();
            }
        }

        public ContaPagar GetById(Guid id)
        {
            return _context.ContasPagar.Find(id);
        }

        public IEnumerable<ContaPagar> GetByPessoa(Guid pessoaId)
        {
            return _context.ContasPagar.Where(c => c.PessoaId == pessoaId).ToList();
        }

        public IEnumerable<ContaPagar> GetByPeriodo(DateTime inicio, DateTime fim)
        {
            return _context.ContasPagar.Where(c => c.DataVencimento >= inicio && c.DataVencimento <= fim).ToList();
        }

        public IEnumerable<ContaPagar> GetByStatus(bool estaPaga)
        {
            return _context.ContasPagar.Where(c => c.EstaPaga == estaPaga).ToList();
        }

        public void Update(ContaPagar contaPagar)
        {
            _context.ContasPagar.Update(contaPagar);
            _context.SaveChanges();
        }
    }
}
