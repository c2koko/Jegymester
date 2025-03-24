using AutoMapper;
using Jegymester.Dtos;
using Jegymester.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jegymester.ServicesA
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            //Screening Mappings
            CreateMap<Screening, ScreeningDto>().ReverseMap();
            CreateMap<ScreeningCreateDto, Screening>();
            CreateMap<ScreeningUpdateDto, Screening>();
            CreateMap<Movie, MovieDto>().ReverseMap();

            //További Mappings-ek ide le
        }
    }
}
