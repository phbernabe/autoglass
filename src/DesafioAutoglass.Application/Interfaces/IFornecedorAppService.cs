using DesafioAutoglass.Application.Dtos.Fornecedor;
using System.Threading.Tasks;

namespace DesafioAutoglass.Application.Interfaces
{
    public interface IFornecedorAppService
    {
        Task<FornecedorOutputDto> GetAsync(int id);
        Task<FornecedorOutputDto> InsertAsync(AdicionarFornecedorInputDto input);
        Task UpdateAsync(EditarFornecedorInputDto input);
        //Task RemoveAsync(int id);
    }
}
