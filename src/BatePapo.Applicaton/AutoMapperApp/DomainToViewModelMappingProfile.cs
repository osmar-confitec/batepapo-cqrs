using AutoMapper;
using BatePapo.Domain.Models;
using BatePapo.ViewModel;

namespace BatePapo.Applicaton.AutoMapperApp
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Customer, CustomerViewModel>();
        }
    }
}
