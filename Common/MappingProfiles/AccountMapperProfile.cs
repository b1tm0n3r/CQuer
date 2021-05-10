using AutoMapper;
using Common.DataModels.IdentityManagement;
using Common.DTOs;

namespace Common.MappingProfiles
{
    public class AccountMapperProfile : Profile
    {
        public AccountMapperProfile()
        {
            CreateMap<Account, AccountDto>()
                .ForMember(dto => dto.Username, x => x.MapFrom(x => x.Name));
        }
    }
}