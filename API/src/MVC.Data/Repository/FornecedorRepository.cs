using Microsoft.EntityFrameworkCore;
using MVC.Business.Interfaces;
using MVC.Business.Models;
using MVC.Data.Context;

namespace MVC.Data.Repository
{
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(MeuDbContext context) : base(context) { }

        public async Task<Fornecedor> ObterFornecedorEndereco(Guid id)
            => await Context.Fornecedores.AsNoTrackingWithIdentityResolution()
                    .Include(p => p.Endereco)
                    .FirstAsync(p => p.Id.Equals(id));

        public async Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id)
            => await Context.Fornecedores.AsNoTrackingWithIdentityResolution()
                    .Include(p => p.Produtos)
                    .Include(p => p.Endereco)
                    .FirstAsync(p => p.Id.Equals(id));
    }
}
