using DesafioAutoglass.Domain.Models;
using System;
using System.Collections.Generic;

namespace DesafioAutoglass.Domain.Repositories
{
    public interface IProdutoRepository : IRepositoryBase<Produto, int>
    {
        IEnumerable<Produto> Buscar(string descricao, DateTime? validoAte, string descricaoFornecedor, string cnpjFornecedor, int skip, int count);
    }
}
