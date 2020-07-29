using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCeTicaretRasim.Models
{
    public class Wishlist
    {
        [Key]
        public int WishlistID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [Required]
        public int ProductID { get; set; }

        public bool IsActive { get; set; }
        public virtual Product Product { get; set; }
        public virtual Customer Customer { get; set; }
    }
}