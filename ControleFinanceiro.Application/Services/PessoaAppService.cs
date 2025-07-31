using System;
using System.Collections.Generic;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;

namespace ControleFinanceiro.Application.Services
{
    public class PessoaAppService : IPessoaAppService
    {
        private readonly IPessoaRepository _pessoaRepository;

        public PessoaAppService(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        public void Create(Pessoa pessoa)
        {
            if (_pessoaRepository.GetByDocumento(pessoa.Documento) != null)
            {
                throw new InvalidOperationException("Documento já cadastrado.");
            }

            _pessoaRepository.Add(pessoa);
        }

        public void Update(Pessoa pessoa)
        {
            var existente = _pessoaRepository.GetById(pessoa.Id);
            if (existente == null)
            {
                throw new InvalidOperationException("Pessoa não encontrada.");
            }

            var outro = _pessoaRepository.GetByDocumento(pessoa.Documento);
            if (outro != null && outro.Id != pessoa.Id)
            {
                throw new InvalidOperationException("Documento já cadastrado.");
            }

            _pessoaRepository.Update(pessoa);
        }

        public void Delete(Guid id)
        {
            _pessoaRepository.Delete(id);
        }

        public Pessoa GetById(Guid id)
        {
            return _pessoaRepository.GetById(id);
        }

        public IEnumerable<Pessoa> GetAll()
        {
            return _pessoaRepository.GetAll();
        }

        public IEnumerable<Pessoa> GetByNome(string nome)
        {
            return _pessoaRepository.GetByNome(nome);
        }

        public Pessoa GetByDocumento(string documento)
        {
            return _pessoaRepository.GetByDocumento(documento);
        }
    }
}
