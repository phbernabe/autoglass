using DesafioAutoglass.Application.Dtos.Produto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioAutoglass.Application.Interfaces
{
    public interface IProdutoAppService
    {
        IEnumerable<ProdutoOutputDto> Search(string descricao, DateTime? validoAte, string descricaoFornecedor, string cnpjFornecedor, int skip, int count);
        Task<ProdutoOutputDto> GetAsync(int id);
        Task<ProdutoOutputDto> InsertAsync(AdicionarProdutoInputDto input);
        Task UpdateAsync(EditarProdutoInputDto input);
        Task RemoveAsync(int id);
    }
}
