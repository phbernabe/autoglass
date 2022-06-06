namespace DesafioAutoglass.Domain.Models
{
    public class Fornecedor : Entity<int>
    {
        public string Descricao { get; set; }
        public string Cnpj { get; set; }
    }
}
