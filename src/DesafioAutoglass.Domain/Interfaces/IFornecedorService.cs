using DesafioAutoglass.Domain.Models;
using System.Threading.Tasks;

namespace DesafioAutoglass.Domain.Interfaces
{
    public interface IFornecedorService
    {
        Task<Fornecedor> BuscarPorIdAsync(int id);
        Task<Fornecedor> BuscarPorCnpjAsync(string cnpj);
        Task<Fornecedor> InserirAsync(Fornecedor fornecedor);
        Task EditarAsync(Fornecedor fornecedor);
    }
}
