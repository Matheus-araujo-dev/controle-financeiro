using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ControleFinanceiro.Application.Services;
using ControleFinanceiro.Domain.Entities;

namespace ControleFinanceiro.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartoesController : ControllerBase
    {
        private readonly ICartaoAppService _service;

        public CartoesController(ICartaoAppService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public ActionResult<Cartao> GetById(Guid id)
        {
            var cartao = _service.GetById(id);
            if (cartao == null)
                return NotFound();
            return Ok(cartao);
        }

        [HttpGet("pessoa/{pessoaId}")]
        public ActionResult<IEnumerable<Cartao>> GetByPessoa(Guid pessoaId)
        {
            return Ok(_service.GetByPessoa(pessoaId));
        }

        [HttpGet("vencendo")]
        public ActionResult<IEnumerable<Cartao>> GetVencendoAte([FromQuery] DateTime data)
        {
            return Ok(_service.GetVencendoAte(data));
        }

        [HttpPost]
        public IActionResult Create(Cartao cartao)
        {
            _service.Add(cartao);
            return CreatedAtAction(nameof(GetById), new { id = cartao.Id }, cartao);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, Cartao cartao)
        {
            if (id != cartao.Id)
                return BadRequest();
            _service.Update(cartao);
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
