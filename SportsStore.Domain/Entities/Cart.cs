using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Entities
{
    public class Cart
    {
        private List<Cartline> linecollection = new List<Cartline>();

        public void AddItem(Product product, int quatity)
        {
            Cartline cart = linecollection.Where(p => p.Product.ProductID == product.ProductID).FirstOrDefault();
            if(cart == null)
            {
                linecollection.Add(new Cartline { Product = product, Quatity = quatity });
            }
            else
            {
                cart.Quatity += quatity;
            }
        }

        public void RemoveItems(Product product)
        {
            linecollection.RemoveAll(p => p.Product.ProductID == product.ProductID);
           
        }

        public decimal ComputeTotal()
        {
            return linecollection.Sum(l => l.Product.Price * l.Quatity);
        }

        public void Clear()
        {
            linecollection.Clear();
        }

        public IEnumerable<Cartline> lines
        {
            get
            {
                return linecollection;
            }
        }
    }

    public class Cartline
    {
        public Product Product { set; get; }
        public int Quatity { set; get; }
    }
}
