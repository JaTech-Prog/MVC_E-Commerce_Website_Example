using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCeTicaretRasim.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailID { get; set; }

        [Required]
        public int ProductID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        public decimal? UnitPrice { get; set; }

        public int Quantity { get; set; }

        public decimal? Discount { get; set; }

        public decimal? TotalAmount { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime OrderDate { get; set; }

        public bool IsCompleted { get; set; }

        public virtual Product Product { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}