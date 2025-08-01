using System;
using Moq;
using Xunit;
using ControleFinanceiro.Application.Services;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;

namespace ControleFinanceiro.Tests.Services
{
    public class FormaPagamentoAppServiceTests
    {
        [Fact]
        public void Add_DescricaoVazia_DeveLancarExcecao()
        {
            var repo = new Mock<IFormaPagamentoRepository>();
            var service = new FormaPagamentoAppService(repo.Object);

            var forma = new FormaPagamento { PessoaId = Guid.NewGuid(), Descricao = "" };

            Assert.Throws<InvalidOperationException>(() => service.Add(forma));
            repo.Verify(r => r.Add(It.IsAny<FormaPagamento>()), Times.Never);
        }

        [Fact]
        public void Update_DescricaoVazia_DeveLancarExcecao()
        {
            var repo = new Mock<IFormaPagamentoRepository>();
            var service = new FormaPagamentoAppService(repo.Object);

            var forma = new FormaPagamento { Id = Guid.NewGuid(), PessoaId = Guid.NewGuid(), Descricao = null };

            Assert.Throws<InvalidOperationException>(() => service.Update(forma));
            repo.Verify(r => r.Update(It.IsAny<FormaPagamento>()), Times.Never);
        }
    }
}

