using Microsoft.EntityFrameworkCore;
using MVC.Business.Interfaces;
using MVC.Business.Models;
using MVC.Data.Context;

namespace MVC.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(MeuDbContext context) : base(context) { }

        public async Task<Produto> ObterProdutoFornecedor(Guid id)
            => await Context.Produtos.AsNoTrackingWithIdentityResolution()
                    .Include(p => p.Fornecedor)
                    .FirstAsync(p => p.Id.Equals(id));

        public async Task<IEnumerable<Produto>> ObterProdutosFornecedores()
            => await Context.Produtos.AsNoTrackingWithIdentityResolution()
                    .Include(p => p.Fornecedor)
                    .OrderBy(p => p.Nome)
                    .ToListAsync();

        public async Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId)
            => await Buscar(p => p.FornecedorId.Equals(fornecedorId));
    }
}
