using Microsoft.EntityFrameworkCore;
using MVC.Business.Interfaces;
using MVC.Business.Models;
using MVC.Data.Context;

namespace MVC.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(MeuDbContext context) : base(context) { }

        public async Task<Endereco> ObterEnderecoPorFonecedor(Guid fornecedorId)
            => await Context.Enderecos.AsNoTrackingWithIdentityResolution()
                .FirstAsync(p => p.FornecedorId.Equals(fornecedorId));
    }
}
