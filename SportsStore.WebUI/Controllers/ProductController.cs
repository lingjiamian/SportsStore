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
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 3;
        public ProductController(IProductRepository repository)
        {
            this.repository = repository;

        }

        public ViewResult List(string category, int page = 1)
        {
            //return View(repository.Products);
            //return View(repository.Products.OrderBy(p => p.ProductID).Skip((page-1)*PageSize).Take(5));
            ProductsListViewModel productsListViewModel = new ProductsListViewModel()
            {
                Products = repository.Products.Where(p => category == null || p.Category == category).OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize),
                pageInfo = new PageInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? repository.Products.Count() : repository.Products.Where(p => p.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(productsListViewModel);
        }

        public FileContentResult GetImage(int productID)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productID);
            if(product != null)
            {
                return File(product.ImageData, product.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}