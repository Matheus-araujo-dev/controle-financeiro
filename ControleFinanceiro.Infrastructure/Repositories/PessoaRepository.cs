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
                entity.Ativo = false;
                _context.SaveChanges();
            }
        }

        public IEnumerable<Pessoa> GetAll()
        {
            return _context.Pessoas.Where(p => p.Ativo).ToList();
        }

        public Pessoa GetByDocumento(string documento)
        {
            return _context.Pessoas.FirstOrDefault(p => p.Documento == documento && p.Ativo);
        }

        public Pessoa GetById(Guid id)
        {
            var pessoa = _context.Pessoas.Find(id);
            return pessoa != null && pessoa.Ativo ? pessoa : null;
        }

        public IEnumerable<Pessoa> GetByNome(string nome)
        {
            return _context.Pessoas.Where(p => p.Nome.Contains(nome) && p.Ativo).ToList();
        }

        public void Update(Pessoa pessoa)
        {
            _context.Pessoas.Update(pessoa);
            _context.SaveChanges();
        }
    }
}
