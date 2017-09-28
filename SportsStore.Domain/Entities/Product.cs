using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
namespace SportsStore.Domain.Entities
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [HiddenInput(DisplayValue =false)]
        public int ProductID { get; set; }

        [Required(ErrorMessage ="Please enter a product name")]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]

        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; }

        [Required(ErrorMessage ="Please enter a positive price")]
        [Range(0.01, double.MaxValue, ErrorMessage ="Please enter a positive price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please enter a category")]
        public string Category { get; set; }


        public byte[] ImageData { set; get; }
        public string ImageMimeType { get; set; }
    }
}
