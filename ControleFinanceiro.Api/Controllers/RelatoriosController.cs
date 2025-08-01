using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ControleFinanceiro.Application.Services;
using ControleFinanceiro.Application.Dtos;
using ControleFinanceiro.Domain.Entities;

namespace ControleFinanceiro.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RelatoriosController : ControllerBase
    {
        private readonly IRelatorioFinanceiroAppService _service;

        public RelatoriosController(IRelatorioFinanceiroAppService service)
        {
            _service = service;
        }

        [HttpGet("movimentacoes")]
        public ActionResult<ResumoMovimentacaoDto> GetMovimentacoes([FromQuery] DateTime inicio, [FromQuery] DateTime fim)
        {
            return Ok(_service.ResumoMovimentacoes(inicio, fim));
        }

        [HttpGet("movimentacoes/tipo")]
        public ActionResult<decimal> GetMovimentacoesPorTipo([FromQuery] DateTime inicio, [FromQuery] DateTime fim, [FromQuery] TipoMovimentacao tipo)
        {
            return Ok(_service.TotalMovimentacoesPorTipo(inicio, fim, tipo));
        }

        [HttpGet("contas")]
        public ActionResult<ResumoContasDto> GetContas([FromQuery] DateTime inicio, [FromQuery] DateTime fim)
        {
            return Ok(_service.ResumoContas(inicio, fim));
        }
    }
}
