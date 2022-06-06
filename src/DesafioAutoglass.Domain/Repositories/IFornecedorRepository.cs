using DesafioAutoglass.Domain.Models;
using System.Threading.Tasks;

namespace DesafioAutoglass.Domain.Repositories
{
    public interface IFornecedorRepository : IRepositoryBase<Fornecedor, int>
    {
        Task<Fornecedor> GetByCnpjAsync(string cnpj);
    }
}
