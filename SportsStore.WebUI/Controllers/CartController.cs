using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository productrepository;
        private IOrderProcessor orderProcessor;
        public CartController(IProductRepository pro, IOrderProcessor iorderProcessor)
        {
            productrepository = pro;
            orderProcessor = iorderProcessor;
        }
        // GET: Cart
        
        public RedirectToRouteResult AddToCart(Cart cart, int productID, string returnUrl)
        {
            Product product = productrepository.Products.FirstOrDefault(p => p.ProductID == productID);
            if(product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productID, string returnUrl)
        {
            Product product = productrepository.Products.FirstOrDefault(p => p.ProductID == productID);
            //Product product = productrepository.Products.Where(p => p.ProductID == productID).FirstOrDefault();
            if(product != null)
            {
                cart.RemoveItems(product);
            }
            
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summury(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel() { Cart = cart, ReturnUrl = returnUrl });
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult CheckOut(Cart cart, ShippingDetails shippingDetails)
        {
            if(cart.lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed! ");
            }
            else
            {
                return View(shippingDetails);
            }
        }
        /*
        public Cart GetCart()
        {
            Cart cart;
            if(Session["Cart"] != null)
            {
                cart =(Cart) Session["Cart"];
            }

            else
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        */
        
    }
}