using AutoMapper;
using DesafioAutoglass.Application.AutoMapper;
using DesafioAutoglass.Application.Controllers;
using DesafioAutoglass.Application.Dtos.Produto;
using DesafioAutoglass.Application.Services;
using DesafioAutoglass.Domain.Extensions;
using DesafioAutoglass.Domain.Models;
using DesafioAutoglass.Domain.Repositories;
using DesafioAutoglass.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DesafioAutoglass.Application.UnitTests.Services
{
    public class ProdutoAppServiceTests
    {
        private readonly ProdutosController sut;
        private readonly Mock<IProdutoRepository> produtoRepositoryStub = new();
        private readonly Mock<IFornecedorRepository> fornecedorRepositoryStub = new();
        //private readonly Mock<IMapper> mapperStub = new();
        private readonly Random rand = new();

        private static IMapper _mapper;

        public ProdutoAppServiceTests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new DomainToViewModelMappingProfile());
                    mc.AddProfile(new ViewModelToDomainMappingProfile());
                });

                _mapper = mappingConfig.CreateMapper();
            }

            sut = new ProdutosController(new ProdutoAppService(
                new ProdutoService(produtoRepositoryStub.Object),
                new FornecedorService(fornecedorRepositoryStub.Object),
                _mapper)
            );
        }

        [Fact]
        public async Task Get_UnexistingItem_ReturnsNull()
        {
            // Arrange
            Produto expected = null;
            produtoRepositoryStub.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(expected);

            // Act
            var result = await sut.Get(rand.Next(0, 100));

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Get_ExistingItem_ReturnsExpectedItem()
        {
            // Arrage
            int produtoId = 1;
            //int fornecedorId = 1;

            var produto = CriarProdutoAleatorio();
            produto.Fornecedor = CriarFornecedorAleatorio();

            produtoRepositoryStub.Setup(x => x.GetByIdAsync(produtoId)).ReturnsAsync(produto);

            // Act
            var result = await sut.Get(produtoId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);
            var output = ((result.Result as OkObjectResult).Value as ProdutoOutputDto);

            Assert.Equal(produto.Descricao, output.Descricao);
            Assert.Equal(produto.DataFabricacao, output.DataFabricacao);
            Assert.Equal(produto.DataValidade, output.DataValidade);
            Assert.Equal(produto.Situacao.GetEnumDisplayName(), output.Situacao);
            Assert.Equal(produto.Fornecedor.Descricao, output.Fornecedor.Descricao);
        }

        [Fact]
        public async Task Get_ExistingItems_ReturnsAllItems()
        {
            // Arrage
            int itemsCount = 50;
            var expected = new List<Produto>();
            for (int x = 0; x < itemsCount; x++)
            {
                expected.Add(CriarProdutoAleatorio());
            }

            produtoRepositoryStub.Setup(x => x.Buscar(null, null, null, null, 0, 0)).Returns(expected);

            // Act 
            var result = await sut.Get(null, null, null, null, 0, 0);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var output = ((result.Result as OkObjectResult).Value as IEnumerable<ProdutoOutputDto>).ToList();

            var randomItem = rand.Next(0, 49);
            Assert.Equal(expected[randomItem].Descricao, output[randomItem].Descricao);
            Assert.Equal(expected[randomItem].DataFabricacao, output[randomItem].DataFabricacao);
            Assert.Equal(expected[randomItem].DataValidade, output[randomItem].DataValidade);
        }

        [Fact]
        public async Task Inserir_Produto_Async()
        {
            // Arrange
            int fornecedorId = 1;
            var fornecedor = CriarFornecedorAleatorio();
            var produto = CriarProdutoAleatorio();

            var produtoInputDto = new AdicionarProdutoInputDto
            {
                Descricao = produto.Descricao,
                DataFabricacao = produto.DataFabricacao,
                DataValidade = produto.DataValidade,
                Situacao = produto.Situacao,
                CodigoFornecedor = fornecedorId
            };

            produtoRepositoryStub.Setup(x => x.AddAsync(It.IsAny<Produto>())).ReturnsAsync(produto);
            fornecedorRepositoryStub.Setup(x => x.GetByIdAsync(fornecedorId)).ReturnsAsync(fornecedor);

            // Act
            var result = await sut.Post(produtoInputDto);

            // Assert
            var created = (result.Result as CreatedAtActionResult).Value as ProdutoOutputDto;
            Assert.Equal(produtoInputDto.Descricao, created.Descricao);
            Assert.Equal(produtoInputDto.DataFabricacao, created.DataFabricacao);
            Assert.Equal(produtoInputDto.DataValidade, created.DataValidade);
            Assert.Equal(produtoInputDto.Situacao.GetEnumDisplayName(), created.Situacao);
        }

        private Produto CriarProdutoAleatorio()
        {
            var randomDate = new DateTime(rand.Next(2000, 2022), rand.Next(1, 12), rand.Next(1, 28));

            return new()
            {
                Descricao = Guid.NewGuid().ToString(),
                DataFabricacao = randomDate,
                DataValidade = randomDate.AddDays(rand.Next(1, 90)),
                Situacao = Situacao.Ativo,
                Fornecedor = new Fornecedor
                {
                    Descricao = "Acme Ltda",
                    Cnpj = "01225698000198"
                }
            };
        }

        private Fornecedor CriarFornecedorAleatorio()
        {
            return new()
            {
                Descricao = Guid.NewGuid().ToString(),
                Cnpj = "01225698000198"
            };
        }
    }
}
