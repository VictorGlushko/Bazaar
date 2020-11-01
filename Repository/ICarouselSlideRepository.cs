using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bazaar.Domain.Entities;

namespace Bazaar.Repository
{
    public interface ICarouselSlideRepository
    {
        IEnumerable<CarouselSlide> GetCarouselSlides();

        CarouselSlide GetCarouselSlide(int id);
        void AddCarouselSlide(CarouselSlide item);

        void UpdateCarouselSlide(int id, CarouselSlide item);

        void DeleteCarouselSlide(int id);
    }
}
