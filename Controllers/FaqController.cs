using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Bazaar.Domain.Dtos;
using Bazaar.Domain.Entities;
using Bazaar.Domain.ViewModel;
using Bazaar.Repository;

namespace Bazaar.Controllers
{
    public class FaqController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public FaqController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        // GET: Faq
        public ActionResult Index()
        {
            var viewmodel = _unitOfWork.FaqItem.GetFaqItems().Select(Mapper.Map<FaqItem, FaqItemViewModel>).OrderBy(f =>f.Order);

            if (viewmodel == null)
                return HttpNotFound();

            return View("Index",viewmodel);
        }
    }
}