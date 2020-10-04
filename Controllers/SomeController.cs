using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bazaar.Controllers
{
    public class SomeController : Controller
    {
        // GET: Some
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index2(string qwe)
        {
            return View();
        }
    }
}