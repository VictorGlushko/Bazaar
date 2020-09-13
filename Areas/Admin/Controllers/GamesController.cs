using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Hosting;
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
                ModelState.AddModelError("", "That game name is taken!");

                return View("New", game);
            }


            var originalDirectory = new DirectoryInfo(string.Format($"{Server.MapPath(@"~")}Content\\img\\uploads"));
            var vOriginalDirectory = @"..\..\Content\img\uploads";


            string gameDirPath = Path.Combine(originalDirectory.ToString(), slug);
            string vGameDirPath = Path.Combine(vOriginalDirectory, slug);

            if (!Directory.Exists(gameDirPath))
            {
                Directory.CreateDirectory(gameDirPath);
            }

            var MediaResources = new MediaResources();


            string fileNamePoster = Path.GetFileName(poster.FileName);

            string vFileFullPath = Path.Combine(vGameDirPath, fileNamePoster);
            string fileFullPath = Path.Combine(gameDirPath, fileNamePoster);


            MediaResources.MainImagePath = vFileFullPath;
            poster.SaveAs(fileFullPath);



            foreach (var img in screens)
            {
                string fileNameScreen = Path.GetFileName(img.FileName);
                string screenFullPath = Path.Combine(gameDirPath, fileNameScreen);

                //ImagePath ip = new ImagePath { Path = Path.Combine(savePath, screenFullPath) };
                //ip.Image = image;
                //images.Add(ip);

                img.SaveAs(screenFullPath);

            }

            MediaResources.GalleryPath = gameDirPath;
            MediaResources.VideoLink = game.MediaResources.VideoLink;



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

            gameDto.Modes = resultsMode;
            gameDto.Platforms = resultsPlatform;
            gameDto.DateAdded = game.DateAdded;
            gameDto.Quantity = game.Quantity;
            gameDto.MediaResources = MediaResources;

            MediaResources.Game = gameDto;
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