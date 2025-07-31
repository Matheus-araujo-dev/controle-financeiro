using Microsoft.AspNetCore.Mvc;
using ControleFinanceiro.Shared.Utils;

namespace ControleFinanceiro.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { status = "ok", version = Constants.ApiVersion });
        }
    }
}
