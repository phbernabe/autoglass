using AutoMapper;
using DesafioAutoglass.Application.Dtos.Fornecedor;
using DesafioAutoglass.Application.Dtos.Produto;
using DesafioAutoglass.Application.Helpers;
using DesafioAutoglass.Domain.Models;

namespace DesafioAutoglass.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile :Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<AdicionarProdutoInputDto, Produto>();

            CreateMap<EditarProdutoInputDto, Produto>()
                .ForMember(x => x.Id, options => options.MapFrom(src => src.Codigo));

            CreateMap<AdicionarFornecedorInputDto, Fornecedor>()
                .ForMember(x => x.Cnpj, options => options.MapFrom(src => DataHelper.OnlyDigits(src.Cnpj)));

            CreateMap<EditarFornecedorInputDto, Fornecedor>()
                .ForMember(x => x.Id, options => options.MapFrom(src => src.Codigo))
                .ForMember(x => x.Cnpj, options => options.MapFrom(src => DataHelper.OnlyDigits(src.Cnpj)));
        }
    }
}