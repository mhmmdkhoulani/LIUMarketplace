using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUMarketplace.Shared.DTOs
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string CartId { get; set; }
    }
}
