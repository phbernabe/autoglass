using AutoMapper;
using DesafioAutoglass.Application.Dtos.Fornecedor;
using DesafioAutoglass.Application.Dtos.Produto;
using DesafioAutoglass.Domain.Extensions;
using DesafioAutoglass.Domain.Models;

namespace DesafioAutoglass.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Produto, ProdutoOutputDto>()
                .ForMember(x => x.Codigo, options => options.MapFrom(src => src.Id))
                .ForMember(x => x.Situacao, options => options.MapFrom(src => src.Situacao.GetEnumDisplayName()));

            CreateMap<Fornecedor, FornecedorOutputDto>().ForMember(x => x.Codigo, options => options.MapFrom(src => src.Id));
        }
    }
}