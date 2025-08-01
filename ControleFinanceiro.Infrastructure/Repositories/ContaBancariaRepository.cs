using System;
using System.Collections.Generic;
using System.Linq;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;
using ControleFinanceiro.Infrastructure.Data;
using ControleFinanceiro.Shared.Utils;

namespace ControleFinanceiro.Infrastructure.Repositories
{
    public class ContaBancariaRepository : IContaBancariaRepository
    {
        private readonly FinanceiroDbContext _context;

        public ContaBancariaRepository(FinanceiroDbContext context)
        {
            _context = context;
        }

        public void Add(ContaBancaria conta)
        {
            conta.Numero = Crypto.Encrypt(conta.Numero, Constants.EncryptionKey);
            _context.ContasBancarias.Add(conta);
            _context.SaveChanges();
            conta.Numero = Crypto.Decrypt(conta.Numero, Constants.EncryptionKey);
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
                conta.Numero = Crypto.Decrypt(conta.Numero, Constants.EncryptionKey);
            }
            return conta;
        }

        public IEnumerable<ContaBancaria> GetByPessoa(Guid pessoaId)
        {
            var contas = _context.ContasBancarias.Where(c => c.PessoaId == pessoaId).ToList();
            foreach (var c in contas)
            {
                c.Numero = Crypto.Decrypt(c.Numero, Constants.EncryptionKey);
            }
            return contas;
        }

        public void Update(ContaBancaria conta)
        {
            conta.Numero = Crypto.Encrypt(conta.Numero, Constants.EncryptionKey);
            _context.ContasBancarias.Update(conta);
            _context.SaveChanges();
            conta.Numero = Crypto.Decrypt(conta.Numero, Constants.EncryptionKey);
        }
    }
}
