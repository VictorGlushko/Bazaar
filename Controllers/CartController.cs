using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bazaar.Models;
using Bazaar.Repository;

namespace Bazaar.Controllers
{
    public class CartController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        public string AddToCart(int id)
        {
            var game = _unitOfWork.Games.GetGame(id);

            if (game != null)
            {
                GetCart().AddItem(game,1);
            }

            return GetCart().Lines.Count().ToString();
        }

        public Cart GetCart()
        {
            Cart cart = (Cart) Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }

            return cart;
        }



        public ActionResult GetCartItems()
        {
           
            return PartialView("_CartItems", GetCart());
        }
    }
}