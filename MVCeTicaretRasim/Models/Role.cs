using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCeTicaretRasim.Models
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        [MaxLength(50),Required]
        public string RoleName { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public virtual ICollection<AdminLogin> AdminLogins { get; set; }
    }
}