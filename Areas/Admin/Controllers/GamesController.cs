using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bazaar.Domain;
using Bazaar.Domain.Entities;
using Bazaar.Domain.ViewModel;
using Bazaar.Repository;


namespace Bazaar.Areas.Admin.Controllers
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
        public ActionResult Create(GameFormViewModel game,
                string[] genres,
                string[] mode,
                string[] os,
                HttpPostedFileBase poster,
                IEnumerable<HttpPostedFileBase> screens
            )
        {

            if (ModelState.IsValid)
            {

            }

            

            string slug = game.Name.GenerateSlug();
            game.Slug = slug;

            if (_unitOfWork.Games.GetGames().Any(g => g.Slag.Equals(slug)))
            {
                game.Genres = _unitOfWork.Genres.GetGenres();
                game.Modes = _unitOfWork.Mode.GetModes();
                game.Platforms = _unitOfWork.Platform.GetPlatforms();
                ModelState.AddModelError("","That game name is taken!");

                return View("New", game);
            }

            

            string vDirPath = Path.Combine("~\\Content\\posters\\", game.Name.GenerateSlug());
            string savePath = Path.Combine("..\\..\\Content\\posters\\", game.Name.GenerateSlug());

            string dirPath = Server.MapPath(vDirPath);
            if (!Directory.Exists(dirPath))
            {
                var dir = Directory.CreateDirectory(dirPath);
            }

            var image = new Image();
            var images = new List<ImagePath>();



            string fileNamePoster = Path.GetFileName(poster.FileName);
            string fileFullPath = Path.Combine(vDirPath, fileNamePoster);
            image.MainImagePath = "None";
            image.PreviewImagePath = Path.Combine(savePath, fileNamePoster);

            poster.SaveAs(Server.MapPath(fileFullPath));




            foreach (var img in screens)
            {
                string fileNameScreen = Path.GetFileName(img.FileName);
                string screenFullPath = Path.Combine(vDirPath, fileNameScreen);

                ImagePath ip = new ImagePath { Path = Path.Combine(savePath, screenFullPath) };
                ip.Image = image;
                images.Add(ip);

                img.SaveAs(Server.MapPath(screenFullPath));

            }

            image.Posters = images;

            var resultGanres = _unitOfWork.Genres.GetGenres()
                .Where(s => genres.Contains(s.Name)).ToList();
            var resultsPlatform = _unitOfWork.Platform.GetPlatforms()
                .Where(s => os.Contains(s.Name)).ToList();
            var resultsMode = _unitOfWork.Mode.GetModes()
                .Where(s => mode.Contains(s.Name)).ToList();

            Game gameDto = new Game();

            gameDto.Id = game.Id;
            gameDto.Name = game.Name;
            gameDto.Slag = game.Slug;
            gameDto.OriginalPrice = game.OriginalPrice;
            gameDto.FinalPrice = game.FinalPrice;
            gameDto.Description = game.Description;
            gameDto.Genres = resultGanres;
            gameDto.ReleaseDate = game.ReleaseDate;
            gameDto.Publisher = game.Publisher;
            gameDto.Developer = game.Developer;
            gameDto.Language = game.Language;
            //gameDto.Tags = 
            gameDto.Modes = resultsMode;
            gameDto.Platforms = resultsPlatform;
            gameDto.DateAdded = game.DateAdded;
            gameDto.Quantity = game.Quantity;
            gameDto.Image = image;

            image.Game = gameDto;
            gameDto.Genres = resultGanres;
            gameDto.Platforms = resultsPlatform;
            gameDto.Modes = resultsMode;
            gameDto.DateAdded = DateTime.Now;

            _unitOfWork.Games.Add(gameDto);
            _unitOfWork.Complete();


            return new RedirectResult("~/Admin/Games");
        }

    }
}