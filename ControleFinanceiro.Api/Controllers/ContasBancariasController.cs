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
    public class ContasBancariasController : ControllerBase
    {
        private readonly IContaBancariaAppService _service;

        public ContasBancariasController(IContaBancariaAppService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public ActionResult<ContaBancaria> GetById(Guid id)
        {
            var conta = _service.GetById(id);
            if (conta == null)
                return NotFound();
            return Ok(conta);
        }

        [HttpGet("pessoa/{pessoaId}")]
        public ActionResult<IEnumerable<ContaBancaria>> GetByPessoa(Guid pessoaId)
        {
            return Ok(_service.GetByPessoa(pessoaId));
        }

        [HttpPost]
        public IActionResult Create(ContaBancaria conta)
        {
            _service.Add(conta);
            return CreatedAtAction(nameof(GetById), new { id = conta.Id }, conta);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, ContaBancaria conta)
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
