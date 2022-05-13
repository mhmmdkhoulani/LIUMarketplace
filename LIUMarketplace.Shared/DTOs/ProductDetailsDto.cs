using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUMarketplace.Shared.DTOs
{
    public class ProductDetailsDto
    {
        public string Id { get; set; }
        public string Name { get; set; } 
        public string Desciption { get; set; }
        public string Status { get; set; }
        public double Price { get; set; }
        public string ImageCoverUrl { get; set; }
        public CategoryDto Category { get; set; }
    }
}
