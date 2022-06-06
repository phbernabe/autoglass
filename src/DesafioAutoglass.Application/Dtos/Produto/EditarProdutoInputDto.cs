using System.ComponentModel.DataAnnotations;

namespace DesafioAutoglass.Application.Dtos.Produto
{
    public class EditarProdutoInputDto : AdicionarProdutoInputDto
    {
        [Required]
        public int Codigo { get; set; }
    }
}
