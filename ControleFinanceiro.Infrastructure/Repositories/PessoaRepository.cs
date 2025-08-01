using System;
using System.Collections.Generic;
using System.Linq;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;
using ControleFinanceiro.Infrastructure.Data;
using ControleFinanceiro.Shared.Utils;

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
            pessoa.Documento = Crypto.Encrypt(pessoa.Documento, Constants.EncryptionKey);
            _context.Pessoas.Add(pessoa);
            _context.SaveChanges();
            pessoa.Documento = Crypto.Decrypt(pessoa.Documento, Constants.EncryptionKey);
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
            var pessoas = _context.Pessoas.Where(p => p.Ativo).ToList();
            foreach (var p in pessoas)
            {
                p.Documento = Crypto.Decrypt(p.Documento, Constants.EncryptionKey);
            }
            return pessoas;
        }

        public Pessoa GetByDocumento(string documento)
        {
            var enc = Crypto.Encrypt(documento, Constants.EncryptionKey);
            var pessoa = _context.Pessoas.FirstOrDefault(p => p.Documento == enc && p.Ativo);
            if (pessoa != null)
            {
                pessoa.Documento = Crypto.Decrypt(pessoa.Documento, Constants.EncryptionKey);
            }
            return pessoa;
        }

        public Pessoa GetById(Guid id)
        {
            var pessoa = _context.Pessoas.Find(id);
            if (pessoa != null && pessoa.Ativo)
            {
                pessoa.Documento = Crypto.Decrypt(pessoa.Documento, Constants.EncryptionKey);
                return pessoa;
            }
            return null;
        }

        public IEnumerable<Pessoa> GetByNome(string nome)
        {
            var pessoas = _context.Pessoas.Where(p => p.Nome.Contains(nome) && p.Ativo).ToList();
            foreach (var p in pessoas)
            {
                p.Documento = Crypto.Decrypt(p.Documento, Constants.EncryptionKey);
            }
            return pessoas;
        }

        public void Update(Pessoa pessoa)
        {
            pessoa.Documento = Crypto.Encrypt(pessoa.Documento, Constants.EncryptionKey);
            _context.Pessoas.Update(pessoa);
            _context.SaveChanges();
            pessoa.Documento = Crypto.Decrypt(pessoa.Documento, Constants.EncryptionKey);
        }
    }
}
