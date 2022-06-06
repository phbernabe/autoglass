using AutoMapper;
using DesafioAutoglass.Application.Dtos.Fornecedor;
using DesafioAutoglass.Application.Interfaces;
using DesafioAutoglass.Domain.Exceptions;
using DesafioAutoglass.Domain.Interfaces;
using DesafioAutoglass.Domain.Models;
using System.Threading.Tasks;

namespace DesafioAutoglass.Application.Services
{
    public class FornecedorAppService : IFornecedorAppService
    {
        private readonly IFornecedorService _service;
        private readonly IMapper _mapper;

        public FornecedorAppService(
            IFornecedorService service,
            IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<FornecedorOutputDto> GetAsync(int id)
        {
            if (id > 0)
            {
                var fornecedor = await _service.BuscarPorIdAsync(id);

                if (fornecedor != null)
                {
                    return _mapper.Map<FornecedorOutputDto>(fornecedor);
                }
            }

            return null;
        }

        public async Task<FornecedorOutputDto> InsertAsync(AdicionarFornecedorInputDto input)
        {
            var fornecedor = _mapper.Map<Fornecedor>(input);

            var output = await _service.InserirAsync(fornecedor);

            return _mapper.Map<FornecedorOutputDto>(output);
        }

        public async Task UpdateAsync(EditarFornecedorInputDto input)
        {
            var fornecedor = await _service.BuscarPorIdAsync(input.Codigo);

            if (fornecedor is null)
            {
                throw new EntityNotFoundException(typeof(Fornecedor).Name, input.Codigo);
            }

            fornecedor = _mapper.Map<Fornecedor>(input);

            await _service.EditarAsync(fornecedor);
        }
    }
}
