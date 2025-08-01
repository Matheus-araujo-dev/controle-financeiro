using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using ControleFinanceiro.Application.Services;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;
using ControleFinanceiro.Shared.Utils;

namespace ControleFinanceiro.Tests.Services
{
    public class ContaBancariaCartaoEncryptionTests
    {
        private class InMemoryContaBancariaRepository : IContaBancariaRepository
        {
            public readonly List<ContaBancaria> Stored = new();

            public void Add(ContaBancaria conta)
            {
                var enc = Crypto.Encrypt(conta.Numero, Constants.EncryptionKey);
                Stored.Add(new ContaBancaria
                {
                    Id = conta.Id,
                    PessoaId = conta.PessoaId,
                    Banco = conta.Banco,
                    Agencia = conta.Agencia,
                    Numero = enc
                });
                conta.Numero = Crypto.Decrypt(enc, Constants.EncryptionKey);
            }

            public void Update(ContaBancaria conta)
            {
                Delete(conta.Id);
                Add(conta);
            }

            public void Delete(Guid id)
            {
                Stored.RemoveAll(c => c.Id == id);
            }

            public ContaBancaria GetById(Guid id)
            {
                var entity = Stored.FirstOrDefault(c => c.Id == id);
                if (entity == null) return null;
                return new ContaBancaria
                {
                    Id = entity.Id,
                    PessoaId = entity.PessoaId,
                    Banco = entity.Banco,
                    Agencia = entity.Agencia,
                    Numero = Crypto.Decrypt(entity.Numero, Constants.EncryptionKey)
                };
            }

            public IEnumerable<ContaBancaria> GetByPessoa(Guid pessoaId)
            {
                return Stored
                    .Where(c => c.PessoaId == pessoaId)
                    .Select(e => new ContaBancaria
                    {
                        Id = e.Id,
                        PessoaId = e.PessoaId,
                        Banco = e.Banco,
                        Agencia = e.Agencia,
                        Numero = Crypto.Decrypt(e.Numero, Constants.EncryptionKey)
                    });
            }
        }

        private class InMemoryCartaoRepository : ICartaoRepository
        {
            public readonly List<Cartao> Stored = new();

            public void Add(Cartao cartao)
            {
                var enc = Crypto.Encrypt(cartao.Numero, Constants.EncryptionKey);
                Stored.Add(new Cartao
                {
                    Id = cartao.Id,
                    PessoaId = cartao.PessoaId,
                    Bandeira = cartao.Bandeira,
                    NomeImpresso = cartao.NomeImpresso,
                    DataValidade = cartao.DataValidade,
                    CodigoSeguranca = cartao.CodigoSeguranca,
                    Limite = cartao.Limite,
                    Numero = enc,
                    NumeroFinal = cartao.NumeroFinal
                });
                cartao.Numero = Crypto.Decrypt(enc, Constants.EncryptionKey);
            }

            public void Update(Cartao cartao)
            {
                Delete(cartao.Id);
                Add(cartao);
            }

            public void Delete(Guid id)
            {
                Stored.RemoveAll(c => c.Id == id);
            }

            public Cartao GetById(Guid id)
            {
                var e = Stored.FirstOrDefault(c => c.Id == id);
                if (e == null) return null;
                return new Cartao
                {
                    Id = e.Id,
                    PessoaId = e.PessoaId,
                    Bandeira = e.Bandeira,
                    NomeImpresso = e.NomeImpresso,
                    DataValidade = e.DataValidade,
                    CodigoSeguranca = e.CodigoSeguranca,
                    Limite = e.Limite,
                    Numero = Crypto.Decrypt(e.Numero, Constants.EncryptionKey),
                    NumeroFinal = e.NumeroFinal
                };
            }

            public IEnumerable<Cartao> GetByPessoa(Guid pessoaId)
            {
                return Stored.Where(c => c.PessoaId == pessoaId)
                    .Select(e => new Cartao
                    {
                        Id = e.Id,
                        PessoaId = e.PessoaId,
                        Bandeira = e.Bandeira,
                        NomeImpresso = e.NomeImpresso,
                        DataValidade = e.DataValidade,
                        CodigoSeguranca = e.CodigoSeguranca,
                        Limite = e.Limite,
                        Numero = Crypto.Decrypt(e.Numero, Constants.EncryptionKey),
                        NumeroFinal = e.NumeroFinal
                    });
            }

            public IEnumerable<Cartao> GetVencendoAte(DateTime data) => Enumerable.Empty<Cartao>();
        }

        [Fact]
        public void ContaBancaria_Add_ShouldEncryptAndDecryptNumero()
        {
            var repo = new InMemoryContaBancariaRepository();
            var service = new ContaBancariaAppService(repo);

            var conta = new ContaBancaria
            {
                Id = Guid.NewGuid(),
                PessoaId = Guid.NewGuid(),
                Banco = "Banco",
                Agencia = "0001",
                Numero = "123456"
            };

            service.Add(conta);

            Assert.Single(repo.Stored);
            Assert.NotEqual("123456", repo.Stored[0].Numero); // stored encrypted
            var retorno = service.GetById(conta.Id);
            Assert.Equal("123456", retorno.Numero); // decrypted on read
        }

        [Fact]
        public void Cartao_Add_ShouldEncryptNumeroAndSetNumeroFinal()
        {
            var repo = new InMemoryCartaoRepository();
            var service = new CartaoAppService(repo);

            var cartao = new Cartao
            {
                Id = Guid.NewGuid(),
                PessoaId = Guid.NewGuid(),
                Numero = "1111222233334444",
                Bandeira = "Visa",
                NomeImpresso = "Test",
                DataValidade = DateTime.Today.AddYears(1),
                CodigoSeguranca = 123,
                Limite = 1000m
            };

            service.Add(cartao);

            Assert.Single(repo.Stored);
            Assert.NotEqual("1111222233334444", repo.Stored[0].Numero); // encrypted
            Assert.Equal("4444", cartao.NumeroFinal);
            var retorno = service.GetById(cartao.Id);
            Assert.Equal("1111222233334444", retorno.Numero); // decrypted
        }
    }
}

