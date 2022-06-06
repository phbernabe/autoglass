using DesafioAutoglass.Domain.Models;
using DesafioAutoglass.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesafioAutoglass.Data.Repositories
{
    public class ProdutoRepository : RepositoryBase<Produto, int>, IProdutoRepository
    {
        public ProdutoRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public IEnumerable<Produto> Buscar(string descricao = null, DateTime? validoAte = null, string descricaoFornecedor = null, string cnpjFornecedor = null, int skip = 0, int count = 20)
        {
            var query = GetAll().Where(x =>
                (string.IsNullOrEmpty(descricao) || x.Descricao.ToUpper().Contains(descricao.ToUpper())) ||
                (string.IsNullOrEmpty(descricaoFornecedor) || x.Fornecedor.Descricao.ToUpper().Contains(descricaoFornecedor.ToUpper())) ||
                (string.IsNullOrEmpty(cnpjFornecedor) || x.Fornecedor.Cnpj.Equals(cnpjFornecedor)) ||
                (!validoAte.HasValue || x.DataValidade <= validoAte.Value)
            )
                .Skip(skip)
                .Take(count);

            return query.ToList();
        }

        public override void Remove(Produto produto)
        {
            if (produto != null)
            {
                produto.Situacao = Situacao.Inativo;
                Update(produto);
            }
        }
    }
}
