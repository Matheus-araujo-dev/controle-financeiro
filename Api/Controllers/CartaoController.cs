using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartaoController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCartoes()
        {
            var cartoes = new[] { new { Id = 1, Numero = "1234" } };
            return Ok(cartoes);
        }

        [HttpPost]
        public IActionResult CreateCartao([FromBody] object cartao)
        {
            // Here you would add persistence logic
            return Created("/cartao/1", cartao);
        }
    }
}
