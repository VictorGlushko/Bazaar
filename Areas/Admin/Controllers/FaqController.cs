using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bazaar.Areas.Admin.Controllers
{
    public class FaqController : Controller
    {
        // GET: Admin/Faq
        public ActionResult Index()
        {
            return View();
        }
    }
}