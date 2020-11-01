using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Bazaar.Domain.Entities;
using Bazaar.Domain.ViewModel;
using Bazaar.Repository;

namespace Bazaar.Areas.Admin.Controllers
{
    public class FaqController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public FaqController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            var faqItemVM = new FaqItemViewModel();

            faqItemVM.Order = _unitOfWork.FaqItem.GetFaqItems().Max(f => f.Order) + 1;

            return View("FaqForm",faqItemVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(FaqItemViewModel viewModel)
        {
            
            if(!ModelState.IsValid)
                return View("FaqForm", viewModel);


            if (viewModel.Order == null)
            {
                viewModel.Order = _unitOfWork.FaqItem.GetFaqItems().Max(f=>f.Order) + 1;
            }

            var faqItem = new FaqItem();
            Mapper.Map(viewModel, faqItem);


            if (viewModel.FaqItemId == 0)
            {
                _unitOfWork.FaqItem.AddFaqItem(faqItem);
            }
            else
            {
                var faqItemInDb = _unitOfWork.FaqItem.GetFaqItem(viewModel.FaqItemId);
                if (faqItemInDb == null)
                    return HttpNotFound();

                _unitOfWork.FaqItem.UpdateFaqItem(viewModel.FaqItemId,faqItem);
            }
    

            _unitOfWork.Complete();
            return RedirectToAction("Index","Faq");
        }

        public ActionResult Edit(int id)
        {

            var faqItemInDb = _unitOfWork.FaqItem.GetFaqItem(id);



            var viewModel = new FaqItemViewModel();
            Mapper.Map(faqItemInDb, viewModel);

            return View("FaqForm", viewModel);
        }
    }
}