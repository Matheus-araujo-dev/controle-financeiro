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
    public class ContaBancariaRepository : IContaBancariaRepository
    {
        private readonly FinanceiroDbContext _context;
        private readonly CryptoOptions _cryptoOptions;
        public ContaBancariaRepository(FinanceiroDbContext context, IOptions<CryptoOptions> options)
        {
            _context = context;
            _cryptoOptions = options.Value;
        }

        public void Add(ContaBancaria conta)
        {
            conta.Numero = Crypto.Encrypt(conta.Numero, _cryptoOptions.Key);
            _context.ContasBancarias.Add(conta);
            _context.SaveChanges();
            conta.Numero = Crypto.Decrypt(conta.Numero, _cryptoOptions.Key);
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
            var conta = _context.ContasBancarias.Find(id);
            if (conta != null)
            {
                conta.Numero = Crypto.Decrypt(conta.Numero, _cryptoOptions.Key);
            }
            return conta;
        }

        public IEnumerable<ContaBancaria> GetByPessoa(Guid pessoaId)
        {
            var contas = _context.ContasBancarias.Where(c => c.PessoaId == pessoaId).ToList();
            foreach (var c in contas)
            {
                c.Numero = Crypto.Decrypt(c.Numero, _cryptoOptions.Key);
            }
            return contas;
        }

        public void Update(ContaBancaria conta)
        {
            conta.Numero = Crypto.Encrypt(conta.Numero, _cryptoOptions.Key);
            _context.ContasBancarias.Update(conta);
            _context.SaveChanges();
            conta.Numero = Crypto.Decrypt(conta.Numero, _cryptoOptions.Key);
        }
    }
}
