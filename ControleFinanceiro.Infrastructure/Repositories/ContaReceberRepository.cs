using System;
using System.Collections.Generic;
using System.Linq;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;
using ControleFinanceiro.Infrastructure.Data;

namespace ControleFinanceiro.Infrastructure.Repositories
{
    public class ContaReceberRepository : IContaReceberRepository
    {
        private readonly FinanceiroDbContext _context;

        public ContaReceberRepository(FinanceiroDbContext context)
        {
            _context = context;
        }

        public void Add(ContaReceber contaReceber)
        {
            _context.ContasReceber.Add(contaReceber);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var entity = _context.ContasReceber.Find(id);
            if (entity != null)
            {
                _context.ContasReceber.Remove(entity);
                _context.SaveChanges();
            }
        }

        public ContaReceber GetById(Guid id)
        {
            return _context.ContasReceber.Find(id);
        }

        public IEnumerable<ContaReceber> GetByPessoa(Guid pessoaId)
        {
            return _context.ContasReceber.Where(c => c.PessoaId == pessoaId).ToList();
        }

        public IEnumerable<ContaReceber> GetByPeriodo(DateTime inicio, DateTime fim)
        {
            return _context.ContasReceber.Where(c => c.DataVencimento >= inicio && c.DataVencimento <= fim).ToList();
        }

        public IEnumerable<ContaReceber> GetByStatus(bool estaRecebido)
        {
            return _context.ContasReceber.Where(c => c.EstaRecebido == estaRecebido).ToList();
        }

        public void Update(ContaReceber contaReceber)
        {
            _context.ContasReceber.Update(contaReceber);
            _context.SaveChanges();
        }
    }
}
