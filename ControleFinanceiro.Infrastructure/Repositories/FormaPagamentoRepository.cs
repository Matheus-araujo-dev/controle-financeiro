using System;
using System.Collections.Generic;
using System.Linq;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;
using ControleFinanceiro.Infrastructure.Data;

namespace ControleFinanceiro.Infrastructure.Repositories
{
    public class FormaPagamentoRepository : IFormaPagamentoRepository
    {
        private readonly FinanceiroDbContext _context;

        public FormaPagamentoRepository(FinanceiroDbContext context)
        {
            _context = context;
        }

        public void Add(FormaPagamento forma)
        {
            _context.FormasPagamento.Add(forma);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var entity = _context.FormasPagamento.Find(id);
            if (entity != null)
            {
                _context.FormasPagamento.Remove(entity);
                _context.SaveChanges();
            }
        }

        public FormaPagamento GetById(Guid id)
        {
            return _context.FormasPagamento.Find(id);
        }

        public IEnumerable<FormaPagamento> GetByPessoa(Guid pessoaId)
        {
            return _context.FormasPagamento.Where(f => f.PessoaId == pessoaId).ToList();
        }

        public IEnumerable<FormaPagamento> GetByCartao(Guid cartaoId)
        {
            return _context.FormasPagamento.Where(f => f.CartaoId == cartaoId).ToList();
        }

        public void Update(FormaPagamento forma)
        {
            _context.FormasPagamento.Update(forma);
            _context.SaveChanges();
        }
    }
}
