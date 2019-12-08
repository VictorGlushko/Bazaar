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
    public class GamesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;


        public GamesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Games
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {

            var viewModel = new GameFormViewModel();
            viewModel.Genres = _unitOfWork.Genres.GetGenres();

            return View(viewModel);

        }

        [HttpPost]
      
        public ActionResult Create(Game game)
        {
            var viewModel = new GameFormViewModel();
            viewModel.Genres = _unitOfWork.Genres.GetGenres();
            if (!ModelState.IsValid)
            {
                return View("New", viewModel);
            }
            return View("New", viewModel);
        }



    }
}