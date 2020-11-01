using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Bazaar.Domain.Entities;
using Bazaar.Models;

namespace Bazaar.Repository
{
    public class CarouselSlideRepository : ICarouselSlideRepository
    {

        private readonly ApplicationDbContext _context;

        public CarouselSlideRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CarouselSlide> GetCarouselSlides()
        {
            return _context.CarouselSlides.ToList();
        }

        public CarouselSlide GetCarouselSlide(int id)
        {
            return _context.CarouselSlides.SingleOrDefault(s => s.Id == id);
        }

        public void AddCarouselSlide(CarouselSlide item)
        {
            _context.CarouselSlides.Add(item);
        }

        public void UpdateCarouselSlide(int id, CarouselSlide item)
        {
            var carouselSlideInDb = _context.CarouselSlides.SingleOrDefault(s => s.Id == id);

            carouselSlideInDb.Order = item.Order;
            carouselSlideInDb.Title = item.Title;
            carouselSlideInDb.Description = item.Description;

        }

        public void DeleteCarouselSlide(int id)
        {
            var carouselSlide = _context.CarouselSlides.SingleOrDefault(s => s.Id == id);
            _context.CarouselSlides.Remove(carouselSlide);
        }
    }
}