using System;
using System.Security.Cryptography;
using System.Text;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;

namespace ControleFinanceiro.Application.Services
{
    public class UsuarioAppService : IUsuarioAppService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioAppService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public void Registrar(Usuario usuario)
        {
            if (_repository.GetByEmail(usuario.Email) != null)
                throw new InvalidOperationException("Usuário já cadastrado.");

            usuario.SenhaHash = HashSenha(usuario.SenhaHash);
            _repository.Add(usuario);
        }

        public Usuario Autenticar(string email, string senha)
        {
            var user = _repository.GetByEmail(email);
            if (user == null)
                return null;

            var hash = HashSenha(senha);
            return user.SenhaHash == hash ? user : null;
        }

        private static string HashSenha(string senha)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(senha);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
