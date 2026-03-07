using AutoMapper;
using TicketManagementProject.API.Entities;
using TicketManagementProject.Shared.DTOs;

namespace TicketManagementProject.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Ticket, TicketDto>();

            CreateMap<TicketDto, Ticket>();

            CreateMap<TicketComment, CommentDto>().ReverseMap();
        }
    }
}
