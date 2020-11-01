using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Bazaar.Domain.Dto;
using Bazaar.Domain.Dtos;
using Bazaar.Domain.Entities;
using Bazaar.Repository;

namespace Bazaar.Controllers.Api
{
    public class CarouselSlideController : ApiController
    {

        private readonly IUnitOfWork _unitOfWork;

        public CarouselSlideController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //GET /api/CarouselSlides
        public IEnumerable<CarouselSlideDto> GetCarouselSlide()
        {
            return _unitOfWork.CarouselSlides.GetCarouselSlides().Select(Mapper.Map<CarouselSlide, CarouselSlideDto>);
        }

        //POST /api/FaqItems
        [HttpPost]
        public IHttpActionResult CreateCarouselSlide(CarouselSlideDto itemDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var carouselSlide = Mapper.Map<CarouselSlideDto, CarouselSlide>(itemDto);
            _unitOfWork.CarouselSlides.AddCarouselSlide(carouselSlide);
            _unitOfWork.Complete();

            itemDto.Id = carouselSlide.Id;

            return Created(new Uri(Request.RequestUri + "/" + itemDto.Id), itemDto);
        }

        //PUT /api/FaqItems/
        [HttpPut]
        public void UpdateCarouselSlide(int id, CarouselSlide item)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var carouselSlide = Mapper.Map<CarouselSlide>(item);
            _unitOfWork.CarouselSlides.UpdateCarouselSlide(id, carouselSlide);
            _unitOfWork.Complete();

        }

        [HttpDelete]
        public void DeleteCarouselSlide(int id)
        {
            var faqItem = _unitOfWork.CarouselSlides.GetCarouselSlide(id);

            if (faqItem == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _unitOfWork.CarouselSlides.DeleteCarouselSlide(id);
            _unitOfWork.Complete();

        }

    }
}
