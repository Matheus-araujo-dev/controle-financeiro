using ControleFinanceiro.Domain.Entities;

namespace ControleFinanceiro.Application.Services
{
    public interface IUsuarioAppService
    {
        void Registrar(Usuario usuario);
        Usuario Autenticar(string email, string senha);
    }
}
