using MVC.Business.Models;

namespace MVC.Business.Interfaces
{
    public interface IEnderecoRepository : IRepository<Endereco>
    {
        // Endereco por fornecedor
        Task<Endereco> ObterEnderecoPorFonecedor(Guid fornecedorId);
    }
}
