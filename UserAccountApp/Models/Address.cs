using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAccountApp.Models
{
    public class Address
    {
        
        public int AddressId { get; set; }

        [Required]
        [StringLength(100)]
        public string City { get; set; }

        [StringLength(100)] // Optional region validation
        public string Region { get; set; }

        [Required]
        [StringLength(200)]
        public string Street { get; set; }

        [Required]
        [StringLength(100)]
        public string Country { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
