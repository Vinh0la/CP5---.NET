using CrudMongoApi.Controllers;
using CrudMongoApi.Models;
using CrudMongoApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace TestProject
{
    public class ProdutosControllerTests
    {
        private readonly ProdutosController _controller;
        private readonly ProdutoRepository _repository;

        public ProdutosControllerTests()
        {
            // Mock o repositório para os testes
            _repository = new ProdutoRepository(); // Substituir com mock de repositório
            _controller = new ProdutosController(_repository);
        }

        [Fact]
        public async Task Create_ReturnsCreatedResult()
        {
            var novoProduto = new Produto { Nome = "Produto Teste", Preco = 10.0M };

            var result = await _controller.Create(novoProduto);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("Produto Teste", ((Produto)createdResult.Value).Nome);
        }
    }

}