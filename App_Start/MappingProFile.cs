using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Bazaar.Domain.Dto;
using Bazaar.Domain.Dtos;
using Bazaar.Domain.Entities;
using Bazaar.Domain.ViewModel;
using Bazaar.Dtos;

namespace Bazaar.App_Start
{
    public class MappingProFile : Profile
    {

        public MappingProFile()
        {
            Mapper.CreateMap<Game, GameDto>();
            Mapper.CreateMap<GameDto, Game>();
            Mapper.CreateMap<FaqItem, FaqItemDto>();
            Mapper.CreateMap<FaqItemDto, FaqItem>();
            Mapper.CreateMap<FaqItemViewModel, FaqItem>();
            Mapper.CreateMap<FaqItem, FaqItemViewModel>();
            Mapper.CreateMap<CarouselSlide, CarouselSlideDto>();
            Mapper.CreateMap<CarouselSlideDto, CarouselSlide>();
            Mapper.CreateMap<CarouselSlide, SlideViewModel>();
            Mapper.CreateMap<SlideViewModel, CarouselSlide>();
        }
    }
}