using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Bazaar.Domain.Entities;
using Bazaar.Dtos;

namespace Bazaar.App_Start
{
    public class MappingProFile : Profile
    {

        public MappingProFile()
        {
            Mapper.CreateMap<Game, GameDto>();
            Mapper.CreateMap<GameDto, Game>();
        }
    }
}