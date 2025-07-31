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
    public class ContasPagarController : ControllerBase
    {
        private readonly IContaPagarAppService _service;

        public ContasPagarController(IContaPagarAppService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public ActionResult<ContaPagar> GetById(Guid id)
        {
            var conta = _service.GetById(id);
            if (conta == null)
                return NotFound();
            return Ok(conta);
        }

        [HttpGet("pessoa/{pessoaId}")]
        public ActionResult<IEnumerable<ContaPagar>> GetByPessoa(Guid pessoaId)
        {
            return Ok(_service.GetByPessoa(pessoaId));
        }

        [HttpGet("periodo")]
        public ActionResult<IEnumerable<ContaPagar>> GetByPeriodo([FromQuery] DateTime inicio, [FromQuery] DateTime fim)
        {
            return Ok(_service.GetByPeriodo(inicio, fim));
        }

        [HttpGet("status/{estaPaga}")]
        public ActionResult<IEnumerable<ContaPagar>> GetByStatus(bool estaPaga)
        {
            return Ok(_service.GetByStatus(estaPaga));
        }

        [HttpPost]
        public IActionResult Create(ContaPagar conta)
        {
            _service.Add(conta);
            return CreatedAtAction(nameof(GetById), new { id = conta.Id }, conta);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, ContaPagar conta)
        {
            if (id != conta.Id)
                return BadRequest();
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
