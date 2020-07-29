using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCeTicaretRasim.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [Required]
        public int OrderDetailID { get; set; }

        [Required]
        public int PaymentID { get; set; }

        public int ShippingID { get; set; }

        public decimal? Discount { get; set; }

        public decimal? Taxes { get; set; }

        public decimal? TotalAmount { get; set; }

        public bool IsCompleted { get; set; }

        [Column(TypeName ="datetime2")]
        public DateTime OrderDate { get; set; }

        public bool Dispatched { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DispatchedDate { get; set; }

        public bool Shipped { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ShippedDate { get; set; }

        public bool Delivered { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DeliveryDate { get; set; }

        [MaxLength(500)]
        public string Notes { get; set; }

        public bool CancelOrder { get; set; }

        public virtual Payment Payment { get; set; }

        public virtual OrderDetail OrderDetail { get; set; }

        public virtual ShippingDetail ShippingDetail { get; set; }

        
    }
}