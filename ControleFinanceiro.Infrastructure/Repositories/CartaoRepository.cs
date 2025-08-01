using System;
using System.Collections.Generic;
using System.Linq;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;
using ControleFinanceiro.Infrastructure.Data;
using ControleFinanceiro.Shared.Utils;

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
            cartao.Numero = Crypto.Encrypt(cartao.Numero, Constants.EncryptionKey);
            _context.Cartoes.Add(cartao);
            _context.SaveChanges();
            cartao.Numero = Crypto.Decrypt(cartao.Numero, Constants.EncryptionKey);
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
            var cartao = _context.Cartoes.Find(id);
            if (cartao != null)
            {
                cartao.Numero = Crypto.Decrypt(cartao.Numero, Constants.EncryptionKey);
            }
            return cartao;
        }

        public IEnumerable<Cartao> GetByPessoa(Guid pessoaId)
        {
            var cartoes = _context.Cartoes.Where(c => c.PessoaId == pessoaId).ToList();
            foreach (var c in cartoes)
            {
                c.Numero = Crypto.Decrypt(c.Numero, Constants.EncryptionKey);
            }
            return cartoes;
        }

        public IEnumerable<Cartao> GetVencendoAte(DateTime data)
        {
            var cartoes = _context.Cartoes.Where(c => c.DataValidade <= data).ToList();
            foreach (var c in cartoes)
            {
                c.Numero = Crypto.Decrypt(c.Numero, Constants.EncryptionKey);
            }
            return cartoes;
        }

        public void Update(Cartao cartao)
        {
            cartao.Numero = Crypto.Encrypt(cartao.Numero, Constants.EncryptionKey);
            _context.Cartoes.Update(cartao);
            _context.SaveChanges();
            cartao.Numero = Crypto.Decrypt(cartao.Numero, Constants.EncryptionKey);
        }
    }
}
