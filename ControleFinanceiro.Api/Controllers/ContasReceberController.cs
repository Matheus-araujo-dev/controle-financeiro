using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ControleFinanceiro.Application.Services;
using ControleFinanceiro.Domain.Entities;

namespace ControleFinanceiro.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ContasReceberController : ControllerBase
    {
        private readonly IContaReceberAppService _service;

        public ContasReceberController(IContaReceberAppService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public ActionResult<ContaReceber> GetById(Guid id)
        {
            var conta = _service.GetById(id);
            if (conta == null)
                return NotFound();
            return Ok(conta);
        }

        [HttpGet("pessoa/{pessoaId}")]
        public ActionResult<IEnumerable<ContaReceber>> GetByPessoa(Guid pessoaId)
        {
            return Ok(_service.GetByPessoa(pessoaId));
        }

        [HttpGet("periodo")]
        public ActionResult<IEnumerable<ContaReceber>> GetByPeriodo([FromQuery] DateTime inicio, [FromQuery] DateTime fim)
        {
            return Ok(_service.GetByPeriodo(inicio, fim));
        }

        [HttpGet("status/{estaRecebido}")]
        public ActionResult<IEnumerable<ContaReceber>> GetByStatus(bool estaRecebido)
        {
            return Ok(_service.GetByStatus(estaRecebido));
        }

        [HttpPost]
        public IActionResult Create(ContaReceber conta)
        {
            _service.Add(conta);
            return CreatedAtAction(nameof(GetById), new { id = conta.Id }, conta);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, ContaReceber conta)
        {
            if (id != conta.Id)
                return BadRequest();
            _service.Update(conta);
            return NoContent();
        }

        [HttpPut("{id}/receber")]
        public IActionResult MarcarComoRecebida(Guid id, [FromBody] DateTime? data)
        {
            var conta = _service.GetById(id);
            if (conta == null)
                return NotFound();
            conta.EstaRecebido = true;
            conta.DataRecebimento = data ?? DateTime.Now;
            _service.Update(conta);
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
