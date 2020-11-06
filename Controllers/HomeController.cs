using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Bazaar.Domain.Entities;
using Bazaar.Domain.ViewModel;
using Bazaar.Dtos;
using Bazaar.Models;
using Bazaar.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Bazaar.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private List<Game> _games;
        private List<Genre> _genres;
        private List<Game> _discountGames;
        private List<SlideViewModel> _slides;

        public HomeController(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
            _games = _unitOfWork.Games.GetGames().ToList();
            _genres = _unitOfWork.Genres.GetGenres().ToList();
            _discountGames = _unitOfWork.Games.GetGames().Where(g => g.FinalPrice < 500).ToList();
            _slides = _unitOfWork.CarouselSlides.GetCarouselSlides().Select(Mapper.Map<CarouselSlide,SlideViewModel>).ToList();
        }

      
        public ActionResult Index()
        {
            GamesViewModel gvm = new GamesViewModel();
            gvm.Games = _games;
            gvm.Genres = _genres;
            gvm.DiscountGames = _discountGames;
            gvm.Slides = _slides;

            return View(gvm);
        }
        public ActionResult GamesOrder(string order)
        {
            var games = _unitOfWork.Games;
            return View("Index", games);

        }

        public ActionResult OnSelectGamer(string order)
        {
            var games = _unitOfWork.Games.GetGames();
            switch (order)
            {
                case "date":
                {
                    games = games.OrderBy(g => g.ReleaseDate);
                }
                    break;
                case "polular":
                {
                    games = games.OrderBy(g => g.Sold);
                }
                    break;
                case "price":
                {
                    // CustomerVM.Customers = CustomerVM.Customers.OrderBy(c => c.Price).ToList();
                }
                    break;
                default:
                    break;
            }


            return PartialView("_Items", games.ToList());
        }

    }
}