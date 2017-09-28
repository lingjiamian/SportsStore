﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private EFDBContext context = new EFDBContext();
        public IEnumerable<Product> Products
        {
            get
            {
                return context.Products;
            }
        }

        public Product DeleteProduct(int ProductID)
        {
            Product dbEntry = context.Products.Find(ProductID);
            if(dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveChange(Product product)
        {
            if(product.ProductID == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product dbEntry = context.Products.Find(product.ProductID);
                if(dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Price = product.Price;
                    dbEntry.Description = product.Description;
                    dbEntry.Category = product.Category;
                    dbEntry.ImageData = product.ImageData;
                    dbEntry.ImageMimeType = product.ImageMimeType;
                }
            }
            context.SaveChanges();
        }
    }
}
