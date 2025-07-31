using System;
using System.Collections.Generic;
using System.Linq;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;
using ControleFinanceiro.Infrastructure.Data;

namespace ControleFinanceiro.Infrastructure.Repositories
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly FinanceiroDbContext _context;

        public PessoaRepository(FinanceiroDbContext context)
        {
            _context = context;
        }

        public void Add(Pessoa pessoa)
        {
            _context.Pessoas.Add(pessoa);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var entity = _context.Pessoas.Find(id);
            if (entity != null)
            {
                _context.Pessoas.Remove(entity);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Pessoa> GetAll()
        {
            return _context.Pessoas.ToList();
        }

        public Pessoa GetByDocumento(string documento)
        {
            return _context.Pessoas.FirstOrDefault(p => p.Documento == documento);
        }

        public Pessoa GetById(Guid id)
        {
            return _context.Pessoas.Find(id);
        }

        public IEnumerable<Pessoa> GetByNome(string nome)
        {
            return _context.Pessoas.Where(p => p.Nome.Contains(nome)).ToList();
        }

        public void Update(Pessoa pessoa)
        {
            _context.Pessoas.Update(pessoa);
            _context.SaveChanges();
        }
    }
}
