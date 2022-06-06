using DesafioAutoglass.Application.Dtos.Fornecedor;
using System;

namespace DesafioAutoglass.Application.Dtos.Produto
{
    public class ProdutoOutputDto
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; }
        public string Situacao { get; set; }
        public FornecedorOutputDto Fornecedor { get; set; }
    }
}
