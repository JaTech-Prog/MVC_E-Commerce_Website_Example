using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCeTicaretRasim.Models
{
    public class RecentlyView
    {
        [Key]
        public int RecentlyViewID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [Required]
        public int ProductID { get; set; }

        [Column(TypeName = "datetime2"),Required]
        public DateTime ViewDate { get; set; }

        [MaxLength(300)]
        public string Notes { get; set; }

        public virtual Product Product { get; set; }

        public virtual Customer Customer { get; set; }
    }
}