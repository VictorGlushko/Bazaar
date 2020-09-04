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
    public class GameController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public GameController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Game
        public ActionResult Index(string slug)
        {
            var game = _unitOfWork.Games.GetGames().FirstOrDefault(g => g.Slag.Equals(slug));

            var gvm = new GameFormViewModel(game);


            return View(gvm);
        }
    }
}