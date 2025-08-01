using System;
using System.Collections.Generic;
using System.Linq;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;
using ControleFinanceiro.Infrastructure.Data;
using ControleFinanceiro.Shared.Utils;
using Microsoft.Extensions.Options;

namespace ControleFinanceiro.Infrastructure.Repositories
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly FinanceiroDbContext _context;
        private readonly CryptoOptions _cryptoOptions;
        public PessoaRepository(FinanceiroDbContext context, IOptions<CryptoOptions> options)
        {
            _context = context;
            _cryptoOptions = options.Value;
        }

        public void Add(Pessoa pessoa)
        {
            pessoa.Documento = Crypto.Encrypt(pessoa.Documento, _cryptoOptions.Key);
            _context.Pessoas.Add(pessoa);
            _context.SaveChanges();
            pessoa.Documento = Crypto.Decrypt(pessoa.Documento, _cryptoOptions.Key);
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
                p.Documento = Crypto.Decrypt(p.Documento, _cryptoOptions.Key);
            }
            return pessoas;
        }

        public Pessoa GetByDocumento(string documento)
        {
            var enc = Crypto.Encrypt(documento, _cryptoOptions.Key);
            var pessoa = _context.Pessoas.FirstOrDefault(p => p.Documento == enc && p.Ativo);
            if (pessoa != null)
            {
                pessoa.Documento = Crypto.Decrypt(pessoa.Documento, _cryptoOptions.Key);
            }
            return pessoa;
        }

        public Pessoa GetById(Guid id)
        {
            var pessoa = _context.Pessoas.Find(id);
            if (pessoa != null && pessoa.Ativo)
            {
                pessoa.Documento = Crypto.Decrypt(pessoa.Documento, _cryptoOptions.Key);
                return pessoa;
            }
            return null;
        }

        public IEnumerable<Pessoa> GetByNome(string nome)
        {
            var pessoas = _context.Pessoas.Where(p => p.Nome.Contains(nome) && p.Ativo).ToList();
            foreach (var p in pessoas)
            {
                p.Documento = Crypto.Decrypt(p.Documento, _cryptoOptions.Key);
            }
            return pessoas;
        }

        public void Update(Pessoa pessoa)
        {
            pessoa.Documento = Crypto.Encrypt(pessoa.Documento, _cryptoOptions.Key);
            _context.Pessoas.Update(pessoa);
            _context.SaveChanges();
            pessoa.Documento = Crypto.Decrypt(pessoa.Documento, _cryptoOptions.Key);
        }
    }
}
