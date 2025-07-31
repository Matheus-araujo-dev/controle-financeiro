using System;
using ControleFinanceiro.Domain.Entities;

namespace ControleFinanceiro.Domain.Repositories
{
    public interface IUsuarioRepository
    {
        void Add(Usuario usuario);
        Usuario GetByEmail(string email);
        Usuario GetById(Guid id);
    }
}
