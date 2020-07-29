using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCeTicaretRasim.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [MaxLength(80),Required]
        public string ProductName { get; set; }

        [Required]
        public int SupplierID { get; set; }

        [Required]
        public int SubCategoryID { get; set; }

        [MaxLength(50)]
        public string QuantityPerUnit  { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        public decimal? OldPrice { get; set; }

        [MaxLength(50)]
        public string UnitWeigth { get; set; }

        [MaxLength(30)]
        public string Size { get; set; }

        public decimal? Discount { get; set; }

        public int UnitsInStock { get; set; }

        public int UnitOnOrder { get; set; }

        public bool ProductAvailable { get; set; }

        [MaxLength(500)]
        public string ImageUrl { get; set; }

        [MaxLength(100)]
        public string AltText { get; set; }

        public bool AddBadge { get; set; }

        [MaxLength(50)]
        public string OfferTitle { get; set; }

        [MaxLength(50)]
        public string OfferBadgeClass { get; set; }

        [Required,MaxLength(600)]
        public string ShortDescription { get; set; }

        [Required,MaxLength(2000)]
        public string LongDescription { get; set; }

        [MaxLength(500)]
        public string Picture1 { get; set; }

        [MaxLength(500)]
        public string Picture2 { get; set; }

        [MaxLength(500)]
        public string Picture3 { get; set; }

        [MaxLength(500)]
        public string Picture4 { get; set; }

        [MaxLength(250)]
        public string Notes { get; set; }

        public virtual SubCategory SubCategory { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual ICollection<RecentlyView> RecentlyViews { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Wishlist> Wishlists { get; set; }
    }
}