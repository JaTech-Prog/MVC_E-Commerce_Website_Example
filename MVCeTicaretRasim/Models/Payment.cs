using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCeTicaretRasim.Models
{
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }

        [Required]
        public int Type { get; set; }
        
        public decimal? CreditAmount { get; set; }
        public decimal? DebitAmount { get; set; }
        public decimal? Balance { get; set; }

        [Column(TypeName ="datetime2")]
        public DateTime PaymentDateTime { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual PaymentType PaymentType { get; set; }
    }
}