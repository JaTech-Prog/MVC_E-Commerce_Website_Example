using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCeTicaretRasim.Models
{
    public class PaymentType
    {
        [Key]
        public int PayTypeID { get; set; }
        [Required,MaxLength(50)]
        public string TypeName { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
    }
}