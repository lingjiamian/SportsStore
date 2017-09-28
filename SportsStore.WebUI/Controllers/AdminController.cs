using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository iproductrepository;

        public AdminController(IProductRepository repository)
        {
            iproductrepository = repository;
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View(iproductrepository.Products);
            
        }

        public ViewResult Edit(int productId)
        {
            Product product = iproductrepository.Products.FirstOrDefault(p => p.ProductID == productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {

                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);   
                }

                iproductrepository.SaveChange(product);
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        public ActionResult Create()
        {
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(int productID)
        {
            Product delProduct = iproductrepository.DeleteProduct(productID);
            if(delProduct != null)
            {
                TempData["message"] = string.Format("{0} has been deleted", delProduct.Name);
            }
            return RedirectToAction("Index");
        }
    }
}