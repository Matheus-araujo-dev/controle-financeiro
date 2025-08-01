using System;
using System.Linq;
using ControleFinanceiro.Domain.Repositories;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Application.Dtos;

namespace ControleFinanceiro.Application.Services
{
    public class RelatorioFinanceiroAppService : IRelatorioFinanceiroAppService
    {
        private readonly IMovimentacaoFinanceiraRepository _movimentacoes;
        private readonly IContaPagarRepository _contasPagar;
        private readonly IContaReceberRepository _contasReceber;

        public RelatorioFinanceiroAppService(
            IMovimentacaoFinanceiraRepository movimentacoes,
            IContaPagarRepository contasPagar,
            IContaReceberRepository contasReceber)
        {
            _movimentacoes = movimentacoes;
            _contasPagar = contasPagar;
            _contasReceber = contasReceber;
        }

        public ResumoMovimentacaoDto ResumoMovimentacoes(DateTime inicio, DateTime fim)
        {
            var items = _movimentacoes.GetByPeriodo(inicio, fim);
            var entradas = items.Where(m => m.Tipo == TipoMovimentacao.Entrada).Sum(m => m.Valor);
            var saidas = items.Where(m => m.Tipo == TipoMovimentacao.Saida).Sum(m => m.Valor);
            return new ResumoMovimentacaoDto
            {
                TotalEntradas = entradas,
                TotalSaidas = saidas
            };
        }

        public decimal TotalMovimentacoesPorTipo(DateTime inicio, DateTime fim, TipoMovimentacao tipo)
        {
            return _movimentacoes.GetByPeriodo(inicio, fim)
                .Where(m => m.Tipo == tipo)
                .Sum(m => m.Valor);
        }

        public ResumoContasDto ResumoContas(DateTime inicio, DateTime fim)
        {
            var pagar = _contasPagar.GetByPeriodo(inicio, fim).Sum(c => c.Valor);
            var receber = _contasReceber.GetByPeriodo(inicio, fim).Sum(c => c.Valor);
            return new ResumoContasDto
            {
                TotalAPagar = pagar,
                TotalAReceber = receber
            };
        }
    }
}
