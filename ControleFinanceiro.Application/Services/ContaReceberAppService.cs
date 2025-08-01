using System;
using System.Collections.Generic;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;

namespace ControleFinanceiro.Application.Services
{
    public class ContaReceberAppService : IContaReceberAppService
    {
        private readonly IContaReceberRepository _repository;
        private readonly IFormaPagamentoRepository _formaPagamentoRepository;
        private readonly IMovimentacaoFinanceiraRepository _movimentacaoRepository;

        public ContaReceberAppService(IContaReceberRepository repository,
            IFormaPagamentoRepository formaPagamentoRepository,
            IMovimentacaoFinanceiraRepository movimentacaoRepository)
        {
            _repository = repository;
            _formaPagamentoRepository = formaPagamentoRepository;
            _movimentacaoRepository = movimentacaoRepository;
        }

        public void Add(ContaReceber contaReceber)
        {
            if (contaReceber.ValorTotal <= 0 || contaReceber.NumeroParcelas <= 0)
            {
                throw new InvalidOperationException("Valor total e número de parcelas devem ser maiores que zero.");
            }

            decimal valorParcela = contaReceber.ValorTotal / contaReceber.NumeroParcelas;

            for (int i = 0; i < contaReceber.NumeroParcelas; i++)
            {
                var parcela = new ContaReceber
                {
                    Id = Guid.NewGuid(),
                    PessoaId = contaReceber.PessoaId,
                    Descricao = $"{contaReceber.Descricao} - Parcela {i + 1}/{contaReceber.NumeroParcelas}",
                    Responsavel = contaReceber.Responsavel,
                    ValorTotal = contaReceber.ValorTotal,
                    NumeroParcelas = contaReceber.NumeroParcelas,
                    Valor = valorParcela,
                    DataVencimento = contaReceber.DataVencimento.AddMonths(i),
                    FormaPagamentoId = contaReceber.FormaPagamentoId
                };

                if (parcela.FormaPagamentoId.HasValue)
                {
                    var forma = _formaPagamentoRepository.GetById(parcela.FormaPagamentoId.Value);
                    if (forma != null && forma.BaixarAutomaticamente)
                    {
                        parcela.EstaRecebido = true;
                        parcela.DataRecebimento = DateTime.Now;
                        var mov = new MovimentacaoFinanceira
                        {
                            Id = Guid.NewGuid(),
                            PessoaId = parcela.PessoaId,
                            Tipo = TipoMovimentacao.Entrada,
                            Valor = parcela.Valor,
                            Data = parcela.DataRecebimento.Value,
                            Descricao = parcela.Descricao,
                            ContaReceberId = parcela.Id
                        };
                        _movimentacaoRepository.Add(mov);
                    }
                }

                _repository.Add(parcela);
            }
        }

        public void Update(ContaReceber contaReceber)
        {
            if (contaReceber.Valor <= 0 || contaReceber.ValorTotal <= 0 || contaReceber.NumeroParcelas <= 0)
            {
                throw new InvalidOperationException("Valor, valor total e número de parcelas devem ser maiores que zero.");
            }

            var existente = _repository.GetById(contaReceber.Id);
            bool gerarMovimentacao = existente != null && !existente.EstaRecebido && contaReceber.EstaRecebido;

            if (gerarMovimentacao && !contaReceber.DataRecebimento.HasValue)
            {
                contaReceber.DataRecebimento = DateTime.Now;
            }

            _repository.Update(contaReceber);

            if (gerarMovimentacao)
            {
                var mov = new MovimentacaoFinanceira
                {
                    Id = Guid.NewGuid(),
                    PessoaId = contaReceber.PessoaId,
                    Tipo = TipoMovimentacao.Entrada,
                    Valor = contaReceber.Valor,
                    Data = contaReceber.DataRecebimento.Value,
                    Descricao = contaReceber.Descricao,
                    ContaReceberId = contaReceber.Id
                };
                _movimentacaoRepository.Add(mov);
            }
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public ContaReceber GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<ContaReceber> GetByPessoa(Guid pessoaId)
        {
            return _repository.GetByPessoa(pessoaId);
        }

        public IEnumerable<ContaReceber> GetByPeriodo(DateTime inicio, DateTime fim)
        {
            return _repository.GetByPeriodo(inicio, fim);
        }

        public IEnumerable<ContaReceber> GetByStatus(bool estaRecebido)
        {
            return _repository.GetByStatus(estaRecebido);
        }
    }
}
