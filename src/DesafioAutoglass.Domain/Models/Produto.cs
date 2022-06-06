using System;

namespace DesafioAutoglass.Domain.Models
{
    public class Produto : Entity<int>
    {
        public string Descricao { get; set; }
        public Situacao Situacao { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; }

        public virtual Fornecedor Fornecedor { get; set; }
    }
}
