using AutoMapper;
using Common.DataModels.StandardEntities;
using Common.DTOs;

namespace Common.MappingProfiles
{
    public class TicketMapperProfile : Profile
    {
        public TicketMapperProfile()
        {
            CreateMap<Ticket, TicketDto>();
        }
    }
}
