using AutoMapper;
using Jegymester.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jegymester.DataContext.Dtos;
using Jegymester.Dtos;

namespace Jegymester.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Roles mapping
            CreateMap<Role, RoleDto>().ReverseMap();


            //User mapping
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserRegisterDto, User>();
            //CreateMap<NotRegisteredUserDto, User>();
            //CreateMap<User, NotRegisteredUserDto>();
            CreateMap<UserUpdateDto, User>();


            //Movies Mapping
            CreateMap<Movie, MovieDto>();
            CreateMap<MovieCreateDto, Movie>();
            CreateMap<MovieUpdateDto, Movie>();


            //Screening Mapping
            CreateMap<Screening, ScreeningDto>().ReverseMap();
            CreateMap<ScreeningCreateDto, Screening>()
                .ForMember(d => d.MovieId, o => o.MapFrom(s => s.MovieId));
            CreateMap<ScreeningUpdateDto, Screening>()
                .ForMember(d => d.MovieId, o => o.MapFrom(s => s.MovieId));


            CreateMap<Screening, ScreeningCreateDto>().ReverseMap();


            //Ticket Mapping
            CreateMap<Ticket, TicketDto>().ReverseMap()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Screening, opt => opt.MapFrom(src => src.Screening))
                .ForMember(dest => dest.Chairs, opt => opt.MapFrom(src => src.Chairs));
            CreateMap<TicketCreateDto, Ticket>()
                .ForMember(d => d.ScreeningId, o => o.MapFrom(s => s.ScreeningId))
                .ForMember(d => d.UserId, o => o.MapFrom(s => s.UserId));
            CreateMap<TicketVerifyDto, Ticket>();

            //Chair mapping
            CreateMap<Chair, ChairDto>().ReverseMap();
        }
    }
}

