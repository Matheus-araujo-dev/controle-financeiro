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
    public class FormasPagamentoController : ControllerBase
    {
        private readonly IFormaPagamentoAppService _service;

        public FormasPagamentoController(IFormaPagamentoAppService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public ActionResult<FormaPagamento> GetById(Guid id)
        {
            var forma = _service.GetById(id);
            if (forma == null)
                return NotFound();
            return Ok(forma);
        }

        [HttpGet("pessoa/{pessoaId}")]
        public ActionResult<IEnumerable<FormaPagamento>> GetByPessoa(Guid pessoaId)
        {
            return Ok(_service.GetByPessoa(pessoaId));
        }

        [HttpGet("cartao/{cartaoId}")]
        public ActionResult<IEnumerable<FormaPagamento>> GetByCartao(Guid cartaoId)
        {
            return Ok(_service.GetByCartao(cartaoId));
        }

        [HttpPost]
        public IActionResult Create(FormaPagamento forma)
        {
            _service.Add(forma);
            return CreatedAtAction(nameof(GetById), new { id = forma.Id }, forma);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, FormaPagamento forma)
        {
            if (id != forma.Id)
                return BadRequest();
            _service.Update(forma);
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
