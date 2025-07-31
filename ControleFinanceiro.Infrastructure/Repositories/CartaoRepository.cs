using System;
using System.Collections.Generic;
using System.Linq;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;
using ControleFinanceiro.Infrastructure.Data;

namespace ControleFinanceiro.Infrastructure.Repositories
{
    public class CartaoRepository : ICartaoRepository
    {
        private readonly FinanceiroDbContext _context;

        public CartaoRepository(FinanceiroDbContext context)
        {
            _context = context;
        }

        public void Add(Cartao cartao)
        {
            _context.Cartoes.Add(cartao);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var entity = _context.Cartoes.Find(id);
            if (entity != null)
            {
                _context.Cartoes.Remove(entity);
                _context.SaveChanges();
            }
        }

        public Cartao GetById(Guid id)
        {
            return _context.Cartoes.Find(id);
        }

        public IEnumerable<Cartao> GetByPessoa(Guid pessoaId)
        {
            return _context.Cartoes.Where(c => c.PessoaId == pessoaId).ToList();
        }

        public IEnumerable<Cartao> GetVencendoAte(DateTime data)
        {
            return _context.Cartoes.Where(c => c.DataValidade <= data).ToList();
        }

        public void Update(Cartao cartao)
        {
            _context.Cartoes.Update(cartao);
            _context.SaveChanges();
        }
    }
}
