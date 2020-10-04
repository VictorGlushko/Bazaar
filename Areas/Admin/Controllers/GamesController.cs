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


            var originalDirectory = new DirectoryInfo(string.Format($"{Server.MapPath(@"~")}Content\\img\\gallery"));
            var vOriginalDirectory = @"..\..\Content\img\gallery";


            string gameDirPath = Path.Combine(originalDirectory.ToString(), slug);
            string vGameDirPath = Path.Combine(vOriginalDirectory, slug);
            string hrPath = Path.Combine(gameDirPath, "HR");
            string lrPath = Path.Combine(gameDirPath, "LR");


            if (!Directory.Exists(gameDirPath))
            {
                Directory.CreateDirectory(gameDirPath);
                Directory.CreateDirectory(hrPath);
                Directory.CreateDirectory(lrPath);
            }


            var SystemRequirements = new SystemRequirements();

            SystemRequirements.OperatingSystem = game.SystemRequirements.OperatingSystem;
            SystemRequirements.Processor       = game.SystemRequirements.Processor;
            SystemRequirements.RAM             = game.SystemRequirements.RAM;
            SystemRequirements.VideoCard       = game.SystemRequirements.VideoCard;
            SystemRequirements.HDD             = game.SystemRequirements.HDD;



            var MediaResources = new MediaResources();



            string fileNamePoster = Path.GetFileName(poster.FileName);

            string vFileFullPath = Path.Combine(vGameDirPath, fileNamePoster);
            string fileFullPath = Path.Combine(gameDirPath, fileNamePoster);


            MediaResources.MainImagePath = vFileFullPath;
            poster.SaveAs(fileFullPath);


            


            foreach (var img in screens)
            {
                string fileNameScreen = Path.GetFileName(img.FileName);
                string hrFullPath = Path.Combine(hrPath, fileNameScreen);
                string lrFullPath = Path.Combine(lrPath, fileNameScreen);

                img.SaveAs(hrFullPath);

                WebImage lrImage = new WebImage(img.InputStream);
                lrImage.Resize(350, 233);
                lrImage.Save(lrFullPath);
                
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
            gameDto.SystemRequirements = SystemRequirements;

            gameDto.Genres = resultGanres;
            gameDto.Platforms = resultsPlatform;
            gameDto.Modes = resultsMode;
            gameDto.DateAdded = DateTime.Now;

            MediaResources.Game = gameDto;
            SystemRequirements.Game = gameDto;

            _unitOfWork.Games.Add(gameDto);
            _unitOfWork.Complete();


            return new RedirectResult("~/Admin/Games");
        }



    }
}