using AutoMapper;
using Jegymester.Dtos;
using Jegymester.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jegymester.DataContext.Dtos;
using MovieDto = Jegymester.DataContext.Dtos.MovieDto;

namespace Jegymester.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Screening Mappings
            CreateMap<Screening, ScreeningDto>().ReverseMap();
            CreateMap<ScreeningCreateDto, Screening>();
            CreateMap<ScreeningUpdateDto, Screening>();
            CreateMap<Movie, MovieDto>().ReverseMap();
            CreateMap<Ticket, TicketDto>().ReverseMap();
            CreateMap<Screening, ScreeningDetailsDto>()
                .ForMember(dest => dest.MovieName, opt => opt.MapFrom(src => src.Movie.MovieName))
                .ReverseMap();


            //Ticket Mappings
            CreateMap<Ticket, TicketDto>();     //fogalmam sincs még hogy reverse map vagy sima
            CreateMap<TicketCreateDto, Ticket>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.ScreeningId, opt => opt.MapFrom(src => src.ScreeningId));

            //Movies Mapping
            CreateMap<Movie, MovieDto>().ReverseMap();
            CreateMap<MovieCreateDto, MovieDto>();
            CreateMap<MovieUpdateDto, MovieDto>();

            CreateMap<Screening, ScreeningDto>();

        }
    }
}
