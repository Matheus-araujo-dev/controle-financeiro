using System;
using System.Collections.Generic;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;

namespace ControleFinanceiro.Application.Services
{
    public class ContaPagarAppService : IContaPagarAppService
    {
        private readonly IContaPagarRepository _repository;
        private readonly IFormaPagamentoRepository _formaPagamentoRepository;
        private readonly IMovimentacaoFinanceiraRepository _movimentacaoRepository;

        public ContaPagarAppService(IContaPagarRepository repository,
            IFormaPagamentoRepository formaPagamentoRepository,
            IMovimentacaoFinanceiraRepository movimentacaoRepository)
        {
            _repository = repository;
            _formaPagamentoRepository = formaPagamentoRepository;
            _movimentacaoRepository = movimentacaoRepository;
        }

        public void Add(ContaPagar contaPagar)
        {
            if (contaPagar.ValorTotal <= 0 || contaPagar.NumeroParcelas <= 0)
            {
                throw new InvalidOperationException("Valor total e número de parcelas devem ser maiores que zero.");
            }

            decimal valorParcela = contaPagar.ValorTotal / contaPagar.NumeroParcelas;

            for (int i = 0; i < contaPagar.NumeroParcelas; i++)
            {
                var parcela = new ContaPagar
                {
                    Id = Guid.NewGuid(),
                    PessoaId = contaPagar.PessoaId,
                    Descricao = $"{contaPagar.Descricao} - Parcela {i + 1}/{contaPagar.NumeroParcelas}",
                    Responsavel = contaPagar.Responsavel,
                    ValorTotal = contaPagar.ValorTotal,
                    NumeroParcelas = contaPagar.NumeroParcelas,
                    Valor = valorParcela,
                    DataVencimento = contaPagar.DataVencimento.AddMonths(i),
                    FormaPagamentoId = contaPagar.FormaPagamentoId
                };

                if (parcela.FormaPagamentoId.HasValue)
                {
                    var forma = _formaPagamentoRepository.GetById(parcela.FormaPagamentoId.Value);
                    if (forma != null && forma.BaixarAutomaticamente)
                    {
                        parcela.EstaPaga = true;
                        parcela.DataPagamento = DateTime.Now;
                        var mov = new MovimentacaoFinanceira
                        {
                            Id = Guid.NewGuid(),
                            PessoaId = parcela.PessoaId,
                            Tipo = TipoMovimentacao.Saida,
                            Valor = parcela.Valor,
                            Data = parcela.DataPagamento.Value,
                            Descricao = parcela.Descricao
                        };
                        _movimentacaoRepository.Add(mov);
                    }
                }

                _repository.Add(parcela);
            }
        }

        public void Update(ContaPagar contaPagar)
        {
            if (contaPagar.Valor <= 0 || contaPagar.ValorTotal <= 0 || contaPagar.NumeroParcelas <= 0)
            {
                throw new InvalidOperationException("Valor, valor total e número de parcelas devem ser maiores que zero.");
            }
            _repository.Update(contaPagar);
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public ContaPagar GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<ContaPagar> GetByPessoa(Guid pessoaId)
        {
            return _repository.GetByPessoa(pessoaId);
        }

        public IEnumerable<ContaPagar> GetByPeriodo(DateTime inicio, DateTime fim)
        {
            return _repository.GetByPeriodo(inicio, fim);
        }

        public IEnumerable<ContaPagar> GetByStatus(bool estaPaga)
        {
            return _repository.GetByStatus(estaPaga);
        }
    }
}
