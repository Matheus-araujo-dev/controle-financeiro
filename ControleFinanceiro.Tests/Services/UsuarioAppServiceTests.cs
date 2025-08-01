using System;
using System.Security.Cryptography;
using System.Text;
using Moq;
using Xunit;
using ControleFinanceiro.Application.Services;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;

namespace ControleFinanceiro.Tests.Services
{
    public class UsuarioAppServiceTests
    {
        private static string Hash(string senha)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(senha);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        [Fact]
        public void Registrar_NovoUsuario_DeveHashSenhaEAdicionar()
        {
            var repo = new Mock<IUsuarioRepository>();
            repo.Setup(r => r.GetByEmail("a@a.com")).Returns((Usuario)null);
            var service = new UsuarioAppService(repo.Object);

            var usuario = new Usuario { Email = "a@a.com", SenhaHash = "123" };

            service.Registrar(usuario);

            var esperado = Hash("123");
            Assert.Equal(esperado, usuario.SenhaHash);
            repo.Verify(r => r.Add(It.Is<Usuario>(u => u.SenhaHash == esperado)), Times.Once);
        }

        [Fact]
        public void Registrar_EmailExistente_DeveLancarExcecao()
        {
            var repo = new Mock<IUsuarioRepository>();
            repo.Setup(r => r.GetByEmail("a@a.com")).Returns(new Usuario());
            var service = new UsuarioAppService(repo.Object);

            var usuario = new Usuario { Email = "a@a.com", SenhaHash = "123" };

            Assert.Throws<InvalidOperationException>(() => service.Registrar(usuario));
            repo.Verify(r => r.Add(It.IsAny<Usuario>()), Times.Never);
        }

        [Fact]
        public void Autenticar_CredenciaisValidas_DeveRetornarUsuario()
        {
            var hash = Hash("123");
            var repo = new Mock<IUsuarioRepository>();
            repo.Setup(r => r.GetByEmail("a@a.com")).Returns(new Usuario { Email = "a@a.com", SenhaHash = hash });
            var service = new UsuarioAppService(repo.Object);

            var user = service.Autenticar("a@a.com", "123");

            Assert.NotNull(user);
        }

        [Fact]
        public void Autenticar_SenhaInvalida_DeveRetornarNull()
        {
            var repo = new Mock<IUsuarioRepository>();
            repo.Setup(r => r.GetByEmail("a@a.com")).Returns(new Usuario { Email = "a@a.com", SenhaHash = Hash("123") });
            var service = new UsuarioAppService(repo.Object);

            var user = service.Autenticar("a@a.com", "321");

            Assert.Null(user);
        }

        [Fact]
        public void Autenticar_EmailInexistente_DeveRetornarNull()
        {
            var repo = new Mock<IUsuarioRepository>();
            repo.Setup(r => r.GetByEmail("a@a.com")).Returns((Usuario)null);
            var service = new UsuarioAppService(repo.Object);

            var user = service.Autenticar("a@a.com", "123");

            Assert.Null(user);
        }
    }
}

