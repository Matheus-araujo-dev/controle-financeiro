using System;
using Moq;
using Xunit;
using ControleFinanceiro.Application.Services;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;

namespace ControleFinanceiro.Tests.Services
{
    public class ContaReceberAppServiceTests
    {
        [Fact]
        public void Update_DeNaoRecebidaParaRecebida_DeveGerarMovimentacao()
        {
            var repo = new Mock<IContaReceberRepository>();
            var formaRepo = new Mock<IFormaPagamentoRepository>();
            var movRepo = new Mock<IMovimentacaoFinanceiraRepository>();
            var service = new ContaReceberAppService(repo.Object, formaRepo.Object, movRepo.Object);

            var id = Guid.NewGuid();
            var existente = new ContaReceber
            {
                Id = id,
                PessoaId = Guid.NewGuid(),
                Descricao = "Teste",
                Responsavel = "Eu",
                ValorTotal = 80m,
                NumeroParcelas = 1,
                Valor = 80m,
                DataVencimento = DateTime.Today,
                EstaRecebido = false
            };
            repo.Setup(r => r.GetById(id)).Returns(existente);

            var atualizar = new ContaReceber
            {
                Id = id,
                PessoaId = existente.PessoaId,
                Descricao = existente.Descricao,
                Responsavel = existente.Responsavel,
                ValorTotal = existente.ValorTotal,
                NumeroParcelas = existente.NumeroParcelas,
                Valor = existente.Valor,
                DataVencimento = existente.DataVencimento,
                EstaRecebido = true,
                DataRecebimento = DateTime.Today
            };

            service.Update(atualizar);

            movRepo.Verify(r => r.Add(It.Is<MovimentacaoFinanceira>(m =>
                m.Tipo == TipoMovimentacao.Entrada &&
                m.Valor == atualizar.Valor &&
                m.Data == atualizar.DataRecebimento &&
                m.PessoaId == atualizar.PessoaId)), Times.Once);
            repo.Verify(r => r.Update(atualizar), Times.Once);
        }
    }
}
