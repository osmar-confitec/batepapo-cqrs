using AutoMapper;
using BatePapo.Domain.Commands;
using BatePapo.ViewModel;

namespace BatePapo.Applicaton.AutoMapperApp
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CustomerViewModel, RegisterNewCustomerCommand>()
                .ConstructUsing(c => new RegisterNewCustomerCommand(  c.Name,  c.SurfaceName,  c.NickName,  c.BirthDate,  c.CPF));
            CreateMap<CustomerViewModel, UpdateCustomerCommand>()
                .ConstructUsing(c => new UpdateCustomerCommand(c.Id, c.Name, c.SurfaceName,c.NickName, c.BirthDate, c.CPF));
        }
    }
}
