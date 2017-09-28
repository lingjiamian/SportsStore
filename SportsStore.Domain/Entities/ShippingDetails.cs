using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SportsStore.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { set; get; }

        [Required(ErrorMessage = "Please enter your first address line")]
        public string line1 { set; get; }
        public string line2 { set; get; }
        public string line3 { set; get; }

        [Required(ErrorMessage = "Please enter your city name")]
        public string City { set; get; }

        [Required(ErrorMessage ="Please enter your state name")]
        public string State { set; get; }
        public string Zip { set; get; }

        [Required(ErrorMessage = "Please enter your country name")]
        public string Country { set; get; }
        public string GiftWrap { set; get; }
    }
}
