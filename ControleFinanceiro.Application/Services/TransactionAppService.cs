using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;

namespace ControleFinanceiro.Application.Services
{
    public class TransactionAppService : ITransactionAppService
    {
        private readonly ITransactionRepository _repository;

        public TransactionAppService(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public void Add(Transaction transaction)
        {
            _repository.Add(transaction);
        }
    }
}
