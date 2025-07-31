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
    public class MovimentacoesController : ControllerBase
    {
        private readonly IMovimentacaoFinanceiraAppService _service;

        public MovimentacoesController(IMovimentacaoFinanceiraAppService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public ActionResult<MovimentacaoFinanceira> GetById(Guid id)
        {
            var mov = _service.GetById(id);
            if (mov == null)
                return NotFound();
            return Ok(mov);
        }

        [HttpGet("pessoa/{pessoaId}")]
        public ActionResult<IEnumerable<MovimentacaoFinanceira>> GetByPessoa(Guid pessoaId)
        {
            return Ok(_service.GetByPessoa(pessoaId));
        }

        [HttpGet("periodo")]
        public ActionResult<IEnumerable<MovimentacaoFinanceira>> GetByPeriodo([FromQuery] DateTime inicio, [FromQuery] DateTime fim)
        {
            return Ok(_service.GetByPeriodo(inicio, fim));
        }

        [HttpGet("tipo/{tipo}")]
        public ActionResult<IEnumerable<MovimentacaoFinanceira>> GetByTipo(TipoMovimentacao tipo)
        {
            return Ok(_service.GetByTipo(tipo));
        }

        [HttpPost]
        public IActionResult Create(MovimentacaoFinanceira mov)
        {
            _service.Add(mov);
            return CreatedAtAction(nameof(GetById), new { id = mov.Id }, mov);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, MovimentacaoFinanceira mov)
        {
            if (id != mov.Id)
                return BadRequest();
            _service.Update(mov);
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
