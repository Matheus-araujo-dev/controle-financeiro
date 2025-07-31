using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ControleFinanceiro.Application.Services;
using ControleFinanceiro.Domain.Entities;

namespace ControleFinanceiro.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionAppService _service;

        public TransactionsController(ITransactionAppService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Create(Transaction transaction)
        {
            _service.Add(transaction);
            return Created(string.Empty, transaction);
        }
    }
}
