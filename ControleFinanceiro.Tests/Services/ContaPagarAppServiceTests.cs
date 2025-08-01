using System;
using Moq;
using Xunit;
using ControleFinanceiro.Application.Services;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;

namespace ControleFinanceiro.Tests.Services
{
    public class ContaPagarAppServiceTests
    {
        [Fact]
        public void Add_ComValoresInvalidos_DeveLancarExcecao()
        {
            var repo = new Mock<IContaPagarRepository>();
            var formaRepo = new Mock<IFormaPagamentoRepository>();
            var movRepo = new Mock<IMovimentacaoFinanceiraRepository>();
            var service = new ContaPagarAppService(repo.Object, formaRepo.Object, movRepo.Object);

            var conta = new ContaPagar
            {
                PessoaId = Guid.NewGuid(),
                Descricao = "Teste",
                Responsavel = "Eu",
                ValorTotal = 0m,
                NumeroParcelas = 0,
                DataVencimento = DateTime.Today
            };

            Assert.Throws<InvalidOperationException>(() => service.Add(conta));
        }

        [Fact]
        public void Update_ComValoresInvalidos_DeveLancarExcecao()
        {
            var repo = new Mock<IContaPagarRepository>();
            var formaRepo = new Mock<IFormaPagamentoRepository>();
            var movRepo = new Mock<IMovimentacaoFinanceiraRepository>();
            var service = new ContaPagarAppService(repo.Object, formaRepo.Object, movRepo.Object);

            var conta = new ContaPagar
            {
                Id = Guid.NewGuid(),
                PessoaId = Guid.NewGuid(),
                Descricao = "Teste",
                Responsavel = "Eu",
                ValorTotal = 0m,
                NumeroParcelas = 0,
                Valor = 0m,
                DataVencimento = DateTime.Today
            };

            Assert.Throws<InvalidOperationException>(() => service.Update(conta));
            repo.Verify(r => r.Update(It.IsAny<ContaPagar>()), Times.Never);
        }
    }
}
