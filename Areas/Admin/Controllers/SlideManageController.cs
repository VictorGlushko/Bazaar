using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using AutoMapper;
using Bazaar.Domain.Entities;
using Bazaar.Domain.ViewModel;
using Bazaar.Repository;

namespace Bazaar.Areas.Admin.Controllers
{
    public class SlideManageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SlideManageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Admin/SlideManage
        public ActionResult Index()
        {
            var _slides = _unitOfWork.CarouselSlides.GetCarouselSlides().Select(Mapper.Map<CarouselSlide,SlideViewModel>);
            return View("SlideManagePage", _slides);
        }

        public ActionResult New()
        {
            var slide = new SlideViewModel();

            var slides = _unitOfWork.CarouselSlides.GetCarouselSlides();
            if(slides.Count() > 0)
                slide.Order = slides.Max(s => s.Order) + 1;
            else
                slide.Order = 1;


            return View("SlideForm", slide);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(SlideViewModel viewModel , HttpPostedFileBase uploadImage)
        {
            if (!ModelState.IsValid || uploadImage == null)
                return View("SlideForm", viewModel);

            var originalDirectory = $"{Server.MapPath(@"~")}Content\\img\\slider";
            var vOriginalDirectory = @"..\..\Content\img\slider";

            string oFullPath = Path.Combine(originalDirectory, uploadImage.FileName);
            string vFullPayh = Path.Combine(vOriginalDirectory, uploadImage.FileName);

            if (!Directory.Exists(originalDirectory))
                Directory.CreateDirectory(originalDirectory);

            if (uploadImage.ContentLength > 0)
            {
                string ext = uploadImage.ContentType.ToLower();
                if (ext != "image/jpg" &&
                    ext != "image/jpeg" &&
                    ext != "image/png"
                )
                {
                    ModelState.AddModelError("","Не верный формат изображения");
                    return View("SlideForm", viewModel);
                }

                WebImage image = new WebImage(uploadImage.InputStream);
                var imgSlide =  image.Resize(780, 440).Crop(1, 1).Write();
                imgSlide.Save(oFullPath);
            }


            viewModel.ImgPath = vFullPayh;
            _unitOfWork.CarouselSlides.AddCarouselSlide(Mapper.Map<CarouselSlide>(viewModel));
            _unitOfWork.Complete();

            return RedirectToAction("Index");
        }
    }
}