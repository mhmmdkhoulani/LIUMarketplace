using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUMarketplace.Models.Models
{
   
    public class CartItem
    {
        public int Id { get; set; } 
        public string ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string CartId { get; set; }
        public virtual Cart Cart { get; set; }
    }
}
