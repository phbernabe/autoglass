using DesafioAutoglass.Domain.Exceptions;
using DesafioAutoglass.Domain.Interfaces;
using DesafioAutoglass.Domain.Models;
using DesafioAutoglass.Domain.Repositories;
using DesafioAutoglass.Domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioAutoglass.Domain.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtos;

        public ProdutoService(IProdutoRepository produtos)
        {
            _produtos = produtos;
        }

        public async Task<Produto> BuscarPorIdAsync(int id)
        {
            return await _produtos.GetByIdAsync(id);
        }

        public IEnumerable<Produto> Pesquisar(string descricao = null, DateTime? validoAte = null, string descricaoFornecedor = null, string cnpjFornecedor = null, int skip = 0, int count = 20)
        {
            return _produtos.Buscar(descricao, validoAte, descricaoFornecedor, cnpjFornecedor, skip, count);
        }

        public async Task ExcluirAsync(int codigo)
        {
            var produto = await _produtos.GetByIdAsync(codigo);

            if (produto == null)
            {
                throw new EntityNotFoundException(typeof(Produto).Name, codigo);
            }

            _produtos.Remove(produto);
            await _produtos.SaveChangesAsync();
        }

        public async Task<Produto> InserirAsync(Produto produto)
        {
            await ValidateAsync(produto);

            produto = await _produtos.AddAsync(produto);
            await _produtos.SaveChangesAsync();

            return produto;
        }

        public async Task EditarAsync(Produto produto)
        {
            await ValidateAsync(produto);

            _produtos.Update(produto);
            await _produtos.SaveChangesAsync();
        }

        private async Task ValidateAsync(Produto produto)
        {
            var validator = new ProdutoValidator();
            var results = await validator.ValidateAsync(produto);

            if (!results.IsValid)
            {
                throw new ModelValidationException(results.Errors.Select(x => x.ErrorMessage).ToList());
            }            
        }
    }
}
