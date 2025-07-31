using System;
using ControleFinanceiro.Application.Dtos;
using ControleFinanceiro.Domain.Entities;

namespace ControleFinanceiro.Application.Services
{
    public interface IRelatorioFinanceiroAppService
    {
        ResumoMovimentacaoDto ResumoMovimentacoes(DateTime inicio, DateTime fim);
        decimal TotalMovimentacoesPorTipo(DateTime inicio, DateTime fim, TipoMovimentacao tipo);
        ResumoContasDto ResumoContas(DateTime inicio, DateTime fim);
    }
}
