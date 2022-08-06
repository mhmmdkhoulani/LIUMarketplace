using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUMarketplace.Models.Models
{
    public class Cart
    {
        [Key]
        public string Id { get; set; }
        public string UserId { get; set; }
        public virtual List<CartItem> CartItems { get; set; }
    }
}
