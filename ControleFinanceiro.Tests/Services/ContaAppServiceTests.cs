using System;
using Moq;
using Xunit;
using ControleFinanceiro.Application.Services;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;

namespace ControleFinanceiro.Tests.Services
{
    public class ContaAppServiceTests
    {
        [Fact]
        public void ContaPagar_ComFormaPagamentoAutomatico_DeveBaixarParcela()
        {
            var contaRepo = new Mock<IContaPagarRepository>();
            var formaRepo = new Mock<IFormaPagamentoRepository>();
            var movRepo = new Mock<IMovimentacaoFinanceiraRepository>();

            var forma = new FormaPagamento { Id = Guid.NewGuid(), BaixarAutomaticamente = true };
            formaRepo.Setup(r => r.GetById(forma.Id)).Returns(forma);

            var service = new ContaPagarAppService(contaRepo.Object, formaRepo.Object, movRepo.Object);

            var conta = new ContaPagar
            {
                PessoaId = Guid.NewGuid(),
                Descricao = "Teste",
                Responsavel = "Eu",
                ValorTotal = 100m,
                NumeroParcelas = 1,
                DataVencimento = DateTime.Today,
                FormaPagamentoId = forma.Id
            };

            service.Add(conta);

            contaRepo.Verify(r => r.Add(It.Is<ContaPagar>(c => c.EstaPaga && c.DataPagamento.HasValue)), Times.Once);
            movRepo.Verify(r => r.Add(It.Is<MovimentacaoFinanceira>(m => m.Tipo == TipoMovimentacao.Saida && m.Valor == 100m)), Times.Once);
        }

        [Fact]
        public void ContaReceber_ComFormaPagamentoAutomatico_DeveBaixarParcela()
        {
            var contaRepo = new Mock<IContaReceberRepository>();
            var formaRepo = new Mock<IFormaPagamentoRepository>();
            var movRepo = new Mock<IMovimentacaoFinanceiraRepository>();

            var forma = new FormaPagamento { Id = Guid.NewGuid(), BaixarAutomaticamente = true };
            formaRepo.Setup(r => r.GetById(forma.Id)).Returns(forma);

            var service = new ContaReceberAppService(contaRepo.Object, formaRepo.Object, movRepo.Object);

            var conta = new ContaReceber
            {
                PessoaId = Guid.NewGuid(),
                Descricao = "Teste",
                Responsavel = "Eu",
                ValorTotal = 200m,
                NumeroParcelas = 1,
                DataVencimento = DateTime.Today,
                FormaPagamentoId = forma.Id
            };

            service.Add(conta);

            contaRepo.Verify(r => r.Add(It.Is<ContaReceber>(c => c.EstaRecebido && c.DataRecebimento.HasValue)), Times.Once);
            movRepo.Verify(r => r.Add(It.Is<MovimentacaoFinanceira>(m => m.Tipo == TipoMovimentacao.Entrada && m.Valor == 200m)), Times.Once);
        }
    }
}
