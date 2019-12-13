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
            viewModel.Modes = _unitOfWork.Mode.GetModes();
            viewModel.Platforms = _unitOfWork.Platform.GetPlatforms();

            return View(viewModel);

        }

        


        [HttpPost]                
        public ActionResult Create(Game game, 
                string[] genres,
                string[] mode,
                string[] os,
                HttpPostedFileBase upload
            )
        {
            string fileName = System.IO.Path.GetFileName(upload.FileName);
            // сохраняем файл в папку Files в проекте
            string imgFullPath = "~/Content/posters/main/" + fileName;

            upload.SaveAs(Server.MapPath(imgFullPath));
     
            game.Image = new Image
            {
                MainImagePath = imgFullPath,
                PreviewImagePath = "none"
            }; 

            var resultGanres = _unitOfWork.Genres.GetGenres()
                .Where( s => genres.Contains(s.Name) ).ToList();
            var resultsOs = _unitOfWork.Platform.GetPlatforms()
                .Where(s => genres.Contains(s.Name)).ToList();
            var resultsMode = _unitOfWork.Mode.GetModes()
                .Where(s => genres.Contains(s.Name)).ToList();

            game.Genres = resultGanres;
            game.Platforms = resultsOs;
            game.Modes = resultsMode;
            game.DateAdded = DateTime.Now;

            _unitOfWork.Games.Add(game);
            _unitOfWork.Complete();


            return View("New");
        }

    }
}