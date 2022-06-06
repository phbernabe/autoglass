using DesafioAutoglass.Domain.Models;
using DesafioAutoglass.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DesafioAutoglass.Data.Repositories
{
    public class FornecedorRepository : RepositoryBase<Fornecedor, int>, IFornecedorRepository
    {
        public FornecedorRepository(ApplicationDbContext context)
            : base(context)
        {

        }

        public async Task<Fornecedor> GetByCnpjAsync(string cnpj)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Cnpj == cnpj);
        }
    }
}
