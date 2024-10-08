using CrudMongoApi.Configurations;
using CrudMongoApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CrudMongoApi.Repositories
{
     public class ProdutoRepository
    {
        private readonly IMongoCollection<Produto> _produtos;

        public ProdutoRepository(IOptions<MongoDBSettings> mongoSettings)
        {
            var client = new MongoClient(mongoSettings.Value.ConnectionString);
            var database = client.GetDatabase(mongoSettings.Value.DatabaseName);
            _produtos = database.GetCollection<Produto>(mongoSettings.Value.CollectionName);
        }

        public ProdutoRepository()
        {
        }

        public async Task<List<Produto>> GetAllAsync() => await _produtos.Find(_ => true).ToListAsync();
        public async Task<Produto> GetByIdAsync(string id) => await _produtos.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task CreateAsync(Produto produto) => await _produtos.InsertOneAsync(produto);
        public async Task UpdateAsync(string id, Produto produto) => await _produtos.ReplaceOneAsync(x => x.Id == id, produto);
        public async Task DeleteAsync(string id) => await _produtos.DeleteOneAsync(x => x.Id == id);
    }

}
