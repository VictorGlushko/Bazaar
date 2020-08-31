using System;
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

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _games = _unitOfWork.Games.GetGames().ToList();


        }

        public ActionResult Index()
        {
            GamesViewModel gvm = new GamesViewModel();
            gvm.Games = _games;

            return View(gvm);
        }


    }
}