using System;
using System.Collections.Generic;
using Moq;
using Xunit;
using ControleFinanceiro.Application.Services;
using ControleFinanceiro.Application.Dtos;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;

namespace ControleFinanceiro.Tests.Services
{
    public class RelatorioFinanceiroAppServiceTests
    {
        [Fact]
        public void ResumoMovimentacoes_DeveCalcularEntradasESaidas()
        {
            var movRepo = new Mock<IMovimentacaoFinanceiraRepository>();
            movRepo.Setup(r => r.GetByPeriodo(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                   .Returns(new List<MovimentacaoFinanceira>
                   {
                       new MovimentacaoFinanceira { Valor = 100m, Tipo = TipoMovimentacao.Entrada },
                       new MovimentacaoFinanceira { Valor = 40m, Tipo = TipoMovimentacao.Saida }
                   });
            var pagarRepo = new Mock<IContaPagarRepository>();
            var receberRepo = new Mock<IContaReceberRepository>();
            var service = new RelatorioFinanceiroAppService(movRepo.Object, pagarRepo.Object, receberRepo.Object);

            var resumo = service.ResumoMovimentacoes(DateTime.Today, DateTime.Today);

            Assert.Equal(100m, resumo.TotalEntradas);
            Assert.Equal(40m, resumo.TotalSaidas);
        }

        [Fact]
        public void TotalMovimentacoesPorTipo_DeveSomarApenasTipoInformado()
        {
            var movRepo = new Mock<IMovimentacaoFinanceiraRepository>();
            movRepo.Setup(r => r.GetByPeriodo(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                   .Returns(new List<MovimentacaoFinanceira>
                   {
                       new MovimentacaoFinanceira { Valor = 50m, Tipo = TipoMovimentacao.Entrada },
                       new MovimentacaoFinanceira { Valor = 20m, Tipo = TipoMovimentacao.Entrada },
                       new MovimentacaoFinanceira { Valor = 30m, Tipo = TipoMovimentacao.Saida }
                   });
            var service = new RelatorioFinanceiroAppService(movRepo.Object, new Mock<IContaPagarRepository>().Object, new Mock<IContaReceberRepository>().Object);

            var total = service.TotalMovimentacoesPorTipo(DateTime.Today, DateTime.Today, TipoMovimentacao.Entrada);

            Assert.Equal(70m, total);
        }

        [Fact]
        public void ResumoContas_DeveSomarTotais()
        {
            var movRepo = new Mock<IMovimentacaoFinanceiraRepository>();
            var pagarRepo = new Mock<IContaPagarRepository>();
            var receberRepo = new Mock<IContaReceberRepository>();

            pagarRepo.Setup(r => r.GetByPeriodo(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                     .Returns(new List<ContaPagar>
                     {
                         new ContaPagar { Valor = 10m },
                         new ContaPagar { Valor = 20m }
                     });
            receberRepo.Setup(r => r.GetByPeriodo(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                       .Returns(new List<ContaReceber>
                       {
                           new ContaReceber { Valor = 30m },
                           new ContaReceber { Valor = 70m }
                       });
            var service = new RelatorioFinanceiroAppService(movRepo.Object, pagarRepo.Object, receberRepo.Object);

            var resumo = service.ResumoContas(DateTime.Today, DateTime.Today);

            Assert.Equal(30m, resumo.TotalAPagar);
            Assert.Equal(100m, resumo.TotalAReceber);
        }
    }
}
