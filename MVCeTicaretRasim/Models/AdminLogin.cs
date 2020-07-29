using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCeTicaretRasim.Models
{
    public class AdminLogin
    {
        [Key]
        public int LoginID { get; set; }

        [Required]
        public int EmpID { get; set; }

        [MaxLength(50),Required]
        public string UserName { get; set; }

        [MaxLength(50), Required]
        public string Password { get; set; }

        public int RoleType { get; set; }

        [MaxLength(50)]
        public string Notes { get; set; }

        public virtual Role Role { get; set; }
        public virtual AdminEmployee AdminEmployee { get; set; }

    }
}