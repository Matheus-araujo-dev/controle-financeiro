using System;
using Moq;
using Xunit;
using ControleFinanceiro.Application.Services;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;

namespace ControleFinanceiro.Tests.Services
{
    public class PessoaAppServiceTests
    {
        [Fact]
        public void Create_UniqueDocumento_AddsPessoaWithAtivoTrue()
        {
            var repo = new Mock<IPessoaRepository>();
            repo.Setup(r => r.GetByDocumento(It.IsAny<string>())).Returns((Pessoa)null);
            var service = new PessoaAppService(repo.Object);
            var pessoa = new Pessoa { Id = Guid.NewGuid(), Nome = "Teste", Email = "t@t.com", Documento = "123" };

            service.Create(pessoa);

            Assert.True(pessoa.Ativo);
            repo.Verify(r => r.Add(pessoa), Times.Once);
        }

        [Fact]
        public void Create_DuplicateDocumento_ThrowsInvalidOperationException()
        {
            var repo = new Mock<IPessoaRepository>();
            repo.Setup(r => r.GetByDocumento("123")).Returns(new Pessoa());
            var service = new PessoaAppService(repo.Object);
            var pessoa = new Pessoa { Id = Guid.NewGuid(), Nome = "Teste", Email = "t@t.com", Documento = "123" };

            Assert.Throws<InvalidOperationException>(() => service.Create(pessoa));
            repo.Verify(r => r.Add(It.IsAny<Pessoa>()), Times.Never);
        }

        [Fact]
        public void Update_NonExistingPessoa_ThrowsInvalidOperationException()
        {
            var repo = new Mock<IPessoaRepository>();
            repo.Setup(r => r.GetById(It.IsAny<Guid>())).Returns((Pessoa)null);
            var service = new PessoaAppService(repo.Object);
            var pessoa = new Pessoa { Id = Guid.NewGuid(), Nome = "Teste", Email = "t@t.com", Documento = "123" };

            Assert.Throws<InvalidOperationException>(() => service.Update(pessoa));
        }

        [Fact]
        public void Update_DuplicateDocumento_ThrowsInvalidOperationException()
        {
            var repo = new Mock<IPessoaRepository>();
            var id = Guid.NewGuid();
            repo.Setup(r => r.GetById(id)).Returns(new Pessoa { Id = id });
            repo.Setup(r => r.GetByDocumento("456")).Returns(new Pessoa { Id = Guid.NewGuid() });
            var service = new PessoaAppService(repo.Object);
            var pessoa = new Pessoa { Id = id, Nome = "Teste", Email = "t@t.com", Documento = "456" };

            Assert.Throws<InvalidOperationException>(() => service.Update(pessoa));
            repo.Verify(r => r.Update(It.IsAny<Pessoa>()), Times.Never);
        }
    }
}
