using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUMarketplace.Shared.DTOs
{
    public class ContactDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string  Email { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
