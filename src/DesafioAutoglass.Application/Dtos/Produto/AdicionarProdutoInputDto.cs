using DesafioAutoglass.Domain.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace DesafioAutoglass.Application.Dtos.Produto
{
    public class AdicionarProdutoInputDto
    {
        [Required]
        public string Descricao { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataFabricacao { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataValidade { get; set; }

        [Required]
        [EnumDataType(typeof(Situacao), ErrorMessage = "Situação inválida")]
        public Situacao Situacao { get; set; }

        [Required]
        public int CodigoFornecedor { get; set; }
    }
}
