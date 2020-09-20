using System;
using System.Collections.Generic;
using System.IO;
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
            var game = _unitOfWork.Games.GetGame(slug);

            if (game == null)
                return View();


            var gvm = new GameFormViewModel(game);


            gvm.GalleryImages = Directory.EnumerateFiles(Server.MapPath("~/Content/Img/gallery/" + slug + "/HR"))
                .Select(fn => Path.GetFileName(fn));

            return View(gvm);
        }
    }
}