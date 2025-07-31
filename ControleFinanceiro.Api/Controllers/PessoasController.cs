using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ControleFinanceiro.Application.Services;
using ControleFinanceiro.Domain.Entities;

namespace ControleFinanceiro.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoasController : ControllerBase
    {
        private readonly IPessoaAppService _service;

        public PessoasController(IPessoaAppService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Pessoa>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Pessoa> GetById(Guid id)
        {
            var pessoa = _service.GetById(id);
            if (pessoa == null)
                return NotFound();
            return Ok(pessoa);
        }

        [HttpGet("busca")]
        public ActionResult<IEnumerable<Pessoa>> GetByNome([FromQuery] string nome)
        {
            return Ok(_service.GetByNome(nome));
        }

        [HttpGet("documento/{documento}")]
        public ActionResult<Pessoa> GetByDocumento(string documento)
        {
            var pessoa = _service.GetByDocumento(documento);
            if (pessoa == null)
                return NotFound();
            return Ok(pessoa);
        }

        [HttpPost]
        public IActionResult Create(Pessoa pessoa)
        {
            _service.Create(pessoa);
            return CreatedAtAction(nameof(GetById), new { id = pessoa.Id }, pessoa);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, Pessoa pessoa)
        {
            if (id != pessoa.Id)
                return BadRequest();
            _service.Update(pessoa);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _service.Delete(id);
            return NoContent();
        }
    }
}
