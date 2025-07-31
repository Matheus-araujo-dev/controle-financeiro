using ControleFinanceiro.Domain.Entities;

namespace ControleFinanceiro.Application.Services
{
    public interface ITransactionAppService
    {
        void Add(Transaction transaction);
    }
}
