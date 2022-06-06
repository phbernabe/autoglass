using AutoMapper;
using DesafioAutoglass.Application.Dtos.Produto;
using DesafioAutoglass.Application.Helpers;
using DesafioAutoglass.Application.Interfaces;
using DesafioAutoglass.Domain.Exceptions;
using DesafioAutoglass.Domain.Interfaces;
using DesafioAutoglass.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioAutoglass.Application.Services
{
    public class ProdutoAppService : IProdutoAppService
    {
        private readonly IProdutoService _produtoService;
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;

        public ProdutoAppService(
            IProdutoService produtoService,
            IFornecedorService fornecedorService,
            IMapper mapper)
        {
            _produtoService = produtoService;
            _fornecedorService = fornecedorService;
            _mapper = mapper;
        }

        public IEnumerable<ProdutoOutputDto> Search(string descricao = null, DateTime? validoAte = null, string descricaoFornecedor = null, string cnpj = null, int skip = 0, int count = 20)
        {
            cnpj = DataHelper.OnlyDigits(cnpj);

            var produtos = _produtoService.Pesquisar(descricao, validoAte, descricaoFornecedor, cnpj, skip, count);

            return _mapper.Map<List<ProdutoOutputDto>>(produtos);
        }

        public async Task<ProdutoOutputDto> GetAsync(int id)
        {
            if (id > 0)
            {
                var produto = await _produtoService.BuscarPorIdAsync(id);

                if (produto != null)
                {
                    return _mapper.Map<ProdutoOutputDto>(produto);
                }
            }

            return null;
        }

        public async Task RemoveAsync(int id)
        {
            if (id > 0)
            {
                await _produtoService.ExcluirAsync(id);
            }
        }

        public async Task<ProdutoOutputDto> InsertAsync(AdicionarProdutoInputDto input)
        {
            var fornecedor = await _fornecedorService.BuscarPorIdAsync(input.CodigoFornecedor);

            if (fornecedor is null)
            {
                throw new EntityNotFoundException(typeof(Fornecedor).Name, input.CodigoFornecedor);
            }

            var produto = _mapper.Map<Produto>(input);
            produto.Fornecedor = fornecedor;

            var output = await _produtoService.InserirAsync(produto);

            return _mapper.Map<ProdutoOutputDto>(output);
        }

        public async Task UpdateAsync(EditarProdutoInputDto input)
        {
            var produto = await _produtoService.BuscarPorIdAsync(input.Codigo);

            if (produto is null)
            {
                throw new EntityNotFoundException(typeof(Produto).Name, input.Codigo);
            }

            var fornecedor = await _fornecedorService.BuscarPorIdAsync(input.CodigoFornecedor);

            if (fornecedor is null)
            {
                throw new EntityNotFoundException(typeof(Fornecedor).Name, input.CodigoFornecedor);
            }

            _mapper.Map<EditarProdutoInputDto, Produto>(input, produto);

            produto.Fornecedor = fornecedor;

            await _produtoService.EditarAsync(produto);
        }
    }
}
