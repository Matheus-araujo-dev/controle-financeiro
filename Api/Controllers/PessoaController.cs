using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPessoas()
        {
            var pessoas = new[] { new { Id = 1, Nome = "Joao" } };
            return Ok(pessoas);
        }

        [HttpPost]
        public IActionResult CreatePessoa([FromBody] object pessoa)
        {
            // Here you would add persistence logic
            return Created("/pessoa/1", pessoa);
        }
    }
}
