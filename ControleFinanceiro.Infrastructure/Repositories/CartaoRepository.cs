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
    public class CartaoRepository : ICartaoRepository
    {
        private readonly FinanceiroDbContext _context;
        private readonly CryptoOptions _cryptoOptions;
        public CartaoRepository(FinanceiroDbContext context, IOptions<CryptoOptions> options)
        {
            _context = context;
            _cryptoOptions = options.Value;
        }

        public void Add(Cartao cartao)
        {
            cartao.Numero = Crypto.Encrypt(cartao.Numero, _cryptoOptions.Key);
            _context.Cartoes.Add(cartao);
            _context.SaveChanges();
            cartao.Numero = Crypto.Decrypt(cartao.Numero, _cryptoOptions.Key);
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
                cartao.Numero = Crypto.Decrypt(cartao.Numero, _cryptoOptions.Key);
            }
            return cartao;
        }

        public IEnumerable<Cartao> GetByPessoa(Guid pessoaId)
        {
            var cartoes = _context.Cartoes.Where(c => c.PessoaId == pessoaId).ToList();
            foreach (var c in cartoes)
            {
                c.Numero = Crypto.Decrypt(c.Numero, _cryptoOptions.Key);
            }
            return cartoes;
        }

        public IEnumerable<Cartao> GetVencendoAte(DateTime data)
        {
            var cartoes = _context.Cartoes.Where(c => c.DataValidade <= data).ToList();
            foreach (var c in cartoes)
            {
                c.Numero = Crypto.Decrypt(c.Numero, _cryptoOptions.Key);
            }
            return cartoes;
        }

        public void Update(Cartao cartao)
        {
            cartao.Numero = Crypto.Encrypt(cartao.Numero, _cryptoOptions.Key);
            _context.Cartoes.Update(cartao);
            _context.SaveChanges();
            cartao.Numero = Crypto.Decrypt(cartao.Numero, _cryptoOptions.Key);
        }
    }
}
