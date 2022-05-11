using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUMarketplace.Shared.DTOs
{
    public class RegisterDto
    {
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(25)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(25)]
        public string LastName { get; set; }

        [Required]
        [StringLength(25)]
        public string Campus { get; set; }

        [Required]
        [StringLength(25)]
        public string Major { get; set; }

        [Required]
        [StringLength(50), MinLength(5)]
        public string Password { get; set; }

        [Required]
        [StringLength(50), MinLength(5)]
        public string ConfirmPassword { get; set; }
    }
}
