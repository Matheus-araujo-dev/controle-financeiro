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
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaAppService _service;

        public CategoriasController(ICategoriaAppService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public ActionResult<Categoria> GetById(Guid id)
        {
            var cat = _service.GetById(id);
            if (cat == null)
                return NotFound();
            return Ok(cat);
        }

        [HttpGet("pessoa/{pessoaId}")]
        public ActionResult<IEnumerable<Categoria>> GetByPessoa(Guid pessoaId)
        {
            return Ok(_service.GetByPessoa(pessoaId));
        }

        [HttpPost]
        public IActionResult Create(Categoria categoria)
        {
            _service.Add(categoria);
            return CreatedAtAction(nameof(GetById), new { id = categoria.Id }, categoria);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, Categoria categoria)
        {
            if (id != categoria.Id)
                return BadRequest();
            _service.Update(categoria);
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
