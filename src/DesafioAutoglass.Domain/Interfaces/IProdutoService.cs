using DesafioAutoglass.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioAutoglass.Domain.Interfaces
{
    public interface IProdutoService
    {
        IEnumerable<Produto> Pesquisar(string descricao, DateTime? validoAte, string descricaoFornecedor, string cnpjFornecedor, int skip, int count);
        Task<Produto> BuscarPorIdAsync(int id);
        Task<Produto> InserirAsync(Produto produto);
        Task EditarAsync(Produto produto);
        Task ExcluirAsync(int codigo);
    }
}
