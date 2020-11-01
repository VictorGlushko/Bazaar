using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bazaar.Domain.Entities;
using Bazaar.Domain.ViewModel;
using Bazaar.Repository;
using Microsoft.Ajax.Utilities;

namespace Bazaar.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CatalogController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Catalog
        public ActionResult Index()
        {
            var viewModel = new CatalogViewModel();
            viewModel.Genres = _unitOfWork.Genres.GetGenres().ToList();
            viewModel.Modes = _unitOfWork.Mode.GetModes().ToList();
            viewModel.Platforms = _unitOfWork.Platform.GetPlatforms().ToList();
            viewModel.Games = _unitOfWork.Games.GetGames().OrderBy(g => g.FinalPrice);
            viewModel.MinPrice = _unitOfWork.Games.GetGames().Select(x => x.FinalPrice).Min();
            viewModel.MaxPrice = _unitOfWork.Games.GetGames().Select(x => x.FinalPrice).Max();
            return View("Index",viewModel);
        }

        [HttpPost]
        public ActionResult SetFilter(FromCatalogViewModel viewModel)
        {
            var games = _unitOfWork.Games.GetGames().AsQueryable();
            //List<Game> resultwe = new List<Game>();

            if (viewModel.Genres == null)
                viewModel.Genres = _unitOfWork.Genres.GetGenres().Select(g => g.Name).ToList();

            if (viewModel.Platforms == null)
                viewModel.Platforms = _unitOfWork.Platform.GetPlatforms().Select(g => g.Name).ToList();

            if (viewModel.Modes == null)
                viewModel.Modes = _unitOfWork.Mode.GetModes().Select(g => g.Name).ToList();

            var resultwe = games.Where(p => p.Genres.Any(c => viewModel.Genres.Any(c2 => c2 == c.Name)) && 
                                        p.Platforms.Any(c => viewModel.Platforms.Any(c2 => c2 == c.Name)) &&
                                        p.Modes.Any(c => viewModel.Modes.Any(c2 => c2 == c.Name)));

            resultwe = resultwe.Where(g => g.FinalPrice >= viewModel.MinPrice && g.FinalPrice <= viewModel.MaxPrice);

            switch (viewModel.Sorting)
            {
                case "price":
                {
                    if(viewModel.Order == FromCatalogViewModel.OrderASC)
                        resultwe = resultwe.OrderBy(g => g.FinalPrice);
                    else
                        resultwe = resultwe.OrderByDescending(g => g.FinalPrice);
                }break;
                    
                case "date":
                {
                    if (viewModel.Order == FromCatalogViewModel.OrderASC)
                        resultwe = resultwe.OrderBy(g => g.ReleaseDate);
                    else
                        resultwe = resultwe.OrderByDescending(g => g.ReleaseDate);
                } break;
                   
                case "rating":
                {

                } break;
                   
                case "discount":
                {

                }break;
                    
            }

            var result = resultwe.ToList();

          ViewData["count"] = result.Count();

            //foreach (var item in viewModel.Genres)
            //{
            //    var qgames =  games.Where(g => g.Genres.Select(q => q.Name).Contains(item));

            //    resultwe.AddRange(qgames.ToList());

            //}
            return PartialView("../Home/_Items", result);
        }
    }
}