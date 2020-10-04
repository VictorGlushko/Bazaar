﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bazaar.Domain.Entities;
using Bazaar.Domain.ViewModel;
using Bazaar.Repository;

namespace Bazaar.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private List<Game> _games;
        private List<Genre> _genres;
        private List<Game> _discountGames;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _games = _unitOfWork.Games.GetGames().ToList();
            _genres = _unitOfWork.Genres.GetGenres().ToList();
            _discountGames = _unitOfWork.Games.GetGames().Where(g => g.FinalPrice < 500).ToList();
        }

        public ActionResult Index()
        {
            GamesViewModel gvm = new GamesViewModel();
            gvm.Games = _games;
            gvm.Genres = _genres;
            gvm.DiscountGames = _discountGames;

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