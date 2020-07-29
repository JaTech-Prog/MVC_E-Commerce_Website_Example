using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCeTicaretRasim.Models
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        public int CustomerID { get; set; }

        [Required]
        public int ProductID { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(500)]
        public string Reviews { get; set; }

        public int Rates { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DateTime { get; set; }

        public bool IsDeleted { get; set; }

        public virtual Product Product { get; set; }

        public virtual Customer Customer { get; set; }
    }
}