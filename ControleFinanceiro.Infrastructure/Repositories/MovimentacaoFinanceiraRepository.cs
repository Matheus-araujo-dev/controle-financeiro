using System;
using System.Collections.Generic;
using System.Linq;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;
using ControleFinanceiro.Infrastructure.Data;

namespace ControleFinanceiro.Infrastructure.Repositories
{
    public class MovimentacaoFinanceiraRepository : IMovimentacaoFinanceiraRepository
    {
        private readonly FinanceiroDbContext _context;

        public MovimentacaoFinanceiraRepository(FinanceiroDbContext context)
        {
            _context = context;
        }

        public void Add(MovimentacaoFinanceira movimentacao)
        {
            _context.MovimentacoesFinanceiras.Add(movimentacao);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var entity = _context.MovimentacoesFinanceiras.Find(id);
            if (entity != null)
            {
                _context.MovimentacoesFinanceiras.Remove(entity);
                _context.SaveChanges();
            }
        }

        public MovimentacaoFinanceira GetById(Guid id)
        {
            return _context.MovimentacoesFinanceiras.Find(id);
        }

        public IEnumerable<MovimentacaoFinanceira> GetByPessoa(Guid pessoaId)
        {
            return _context.MovimentacoesFinanceiras.Where(m => m.PessoaId == pessoaId).ToList();
        }

        public IEnumerable<MovimentacaoFinanceira> GetByPeriodo(DateTime inicio, DateTime fim)
        {
            return _context.MovimentacoesFinanceiras.Where(m => m.Data >= inicio && m.Data <= fim).ToList();
        }

        public IEnumerable<MovimentacaoFinanceira> GetByTipo(TipoMovimentacao tipo)
        {
            return _context.MovimentacoesFinanceiras.Where(m => m.Tipo == tipo).ToList();
        }

        public void Update(MovimentacaoFinanceira movimentacao)
        {
            _context.MovimentacoesFinanceiras.Update(movimentacao);
            _context.SaveChanges();
        }
    }
}
