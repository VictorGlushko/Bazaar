using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bazaar.Controllers
{
    public class GameController : Controller
    {
        // GET: Game
        public ActionResult Index(int id)
        {
            return View(id);
        }
    }
}