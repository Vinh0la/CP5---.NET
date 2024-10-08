using CrudMongoApi.Models;
using CrudMongoApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CrudMongoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly ProdutoRepository _repository;

        public ProdutosController(ProdutoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetAll() => Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetById(string id)
        {
            var produto = await _repository.GetByIdAsync(id);
            if (produto == null) return NotFound();
            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Produto produto)
        {
            await _repository.CreateAsync(produto);
            return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Produto produto)
        {
            var existingProduct = await _repository.GetByIdAsync(id);
            if (existingProduct == null) return NotFound();
            await _repository.UpdateAsync(id, produto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingProduct = await _repository.GetByIdAsync(id);
            if (existingProduct == null) return NotFound();
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }

}
