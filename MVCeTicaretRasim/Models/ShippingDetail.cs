using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCeTicaretRasim.Models
{
    public class ShippingDetail
    {
        [Key]
        public int ShippingID { get; set; }

        [MaxLength(50),Required]
        public string FirstName { get; set; }

        [MaxLength(50), Required]
        public string LastName { get; set; }

        [MaxLength(50), Required]
        public string Email { get; set; }

        [MaxLength(20), Required]
        public string Mobile { get; set; }

        [MaxLength(100), Required]
        public string Address { get; set; }

        [MaxLength(20)]
        public string Province { get; set; }

        [MaxLength(20)]
        public string City { get; set; }

        [MaxLength(50)]
        public string PostalCode { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}