using DesafioAutoglass.Domain.Exceptions;
using DesafioAutoglass.Domain.Interfaces;
using DesafioAutoglass.Domain.Models;
using DesafioAutoglass.Domain.Repositories;
using DesafioAutoglass.Domain.Validators;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioAutoglass.Domain.Services
{
    public class FornecedorService : IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedores;

        public FornecedorService(IFornecedorRepository fornecedores)
        {
            _fornecedores = fornecedores;
        }

        public async Task<Fornecedor> BuscarPorCnpjAsync(string cnpj)
        {
            return await _fornecedores.GetByCnpjAsync(cnpj);
        }

        public async Task<Fornecedor> BuscarPorIdAsync(int id)
        {
            return await _fornecedores.GetByIdAsync(id);
        }

        public async Task<Fornecedor> InserirAsync(Fornecedor fornecedor)
        {
            await ValidateAsync(fornecedor);

            fornecedor = await _fornecedores.AddAsync(fornecedor);
            await _fornecedores.SaveChangesAsync();

            return fornecedor;
        }

        public async Task EditarAsync(Fornecedor fornecedor)
        {
            await ValidateAsync(fornecedor);

            _fornecedores.Update(fornecedor);
            await _fornecedores.SaveChangesAsync();
        }

        private async Task ValidateAsync(Fornecedor fornecedor)
        {
            var validator = new FornecedorValidator();
            var results = await validator.ValidateAsync(fornecedor);

            if (!results.IsValid)
            {
                throw new ModelValidationException(results.Errors.Select(x => x.ErrorMessage).ToList());
            }
        }
    }
}
