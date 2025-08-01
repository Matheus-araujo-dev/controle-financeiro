using System;
using System.Collections.Generic;
using System.Linq;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;
using ControleFinanceiro.Infrastructure.Data;

namespace ControleFinanceiro.Infrastructure.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly FinanceiroDbContext _context;

        public CategoriaRepository(FinanceiroDbContext context)
        {
            _context = context;
        }

        public void Add(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var entity = _context.Categorias.Find(id);
            if (entity != null)
            {
                _context.Categorias.Remove(entity);
                _context.SaveChanges();
            }
        }

        public Categoria GetById(Guid id)
        {
            return _context.Categorias.Find(id);
        }

        public IEnumerable<Categoria> GetByPessoa(Guid pessoaId)
        {
            return _context.Categorias.Where(c => c.PessoaId == pessoaId).ToList();
        }

        public void Update(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            _context.SaveChanges();
        }
    }
}
