using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCeTicaretRasim.Models
{
    public class AdminEmployee
    {
        [Key]
        public int EmpID { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        public int Age { get; set; }

        public bool Gender { get; set; }

        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(15)]
        public string Phone { get; set; }

        [MaxLength(15)]
        public string Mobile { get; set; }

        [MaxLength(100)]
        public string Picture { get; set; }

        public virtual ICollection<AdminLogin> AdminLogins { get; set; }
    }
}