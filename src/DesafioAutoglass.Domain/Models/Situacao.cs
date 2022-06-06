using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DesafioAutoglass.Domain.Models
{
    public enum Situacao
    {
        [Display(Name = "Ativo")]
        Ativo = 1,
        [Display(Name = "Inativo")]
        Inativo = 2
    }
}
